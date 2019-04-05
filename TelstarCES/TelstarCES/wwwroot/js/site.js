// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification

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

function getConnections(callback) {
    $.getJSON("/api/Connection/Connections", null, function (response) {
            callback(response);
        });
}

function getConnection(connectionId, callback) {
    $.getJSON("/api/connection",
        { "ConnectionId": connectionId }, function (response) {
            callback(response);
        });
}

function addConnection(connection, callback) {
    $.ajax({
        url: '/api/Connection',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(connection),
        dataType: 'json'
    }).done(function (response) {
        callback(response)
    });
};

function updateConnection(connection, callback) {
    $.ajax({
        url: '/api/Connection',
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(connection),
        dataType: 'json'
    }).done(function (response) {
        callback(response)
    });
};

function deleteConnection(connection, callback) {
    $.ajax({
        url: '/api/Connection',
        type: 'DELETE',
        contentType: 'application/json',
        data: JSON.stringify(connection),
        dataType: 'json'
    }).done(function (response) {
        callback(response)
    });
};

function getParcelTypes(callback) {
    $.getJSON("/api/ParcelType/ParcelTypes", null, function (response) {
        callback(response);
    });
}

function getParcelType(ParcelTypeId, callback) {
    $.getJSON("/api/ParcelType",
        { "ParcelTypeId": ParcelTypeId }, function (response) {
            callback(response);
        });
}

function addSegments(segments, orderId, callback)
{

}

function addOrder(segments,
    customerName, customerEmail, customerPhoneNumber, customerAdress1, customerAdress2, customerZipCode, customerPOBox, customerCity, customerCountry,
    orderRecommended, orderTotalPrice, orderTotalDuration, orderFromCity, orderToCity, orderWeight, parcelTypeId, callback) {
    let customer = {
        "CustomerName": customerName,
        "Email": customerEmail,
        "Number": customerPhoneNumber,
        "Address1": customerAdress1,
        "Address2": customerAdress2,
        "ZipCode": customerZipCode,
        "POBox": customerPOBox,
        "City": customerCity,
        "Country": customerCountry
    };
    let order = {
        "Recommended": orderRecommended,
        "TotalPrice": orderTotalPrice,
        "TotalDuration": orderTotalDuration,
        "FromCity": orderFromCity,
        "ToCity": orderToCity,
        "Weight": orderWeight,
        "Customer": customer,
        "ParcelTypeId": parcelTypeId,
        "Segments": segments
    };
    $.ajax({
        url: '/api/Order',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(order),
        dataType: 'json'
    }).done(function (response) {
        callback(response)
    });
}