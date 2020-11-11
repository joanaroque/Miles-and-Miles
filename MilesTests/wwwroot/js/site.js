﻿'use strict'

var connection = new signalR.HubConnectionBuilder().withUrl("/notificationhub").build();

start();

connection.on("dbchangenotification", function () {
    console.log("You have stuff to do");
})

async function start() {
    try {
        await connection.start();
        console.log("connected");
    } catch (err) {
        console.log(err);
        setTimeout(() => start(), 5000);
    }
};


/**
 * Calls the action:Edit on the target item.
 * The action belongs to UserController.
 * Target item is stored in html identifier:
 * 
 * data-value
 * */
function openPartial() {
    const id = event.currentTarget.getAttribute("data-value");
    const action = event.currentTarget.getAttribute("action");

    $('#form_container').load(getPath(document.location.pathname) + '/' + action + '?id=' + id);
    window.location.hash = "#form_container";
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


function getPartial(container, action, controller, id) {
    $(container).load('/' + controller + '/' + action + '?id=' + id);
}


//function loadFlightCombo(sourceEl, destEl) {
//    $(destEl).empty();
    
//    $.ajax({
//        type: "POST",
//        url: "/User/GetFlights",
//        data: { partnerId: $(sourceEl).val() },
//        dataType: "json",
//        success: function (result) {
//            $.each(result, function (i, flight) {
//                $(destEl).append('<option value="'
//                    + flight.id + '">'
//                    + flight.Origin + "->" + flight.destination
//                    + " " + flight.departureDate
//                    + '</option>');
//            })
//        },
//        error: function (err) {
//            console.log(err);
//        }
//    });
//}

/**
 * 
 * */
window.addEventListener("load", function () {
    let routeAction = "";
    let routeHref = "";
    let routeId = "";
    $("a[id*=btnDeleteItem]").click(function () {
        routeAction = event.currentTarget.getAttribute("action");
        routeHref = event.currentTarget.getAttribute("controller");
        routeId = event.currentTarget.getAttribute("data-id");
        $('#deleteDialog').modal("show");
        return false;
    });
    $("#btnNoDelete").click(function () {
        $("#deleteDialog").modal('hide');
        return false;
    });
    $("#btnYesDelete").click(function () {
        window.location.href = '/' + routeHref + '/' + routeAction + '/' + routeId;
    });
})


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

$(function () {
    $(".index-table").DataTable({
        responsive: true,
        autoWidth: false,
        paging: false,
        info: false
    });
});