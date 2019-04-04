﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification

// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.


function getCities(callback) {
    $.getJSON("/api/City/Cities", null, function (response) {
        callback(response);
    });
}

function getCity(cityId, callback) {
    $.getJSON("/api/City",
        { "cityId": cityId }, function (response) {
            callback(response);
    });
}

function updateCity(city, callback) {
    $.ajax({
        url: '/api/City',
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(city),
        dataType: 'json'
    }).done(function(response) {
        callback(response);
    });
};

function addCity(cityName, callback) {
    $.ajax({
        url: '/api/City',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({
            CityName: cityName,
        }),
        dataType: 'json'
    }).done(function (response) {
        callback(response)
    });
};

function deleteCity(city, callback) {
    $.ajax({
        url: '/api/City',
        type: 'DELETE',
        contentType: 'application/json',
        data: JSON.stringify(city),
        dataType: 'json'
    }).done(function (response) {
        callback(response);
    });
};