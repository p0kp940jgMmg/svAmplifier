
$(function () {
    $('#map').vectorMap({
        map: 'se_merc', onRegionClick: function (event, code) {
            let url = "@Url.Action(nameof(UserController.Market), "User")";
            window.location.href = url + "/" + code;
        }
    });
});

$(document).ready(function () {

    $('')
});