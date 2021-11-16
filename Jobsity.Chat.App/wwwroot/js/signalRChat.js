"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatter").build();
document.getElementById("sendButton").disabled = true;

connection.on("receive", function (message) {
    let container = document.createElement("div");
    container.className = "container";

    let outterRow = document.createElement("div");
    outterRow.className = "row";

    let paintedDiv = document.createElement("div");
    paintedDiv.className = "col-5 message " + (message.userID === loggedUserId ? "mine offset-7" : "")

    let messageText = document.createElement("p");
    messageText.textContent = message.textMessage;

    let innerRow = document.createElement("div");
    innerRow.className = "row";

    let sender = document.createElement("div");
    sender.className = "col-6 sender";
    sender.textContent = message.userName;

    let time = document.createElement("div");
    time.className = "col-6 time";
    let date = new Date(message.createTime);
    let hour = date.getHours();
    let min = date.getMinutes();
    let seconds = date.getSeconds();
    time.textContent = date.toLocaleDateString() + " " + `${hour}:${min}:${seconds}`;

    innerRow.appendChild(sender);
    innerRow.appendChild(time);

    paintedDiv.appendChild(messageText);
    paintedDiv.appendChild(innerRow);

    outterRow.appendChild(paintedDiv);

    container.appendChild(outterRow);

    let messagesList = document.getElementById("messagesList");
    messagesList.appendChild(container);
    scrollToLastMessages();
    if (messagesList.children.length > 50) {
        let first = messagesList.children[0];
        messagesList.remove(first);
    }
});



function sendMessage() {
    var message = document.getElementById("messageInput").value;
    var chatMessage = {
        TextMessage: message,
        userId: loggedUserId,
        userName: loggedUserName
    };
    connection.invoke("SendAll", chatMessage).catch(function (err) {
        return console.error(err.toString());
    });
}

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    scrollToLastMessages();

}).catch(function (err) {
    return console.error(err.toString());
});



function scrollToLastMessages() {
    let messagesList = document.getElementById("messagesList");
    messagesList.scrollTop = messagesList.scrollHeight;
}

document.querySelector("#messageInput").addEventListener('keypress', function (e) {
    if (e.key == 'Enter') {
        sendMessage();
        document.querySelector("#messageInput").value = "";
        e.preventDefault();
    }
})

document.getElementById("sendButton").addEventListener("click", function (event) {
    sendMessage();
    event.preventDefault();
});