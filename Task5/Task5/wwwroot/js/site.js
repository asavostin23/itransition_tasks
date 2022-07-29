function switchPage(page) {
    let pages = document.querySelectorAll('.page');
    pages.forEach(page => page.style.display = 'none');
    document.querySelector('.' + page).style.display = "initial";
}

async function hubConnect() {
    if (document.querySelector('#userName') != null) {
        let userName = document.querySelector('#userName').value;
        const hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('/Messages')
            .build();
        hubConnection.serverTimeoutInMilliseconds = 100000;
        hubConnection.on('ReceiveMessages', messages => {
            let messagesTable = document.querySelector('#messagesTable');
            messagesTable.innerHTML = '<tbody>';
            for (let message of messages) {
                messagesTable.innerHTML += '<tr><td>' + message.sender + '</td><td>' + message.title + '</td><td>' + message.createdDate + '</td></tr>';
            }
            messagesTable.innerHTML += '</tbody>';
            if (document.querySelector('.inbox').style.display == 'none')
                alert('Новое сообщение!');
        });
        hubConnection.on('UpdateMessages', () => {
            hubConnection.invoke('SendMessages');
        });
        await hubConnection.start();

        await hubConnection.invoke('Register', userName);
        await hubConnection.invoke('SendMessages');
    }
}
hubConnect();