

var maps = [];
var markers = [];

function initMap() {
    var $maps = $('.map');
    $.each($maps, function (i, value) {
        var uluru = { lat: parseFloat($(value).attr('lat')), lng: parseFloat($(value).attr('lng')) };

        var mapDivId = $(value).attr('id');

        maps[mapDivId] = new google.maps.Map(document.getElementById(mapDivId), {
            zoom: 15,
            center: uluru
        });

        markers[mapDivId] = new google.maps.Marker({
            position: uluru,
            map: maps[mapDivId]
        });
    })
}



//Klicka utanför forms för att gömma Divarna
$('body').click(function () {
    if ($("#pickWrap").hasClass('active')) {
        $('.pickWrap').toggleClass('active');
    }
    if ($("#marketWrap").hasClass('active')) {
        $('.marketWrap, a').toggleClass('active');
    }
});

$("#pickWrap").click(function (e) {
    //e.stopImmediatePropagation();
    e.stopPropagation();
});

$("#marketWrap").click(function (e) {
    //e.stopImmediatePropagation();
    e.stopPropagation();
});

$('.marketItem-btn').on('click', function () {
    $('.marketWrap, a').toggleClass('active');

    return false;
});

$('#btn2').on('click', function () {
    $('.marketWrap, a').toggleClass('active');

    return false;
});

$('.pickItem-btn').on('click', function () {
    $('.pickWrap, a').toggleClass('active');

    return false;
});

$('#btn').on('click', function () {
    $('.pickWrap, a').toggleClass('active');

    return false;
});


function initAutocomplete() {

    var marketMap = new google.maps.Map(document.getElementById('pickMap'), {
        center: { lat: 59.3293, lng: 18.0686 },
        zoom: 13,
        mapTypeId: 'roadmap'
    });

    // Create the search box and link it to the UI element.
    var input = document.getElementById('pac-input');
    var searchBox = new google.maps.places.SearchBox(input);
    marketMap.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

    // Bias the SearchBox results towards current map's viewport.
    marketMap.addListener('bounds_changed', function () {
        searchBox.setBounds(marketMap.getBounds());
    });

    var markers = [];
    // Listen for the event fired when the user selects a prediction and retrieve
    // more details for that place.
    searchBox.addListener('places_changed', function () {
        var places = searchBox.getPlaces();

        if (places.length === 0) {
            return;
        }

        // Clear out the old markers.
        markers.forEach(function (marker) {
            marker.setMap(null);
        });
        markers = [];

        // For each place, get the icon, name and location.
        var bounds = new google.maps.LatLngBounds();
        places.forEach(function (place) {
            if (!place.geometry) {
                console.log("Returned place contains no geometry");
                return;
            }
            var icon = {
                url: place.icon,
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(25, 25)
            };

            // Create a marker for each place.
            markers.push(new google.maps.Marker({
                map: marketMap,
                icon: icon,
                title: place.name,
                position: place.geometry.location
            }));

            //(Cristian) sätter valda kordinater i form latLng
            $('#latLngForm').val(place.geometry.location);

            //alert("Market"+place.geometry.location);

            if (place.geometry.viewport) {
                // Only geocodes have viewport.
                bounds.union(place.geometry.viewport);
            } else {
                bounds.extend(place.geometry.location);
            }
        });
        marketMap.fitBounds(bounds);
    });
}