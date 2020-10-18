'use strict'

//opens the Create menus in Premium Index
function openMenu() {
    //gets the value from the button that triggered the event
    const value = event.currentTarget.getAttribute('data-value');

    //gets all children from the div form_container into an array 
    const arrayofdivs = Array.from(byID('form_container').children);

    //iterates through the array and 'hides' all the divs
    arrayofdivs.forEach(item => {
        item.classList.add('hidden');
    });

    //shows only the div that the event triggered
    byID(value).classList.remove('hidden');
}


//opens the Edit partial in Premium Index
function openPartial() {
    const id = event.currentTarget.getAttribute("data-value");

    $('#detailsPremium').load('@Url.Action("Details")', id);
}

function openPartialCreate() {
    const action = event.currentTarget.getAttribute("data-value");

    $('#detailsPremium').load('@Url.Action("'+{action}+'")');
}

function byID(elementId) {
    return document.getElementById(elementId);
}

function byClass(elementClass) {
    return document.getElementsByClassName(elementClass);
}

function addClassById(elementId, className) {
    byID(elementId).classList.add(className);
}

function removeClassById(elementId, className) {
    byID(elementId).classList.remove(className);
}

function changeCss(elementId, href) {
    byID(elementId).href = href;
}
