(function () {
    'use strict';

    function messagesCtrl(breadcrumbService, apiFactory, loadingContentService, notificationService) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('messages');
        getMessagesData();

        var basicImgPath = "/images/user-avatars/";

        initSelectedMessageModel();
        initReplyMessageModel();
        initNewAdminMessageModel();
        initMessageModel();
        initDeleteClusterModel();

        self.toggleMessageReply = function () {
            if (self.replyMessageModel.isOpen) {
                self.replyMessageForm.$setUntouched();
                self.replyMessageForm.$setPristine();
                initReplyMessageModel();
            } else {
                self.replyMessageModel.previousMessageId = self.messagesModel.list[self.selectedMessageModel.selectedClusterIndex].id;
                self.replyMessageModel.isOpen = true;
            }
        }

        self.toggleDeleteCluster = function () {
            if (self.deleteClusterModel.isOpen) {
                initDeleteClusterModel();
            } else {
                self.deleteClusterModel.isOpen = true;
            }
        }

        self.toggleNewMessage = function () {
            if (self.newAdminMessageModel.isOpen) {
                initNewAdminMessageModel();
            } else {
                self.newAdminMessageModel.isOpen = true;
            }
        }

        self.deleteCluster = function () {
            var starterMessageId = self.selectedMessageModel.messages[self.selectedMessageModel.messages.length - 1].id;
            apiFactory.genericPost(
                function () {
                    self.deleteClusterModel.disableButton = true;
                },
                function () {
                    initDeleteClusterModel();
                    initSelectedMessageModel();
                    getMessagesData();
                },
                function (data) {
                    self.deleteClusterModel.disableButton = false;
                    notificationService.showNotifications(data);
                },
                function () {
                    self.deleteClusterModel.disableButton = false;
                },
                apiFactory.apiEnum.FakeDeleteCluster, { StarterMessageId: starterMessageId });
        }


        self.newMessagePost = function () {
            apiFactory.genericPost(
                function () {
                    loadingContentService.setIsLoading('sendQuickMessage', true);
                    self.newAdminMessageModel.disableButton = true;
                },
                function () {
                    initNewAdminMessageModel();
                    getMessagesData();
                },
                function (data) {
                    loadingContentService.setIsLoading('sendQuickMessage', false);
                    self.newAdminMessageModel.disableButton = false;
                    notificationService.showNotifications(data);
                },
                function () {
                    loadingContentService.setIsLoading('sendQuickMessage', false);
                    self.newAdminMessageModel.disableButton = false;
                },
                apiFactory.apiEnum.SendQuickMessage, { Text: self.newAdminMessageModel.text });
        }

        self.replyMessagePost = function () {
            apiFactory.genericPost(
                function () {
                    self.replyMessageModel.disableButton = true;
                },
                function (data) {
                    appendToMessagesListModel(self.selectedMessageModel.selectedClusterIndex, data.Result);
                    initReplyMessageModel();
                },
                function (data) {
                    self.replyMessageModel.disableButton = false;
                    notificationService.showNotifications(data);
                },
                function () {
                    self.replyMessageModel.disableButton = false;

                },
                apiFactory.apiEnum.ReplyPortalMessage, { PreviousMessageId: self.replyMessageModel.previousMessageId, Text: self.replyMessageModel.text });
        }

        self.toggleMessage = function (messageIndex) {
            initReplyMessageModel();
            if (self.selectedMessageModel.selectedClusterIndex === messageIndex) {
                initSelectedMessageModel();
            } else {
                self.selectedMessageModel = {
                    isSelected: true,
                    selectedClusterIndex: messageIndex,
                    messages: self.messagesModel.list[messageIndex].messages,
                    title: self.messagesModel.list[messageIndex].title
                }
            }
        }

        function appendToMessagesListModel(selectedClusterIndex, messageData) {
            var cluster = self.messagesModel.list[selectedClusterIndex];
            cluster.createDate = messageData.CreateDate;
            cluster.text = messageData.Text;
            cluster.imgPath = cluster.receiverUser.Id === messageData.UserId ? basicImgPath + cluster.receiverUser.ImgId + ".jpg" : basicImgPath + self.messagesModel.user.ImgId + ".jpg";
            cluster.id = messageData.Id;
            cluster.messages.unshift({
                imgPath: cluster.receiverUser.Id === messageData.UserId ? basicImgPath + cluster.receiverUser.ImgId + ".jpg" : basicImgPath + self.messagesModel.user.ImgId + ".jpg",
                initials: cluster.receiverUser.Id === messageData.UserId ? cluster.receiverUser.Initials : self.messagesModel.user.Initials,
                createDate: messageData.CreateDate,
                text: messageData.Text
            });
        }

        function getMessagesData() {
            apiFactory.genericPost(
             function () {
                 initMessageModel();
                 loadingContentService.setIsLoading('getMessagesLoader', true);
             },
             function (data) {
                 createMessagesList(data.Result);
             },
             function (data) {
                 loadingContentService.setIsLoading('getMessagesLoader', false);
                 notificationService.showNotifications(data);
             },
             function () {
                 loadingContentService.setIsLoading('getMessagesLoader', false);
             },
             apiFactory.apiEnum.GetUserMessagesClusters);
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
                    imgPath: receiverUser.Id === lastMessage.UserId ? basicImgPath + receiverUser.ImgId + ".jpg" : basicImgPath + self.messagesModel.user.ImgId + ".jpg",
                    id: lastMessage.Id,
                    messages: getMessages(cluster.Messages, receiverUser, self.messagesModel.user),
                    receiverUser: receiverUser
                }
                self.messagesModel.list.push(itemListModel);
            }
        }

        function getMessages(messages, receiverUser, user) {
            var messagesData = [];
            for (var i = 0; i < messages.length; i++) {
                var message = messages[i];
                messagesData.push({
                    imgPath: receiverUser.Id === message.UserId ? basicImgPath + receiverUser.ImgId + ".jpg" : basicImgPath + user.ImgId + ".jpg",
                    initials: receiverUser.Id === message.UserId ? receiverUser.Initials : user.Initials,
                    createDate: message.CreateDate,
                    text: message.Text,
                    id: message.Id
                });
            }
            return messagesData;
        }

        function initSelectedMessageModel() {
            self.selectedMessageModel = {
                isSelected: false,
                selectedClusterIndex: -1
            }
        }

        function initMessageModel() {
            self.messagesModel = {
                user: null,
                list: []
            }
        }

        function initReplyMessageModel() {
            self.replyMessageModel = {
                text: null,
                previousMessageId: -1,
                isOpen: false,
                disableButton: false
            }
        }

        function initNewAdminMessageModel() {
            self.newAdminMessageModel = {
                text: null,
                isOpen: false,
                disableButton: false
            }
        }

        function initDeleteClusterModel() {
            self.deleteClusterModel = {
                isOpen: false,
                disableButton: false
            }
        }
    }

    angular.module('portalApp').controller('messagesCtrl', ['breadcrumbService', 'apiFactory', 'loadingContentService', 'notificationService', messagesCtrl]);
})();