(function () {
    'use strict';

    function messagesCtrl(breadcrumbService, apiFactory, loadingContentService, notificationService) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('messages');
        getMessagesData();

        self.selectedMessageModel = {
            isSelected: false,
            selectedClusterIndex: -1
        }

        self.lastMessagesListModel = [];

        self.replyMessageModel = {
            text: null,
            previousMessageId: -1,
            isOpen:false
        }

        self.replyMessageStart = function() {
            self.replyMessageModel.previousMessageId = self.lastMessagesListModel[self.selectedMessageModel.selectedClusterIndex].messages[0];
            self.replyMessageModel.isOpen = true;
        }

        self.cancelMessageReply = function() {
            self.replyMessageModel = {
                text: null,
                previousMessageId: -1,
                isOpen: false
            }
        }

        self.toggleMessage = function (messageIndex) {
            self.replyMessageModel = {
                text: null,
                previousMessageId: -1,
                isOpen: false
            }
            if (self.selectedMessageModel.selectedClusterIndex === messageIndex) {
                self.selectedMessageModel = {
                    isSelected: false,
                    selectedClusterIndex: -1,
                    messages: null,
                    title: null
                }
            } else {
                self.selectedMessageModel = {
                    isSelected: true,
                    selectedClusterIndex: messageIndex,
                    messages: self.lastMessagesListModel[messageIndex].messages,
                    title: self.lastMessagesListModel[messageIndex].title
                }
            }
        }

        function getMessagesData() {
            loadingContentService.setIsLoading('getMessagesLoader', true);
            apiFactory.post(apiFactory.apiEnum.GetUserMessagesClusters, {}).then(function (data) {
                if (data.IsValid) {
                    createMessagesList(data.Result);
                }

                loadingContentService.setIsLoading('getMessagesLoader', false);
                notificationService.showNotifications(data);
                console.log(data);
            }, function (e) {
                console.log(e);
                loadingContentService.setIsLoading('getMessagesLoader', false);
                Materialize.toast("Wystąpił błąd podczas łączenia się z serwerem.", 8000, 'toast-red');
            });
        }

        function createMessagesList(data) {
            var user = data.User;
            for (var i = 0; i < data.Clusters.length; i++) {
                var cluster = data.Clusters[i];

                var lastMessage = getLastVisibleMessage(cluster.Messages);
                var starterMessage = cluster.Messages[cluster.Messages.length - 1];
                var receiverUser = cluster.ReceiverUser;
                var itemListModel = {
                    createDate: lastMessage.CreateDate,
                    text: lastMessage.Text,
                    title: starterMessage.Title,
                    imgPath: receiverUser.Id === lastMessage.UserId ? "/images/user-avatars/" + receiverUser.ImgId + ".jpg" : "/images/user-avatars/" + user.ImgId + ".jpg",
                    messages: getMessages(cluster.Messages, receiverUser, user)
                }
                self.lastMessagesListModel.push(itemListModel);
            }
            console.log(self.lastMessagesListModel);
        }

        function getMessages(messages, receiverUser, user) {
            var messagesData = [];
            for (var i = 0; i < messages.length; i++) {
                var message = messages[i];
                messagesData.push({
                    imgPath: receiverUser.Id === message.UserId ? "/images/user-avatars/" + receiverUser.ImgId + ".jpg" : "/images/user-avatars/" + user.ImgId + ".jpg",
                    initials: receiverUser.Id === message.UserId ? receiverUser.Initials : user.Initials,
                    createDate: message.CreateDate,
                    text: message.Text
                });
            }
            return messagesData;
        }

        function getLastVisibleMessage(messages) {
            for (var j = 0; j < messages.length; j++) {
                if (messages[j].IsDisplayed === true) {
                    return messages[j];
                }
            }
        }
    }

    angular.module('portalApp').controller('messagesCtrl', ['breadcrumbService', 'apiFactory', 'loadingContentService', 'notificationService', messagesCtrl]);
})();