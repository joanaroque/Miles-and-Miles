'use strict'


/**
 * Calls the action:Edit on the target item.
 * The action belongs to UserController.
 * Target item is stored in html identifier:
 * 
 * data-value
 * */
function openPartial() {
    const id = event.currentTarget.getAttribute("data-value");

    $('#form_container').load('/User/Edit?id='+id);
}


/**
 * Calls an action with the given parameters
 * The action called is retrieved from the html identifiers:
 * 
 * controller
 * action
 * */
function openPartialCreate() {
    const action = event.currentTarget.getAttribute("action");
    const controller = event.currentTarget.getAttribute("controller");

    $('#form_container').load('/'+controller+'/'+action);
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
