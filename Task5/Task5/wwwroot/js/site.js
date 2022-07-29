function getMessages() {
    let userName = document.querySelector('#userName').value;
    let url = '/api/GetMessages/' + userName;
    let messagesTable = document.querySelector('#messagesTable');
    if (messagesTable != null) {
        messagesTable.innerHTML += '<tbody>';
    }
    let response = await fetch(url);
    if (response.ok) {
        let jsonMessages = await response.json();
        let messages = JSON.parse(jsonMessages);
        for (let message of messages) {
            messagesTable.innerHTML += '<tr><td>' + message.sender + '</td><td>' + message.title + '</td><td>' + message.createdDate + '</td></tr>';
        }
        messagesTable.innerHTML += '</tbody>';
    } else {
        alert("Ошибка HTTP: " + response.status);
    }
}
let messagesTimer = setInterval(getMessages, 5000);