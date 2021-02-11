// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    // On load, empty the text fields
    $('.register-input').val('');

    // Navigation toggle for each item in the sidebar with a dropdown list
    $('.event-link').click(function () {
        $(this).next().toggle();
    });
    $('.manager-link').click(function () {
        $(this).next().toggle();
    });
    $('.attendee-link').click(function () {
        $(this).next().toggle();
    });

    // Alert the user they have registered for the event
    $('#registerBtn').click(function () {
        window.alert("You have registered for this event!");
    });

});