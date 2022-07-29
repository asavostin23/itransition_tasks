async function hubConnect() {
    if (document.querySelector('#userName') != null) {
        let userName = document.querySelector('#userName').value;
        const hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('/Messages')
            .build();
        hubConnection.on('ReceiveMessages', messages => {
            let messagesTable = document.querySelector('#messagesTable');
            if (messagesTable != null) {
                messagesTable.innerHTML += '<tbody>';
                for (let message of messages) {
                    messagesTable.innerHTML += '<tr><td>' + message.sender + '</td><td>' + message.title + '</td><td>' + message.createdDate + '</td></tr>';
                }
                messagesTable.innerHTML += '</tbody>';
            }
            else { alert('Новое сообщение!');}
        });
        hubConnection.on('UpdateMessages', () => {
            hubConnection.invoke('SendMessages');
        });
        hubConnection.on('Debug', text => alert(text));
        await hubConnection.start();
        await hubConnection.invoke('Register', userName);
        await hubConnection.invoke('SendMessages');
    }
}
hubConnect();