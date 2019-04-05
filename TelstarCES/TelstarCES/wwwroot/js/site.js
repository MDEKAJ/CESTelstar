
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

function getCityConnections(cityId, callback) {
    $.getJSON("/api/Connection/GetForCity", { "CityId": cityId }, function(response) {
        callback(response)
    })
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

function deleteConnection(connectionId, callback) {
    var connection = getConnection(connectionId, function(response) {
        if (response) {
            $.ajax({
                url: '/api/Connection',
                type: 'DELETE',
                contentType: 'application/json',
                data: JSON.stringify(response),
                dataType: 'json'
            }).done(function(response) {
                callback(response)
            });
        } else {
            callback(null)
        }
    })

};