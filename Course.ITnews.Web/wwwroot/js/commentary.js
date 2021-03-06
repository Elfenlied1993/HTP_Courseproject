﻿"use strict";
var connectionComment = new signalR.HubConnectionBuilder().withUrl("/commentaryHub").build();
function deleteItem(form) {
    console.log(form);
    $(form).parents('a').remove();
}
//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connectionComment.on("ReceiveCommentary",
    function (user, message, authorId, newsId, commentId) {
        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        var encodedMsg = msg;
        document.getElementById("messageInput").value = "";
        var cardDiv = $('<a>').appendTo($('#comments'));
        var cardHeader = $('<div>',
            {
                class: 'card-header',
            }).appendTo(cardDiv);
      
        var card = $('<div>',
            {
                class: 'card',
                text: encodedMsg
            }).appendTo(cardDiv);
        var user = $('<a>',
            {
                text:user+' says',
                href: "/Users/Details?userId=" + authorId,
            }).appendTo(cardHeader);
        var deleteButton = $('<form>',
            {
                id: 'form-' + commentId,
                method: 'post',
                action: '/News/DeleteComment/' + commentId,
                'data-ajax': 'true',
                'data-ajax-success': 'deleteItem(this)'
            }).appendTo(cardDiv);

        var button = $('<button>',
            {
                type: 'submit',
                class: 'btn btn-sm btn-danger d-none d-md-inline-block',
                text: 'Delete'
            }).appendTo(deleteButton);
        var likeButton = $('<button>',
            {
                class: 'like-button',
                title: 'Click to like'
            }).appendTo(cardDiv);
        var like = $('<i>',
            {
                class: 'fa fa-thumbs-up',
            }).appendTo(likeButton);
        var likeCount = $('<span>',
            {
                id:'count-' +commentId,
                class: 'like-count',
                value:0,
                text: '0' 
            }).appendTo(likeButton);
        likeButton.click(function () {
            connectionComment.invoke("Like", authorId, commentId).catch(function (err) {
                return console.error(err.toString());
            });
        });
        button.click(function () {
            connectionComment.invoke("DeleteCommentary", commentId).catch(function (err) {
                return console.error(err.toString());
            });
        });
    });
connectionComment.on("ReceiveDelete", function (commentId) {
    var deleteForm = document.getElementById('form-' + commentId);
    deleteItem(deleteForm);
});
connectionComment.on("ReceiveLike",
    function (commentId, count) {
        var counter = document.getElementById('count-' + commentId);

        $(counter).replaceWith('<span class="like-count" id="count-'+commentId+'">' + count + '</span>');
    });
connectionComment.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});
document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var authorId = document.getElementById("authorId").value;
    var newsId = document.getElementById("newsId").value;
    var commentId = 1 + Number(document.getElementById("commentId").value);
    document.getElementById("messageInput").value = "";
    connectionComment.invoke("SendCommentary", user, message, authorId, newsId, commentId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
function removeButton(formId) {
    connectionComment.invoke("DeleteCommentary", formId).catch(function (err) {
        return console.error(err.toString());
    });
};

function likeButton(authorId, commentId) {
    connectionComment.invoke("Like", authorId, commentId).catch(function(err) {
        return console.error(err.toString());
    });
};