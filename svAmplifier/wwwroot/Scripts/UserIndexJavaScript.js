

var maps = [];
var markers = [];

function initMap() {
    var $maps = $('.map');
    $.each($maps, function (i, value) {
        var uluru = { lat: parseFloat($(value).attr('lat')), lng: parseFloat($(value).attr('lng')) };

        var mapDivId = $(value).attr('id');

        maps[mapDivId] = new google.maps.Map(document.getElementById(mapDivId), {
            zoom: 10,
            center: uluru
        });

        markers[mapDivId] = new google.maps.Marker({
            position: uluru,
            map: maps[mapDivId]
        });
    })
}
