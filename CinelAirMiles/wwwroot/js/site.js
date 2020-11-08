// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


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

