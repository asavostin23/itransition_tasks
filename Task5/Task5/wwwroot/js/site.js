function switchPage(page) {
    let pages = document.querySelectorAll('.page');
    pages.forEach(page => page.style.display = 'none');
    document.querySelector('.' + page).style.display = "initial";
}

function autocomplete(inp, arr) {
    var currentFocus;
    inp.addEventListener("input", function (e) {
        var a, b, i, val = this.value;
        closeAllLists();
        if (!val) { return false; }
        currentFocus = -1;
        a = document.createElement("DIV");
        a.setAttribute("id", this.id + "autocomplete-list");
        a.setAttribute("class", "autocomplete-items");
        this.parentNode.appendChild(a);
        for (i = 0; i < arr.length; i++) {
            if (arr[i].substr(0, val.length).toUpperCase() == val.toUpperCase()) {
                b = document.createElement("DIV");
                b.innerHTML = "<strong>" + arr[i].substr(0, val.length) + "</strong>";
                b.innerHTML += arr[i].substr(val.length);
                b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
                b.addEventListener("click", function (e) {
                    inp.value = this.getElementsByTagName("input")[0].value;
                    closeAllLists();
                });
                a.appendChild(b);
            }
        }
    });
    inp.addEventListener("keydown", function (e) {
        var x = document.getElementById(this.id + "autocomplete-list");
        if (x) x = x.getElementsByTagName("div");
        if (e.keyCode == 40) {
            currentFocus++;
            addActive(x);
        } else if (e.keyCode == 38) {
            currentFocus--;
            addActive(x);
        } else if (e.keyCode == 13) {
            e.preventDefault();
            if (currentFocus > -1) {
                if (x) x[currentFocus].click();
            }
        }
    });
    function addActive(x) {
        if (!x) return false;
        removeActive(x);
        if (currentFocus >= x.length) currentFocus = 0;
        if (currentFocus < 0) currentFocus = (x.length - 1);
        x[currentFocus].classList.add("autocomplete-active");
    }
    function removeActive(x) {
        for (var i = 0; i < x.length; i++) {
            x[i].classList.remove("autocomplete-active");
        }
    }
    function closeAllLists(elmnt) {
        var x = document.getElementsByClassName("autocomplete-items");
        for (var i = 0; i < x.length; i++) {
            if (elmnt != x[i] && elmnt != inp) {
                x[i].parentNode.removeChild(x[i]);
            }
        }
    }
    document.addEventListener("click", function (e) {
        closeAllLists(e.target);
    });
}

async function getMessages(name) {
    let url = '/messages/' + name;
    let response = await fetch(url);
    if (response.ok) {
        let messages = await response.json();
        let messagesTable = document.querySelector('#messagesTable');
        messagesTable.innerHTML = '<tbody>';
        for (let message of messages) {
            messagesTable.innerHTML += '<tr><td>' + message.sender + '</td><td> <button type="button" class="btn-no-style" data-toggle="popover" title="' + message.body + '">' + message.title + '</button></td><td>' + message.createdDate + '</td></tr>';
        }
        messagesTable.innerHTML += '</tbody>';
    }
    $(function () {
        $('[data-toggle="popover"]').popover()
    });
}
async function setAutocompleteData() {
    let url = '/autocomplete';
    let response = await fetch(url);
    if (response.ok) {
        let users = await response.json();
        autocomplete(document.querySelector('#inputReciever'), users);
    }
}
async function sendMessage() {
    let message = {
        reciever: document.querySelector('#inputReciever').value,
        title: document.querySelector('#inputTitle').value,
        body: document.querySelector('#inputBody').value,
        sender: document.querySelector('#userName').value
    };
    await fetch('/Messages', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;'
        },
        body: JSON.stringify(message)
    });
}
if (document.querySelector('#userName') != null) {
    getMessages(document.querySelector('#userName').value);
    setInterval(() => getMessages(document.querySelector('#userName').value), 5000);
    setAutocompleteData();
}

