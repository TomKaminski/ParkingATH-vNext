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

        self.messagesModel = {
            user: null,
            list: []
        }

        self.replyMessageModel = {
            text: null,
            previousMessageId: -1,
            isOpen:false,
            disableButton: false
        }

        self.replyMessageStart = function() {
            self.replyMessageModel.previousMessageId = self.messagesModel.list[self.selectedMessageModel.selectedClusterIndex].id;
            self.replyMessageModel.isOpen = true;
        }

        function appendToMessagesListModel(selectedClusterIndex, messageData) {
            var cluster = self.messagesModel.list[selectedClusterIndex];

            cluster.createDate = messageData.CreateDate;
            cluster.text = messageData.Text;
            cluster.imgPath = cluster.receiverUser.Id === messageData.UserId ? "/images/user-avatars/" + cluster.receiverUser.ImgId + ".jpg" : "/images/user-avatars/" + self.messagesModel.user.ImgId + ".jpg";
            cluster.id = messageData.Id;
            cluster.messages.unshift({
                imgPath: cluster.receiverUser.Id === messageData.UserId ? "/images/user-avatars/" + cluster.receiverUser.ImgId + ".jpg" : "/images/user-avatars/" + self.messagesModel.user.ImgId + ".jpg",
                initials: cluster.receiverUser.Id === messageData.UserId ? cluster.receiverUser.Initials : self.messagesModel.user.Initials,
                createDate: messageData.CreateDate,
                text: messageData.Text
            });
        }

        self.replyMessagePost = function () {
            self.replyMessageModel.disableButton = true;
            apiFactory.post(apiFactory.apiEnum.ReplyPortalMessage, { PreviousMessageId: self.replyMessageModel.previousMessageId, Text: self.replyMessageModel.text }).then(function (data) {
                if (data.IsValid) {
                    appendToMessagesListModel(self.selectedMessageModel.selectedClusterIndex, data.Result);
                    self.replyMessageModel = {
                        text: null,
                        previousMessageId: -1,
                        isOpen: false,
                        disableButton: false
                    }
                }
                self.replyMessageModel.disableButton = false;
                notificationService.showNotifications(data);
                console.log(data);
            }, function (e) {
                self.replyMessageModel.disableButton = false;
                console.log(e);
                Materialize.toast("Wystąpił błąd podczas łączenia się z serwerem.", 8000, 'toast-red');
            });
        }

        self.cancelMessageReply = function () {
            self.replyMessageForm.$setUntouched();
            self.replyMessageForm.$setPristine();
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
                    messages: self.messagesModel.list[messageIndex].messages,
                    title: self.messagesModel.list[messageIndex].title
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
            self.messagesModel.user = data.User;
            for (var i = 0; i < data.Clusters.length; i++) {
                var cluster = data.Clusters[i];

                var lastMessage = cluster.Messages[0];
                var starterMessage = cluster.Messages[cluster.Messages.length - 1];
                var receiverUser = cluster.ReceiverUser;
                var itemListModel = {
                    createDate: lastMessage.CreateDate,
                    text: lastMessage.Text,
                    title: starterMessage.Title,
                    imgPath: receiverUser.Id === lastMessage.UserId ? "/images/user-avatars/" + receiverUser.ImgId + ".jpg" : "/images/user-avatars/" + self.messagesModel.user.ImgId + ".jpg",
                    id: lastMessage.Id,
                    messages: getMessages(cluster.Messages, receiverUser, self.messagesModel.user),
                    receiverUser: receiverUser
                }
                self.messagesModel.list.push(itemListModel);
            }
            console.log(self.messagesModel);
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
    }

    angular.module('portalApp').controller('messagesCtrl', ['breadcrumbService', 'apiFactory', 'loadingContentService', 'notificationService', messagesCtrl]);
})();