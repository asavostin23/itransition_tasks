document.querySelector('#submitButton').addEventListener('click', async () => {
    let seed = document.querySelector('#seedInput').value;
    let errorLevel = document.querySelector('#errorLevelTextInput').value;
    let url = '/PeopleGen/' + document.querySelector('#selectRegion').value + '?seed=' + seed + '&errorLevel=' + errorLevel + '&';
    let firstPageResponse = await fetch(url + 'page=1');
    let secondPageResponse = await fetch(url + 'page=2');
    if (firstPageResponse.ok && secondPageResponse.ok) {
        let people = await firstPageResponse.json();
        people = people.concat(await secondPageResponse.json());
        let htmlString = '';
        let i = 1;
        for (let person of people) {
            htmlString += '<tr><td>' + (i++).toString() + '</td><td>' + person.uniqueId + '</td><td>' + person.surname + '</td><td>' + person.name + '</td><td>' + person.patronymic + '</td><td>' + person.adress + '</td><td>' + person.phone + '</td></tr>';
        }
        document.querySelector('table tbody').innerHTML = htmlString;
    }
    window.addEventListener("scroll", throttle(checkPosition, 350));
    window.addEventListener("resize", throttle(checkPosition, 350));
});

async function checkPosition() {
    const height = document.body.offsetHeight;
    const screenHeight = window.innerHeight;
    const scrolled = window.scrollY;
    const threshold = height - screenHeight / 4;
    const position = scrolled + screenHeight;
    if (position >= threshold) {
        await loadPage(nextPage++);
    }
}
function throttle(callee, timeout) {
    let timer = null;
    return function perform(...args) {
        if (timer) return
        timer = setTimeout(() => {
            callee(...args);
            clearTimeout(timer);
            timer = null;
        }, timeout);
    }
}
async function loadPage(page) {
    if (isLoading) return;
    isLoading = true;
    let seed = document.querySelector('#seedInput').value;
    let errorLevel = document.querySelector('#errorLevelTextInput').value;
    let url = '/PeopleGen/' + document.querySelector('#selectRegion').value + '?seed=' + seed + '&page=' + page + '&errorLevel=' + errorLevel;
    let response = await fetch(url);
    if (response.ok) {
        let people = await response.json();
        let htmlString = '';
        let i = Number(document.querySelector('table tbody').lastChild.firstChild.innerHTML) + 1;
        for (let person of people) {
            htmlString += '<tr><td>' + (i++).toString() + '</td><td>' + person.uniqueId + '</td><td>' + person.surname + '</td><td>' + person.name + '</td><td>' + person.patronymic + '</td><td>' + person.adress + '</td><td>' + person.phone + '</td></tr>';
        }
        document.querySelector('table tbody').innerHTML += htmlString;
    }
    isLoading = false;
}

let nextPage = 1;
let isLoading = false;