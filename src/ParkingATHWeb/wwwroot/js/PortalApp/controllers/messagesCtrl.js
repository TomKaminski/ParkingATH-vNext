(function () {
    'use strict';

    function messagesCtrl(breadcrumbService, apiFactory, loadingContentService, notificationService) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('messages');
        getMessagesData();

        self.selectedMessageModel = {
            isSelected: false,
            Title: null
        }

        self.lastMessagesListModel = [];

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
                    imgPath: receiverUser.Id === lastMessage.UserId ? "/images/user-avatars/" + receiverUser.ImgId + ".jpg" : "/images/user-avatars/" + user.ImgId + ".jpg"
                }
                self.lastMessagesListModel.push(itemListModel);
            }
            console.log(self.lastMessagesListModel);
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