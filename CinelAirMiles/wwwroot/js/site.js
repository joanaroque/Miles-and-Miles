'use strict'


function openPartial() {
    const id = event.currentTarget.getAttribute("data-value");
    const action = event.currentTarget.getAttribute("action");

    $('#form_container').load(getPath(document.location.pathname) + '/' + action + '?id=' + id);
}

/**
 * Calls an action with the given parameters
 * The action called is retrieved from the html identifiers:
 * 
 * action
 * */
function openPartialCreate() {
    const action = event.currentTarget.getAttribute("action");

    $('#form_container').load(getPath(document.location.pathname) + '/' + action);
}

function calculatePrice() {
    let value = $(event.currentTarget).val();
    value = value * 0.05;
    $("#price").val(value);
}



//var connection = new signalR.HubConnectionBuilder().withUrl("/notificationhub").build();

//start();

//connection.on("dbchangenotification", function () {
//    console.log("You have stuff to do");
//})

//async function start() {
//    try {
//        await connection.start();
//        console.log("connected");
//    } catch (err) {
//        console.log(err);
//        setTimeout(() => start(), 5000);
//    }
//};


/**
 * Load a partial with parameters
 * @param {any} container The div that contains the form
 * @param {any} action The action to be called
 * @param {any} controller The controller associated
 * @param {any} id The id of the Item
 */
function getPartial(container, action, controller, id) {
    if (id === null) {
        $(container).load('/' + controller + '/' + action);
    } else {
        $(container).load('/' + controller + '/' + action + '?id=' + id);
    }
}


/**
 * Fast call for getElementById
 * 
 * @param {any} elementId
 */
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

function getPath(routehref) {
    return routehref.slice(routehref.indexOf('/'), routehref.lastIndexOf('/'));
}

//$(function () {
//    $(".index-table").DataTable({
//        responsive: true,
//        autoWidth: false,
//        paging: false,
//        info: false
//    });
//});