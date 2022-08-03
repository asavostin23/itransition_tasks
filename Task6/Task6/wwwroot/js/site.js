document.querySelector('#submitButton').addEventListener('click', async () => {
    nextPage = 3;
    isLoading = true;
    let seed = document.querySelector('#seedInput').value;
    let errorLevel = document.querySelector('#errorLevelTextInput').value;
    let url = '/PeopleGen/' + document.querySelector('#selectRegion').value + '?seed=' + seed + '&errorLevel=' + errorLevel + '&';
    let firstPageFetch = fetch(url + 'page=1');
    let secondPageFetch = fetch(url + 'page=2');
    let firstPageResponse = await firstPageFetch;
    let secondPageResponse = await secondPageFetch;
    if (firstPageResponse.ok && secondPageResponse.ok) {
        let people = await firstPageResponse.json();
        people = people.concat(await secondPageResponse.json());
        let htmlString = '';
        let i = 1;
        for (let person of people) {
            htmlString += '<tr><td>' + (i++).toString() + '</td><td>' + person.uniqueId + '</td><td>' + person.surname + ' ' + person.name + ' ' + person.patronymic + '</td><td>' + person.adress + '</td><td>' + person.phone + '</td></tr>';
        }
        document.querySelector('table tbody').innerHTML = htmlString;
    }
    document.querySelector('#csvExportButton').disabled = false;
    isLoading = false;
});

async function checkPosition() {
    const height = document.body.offsetHeight;
    const screenHeight = window.innerHeight;
    const scrolled = window.scrollY;
    const threshold = height - screenHeight / 4;
    const position = scrolled + screenHeight;
    if (position >= threshold) {
        await loadPage();
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
async function loadPage() {
    if (isLoading) return;
    isLoading = true;
    let seed = document.querySelector('#seedInput').value;
    let errorLevel = document.querySelector('#errorLevelTextInput').value;
    let url = '/PeopleGen/' + document.querySelector('#selectRegion').value + '?seed=' + seed + '&page=' + nextPage + '&errorLevel=' + errorLevel;
    let response = await fetch(url);
    if (response.ok) {
        let people = await response.json();
        let htmlString = '';
        let i = Number(document.querySelector('table tbody').lastChild.firstChild.innerHTML) + 1;
        for (let person of people) {
            htmlString += '<tr><td>' + (i++).toString() + '</td><td>' + person.uniqueId + '</td><td>' + person.surname + ' ' + person.name + ' ' + person.patronymic + '</td><td>' + person.adress + '</td><td>' + person.phone + '</td></tr>';
        }
        document.querySelector('table tbody').innerHTML += htmlString;
    }
    isLoading = false;
    nextPage++;
}

document.querySelector('#csvExportButton').addEventListener('click', async () => {
    isLoading = true;
    let seed = document.querySelector('#seedInput').value;
    let errorLevel = document.querySelector('#errorLevelTextInput').value;
    let url = '/csvexport/get?region=' + document.querySelector('#selectRegion').value + '&seed=' + seed + '&lastpage=' + String(nextPage - 1) + '&errorLevel=' + errorLevel;
    window.open(url);
    isLoading = false;
});

let nextPage = 3;
let isLoading = true;
window.addEventListener("scroll", throttle(checkPosition, 350));
window.addEventListener("resize", throttle(checkPosition, 350));