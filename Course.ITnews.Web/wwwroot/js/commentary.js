"use strict";
var connectionComment = new signalR.HubConnectionBuilder().withUrl("/commentaryHub").build();
function deleteItem(form) {
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
                text: user + ' says:'
            }).appendTo(cardDiv);
        var card = $('<div>',
            {
                class: 'card',
                text: encodedMsg
            }).appendTo(cardDiv);
        var deleteButton = $('<form>',
            {
                id:'form-'+commentId,
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
        button.click(function () {
            connectionComment.invoke("DeleteCommentary", commentId).catch(function (err) {
                return console.error(err.toString());
            });
        });
    });
connectionComment.on("ReceiveDelete", function(commentId) {
    var deleteForm = document.getElementById('form-' + commentId);
    console.log(deleteForm);
    deleteItem(deleteForm);
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
}