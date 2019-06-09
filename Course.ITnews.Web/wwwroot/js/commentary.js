"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/commentaryHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveCommentary", function (user, message,authorId,newsId) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg =msg;
    var authorId = document.getElementById("authorId").value;
    var newsId = document.getElementById("newsId").value;
    document.getElementById("messageInput").value = "";
    var cardHeader = $('<div>',
        {
            class: 'card-header',
            text: user + ' says:'
        }).appendTo($('#comments'));
    var card = $('<div>',
        {
            class: 'card',
            text: encodedMsg
        }).appendTo($('#comments'));
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var authorId = document.getElementById("authorId").value;
    var newsId = document.getElementById("newsId").value;
    document.getElementById("messageInput").value = "";

    connection.invoke("SendCommentary", user, message, authorId, newsId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});