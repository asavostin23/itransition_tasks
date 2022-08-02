document.querySelector('#submitButton').addEventListener('click', async () => {
    let seed = document.querySelector('#seedInput').value;
    let url = '/peoplegen/' + document.querySelector('#selectRegion').value + '?seed=' + seed + '&';
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
});