// http://www.adequatelygood.com/JavaScript-Module-Pattern-In-Depth.html

var sordid = function (my) {
    var fadeTimeout = 5000;
    var container = $('#alertContainer');

    var appendAlert = function (message, type, fade) {
        var html = '<div class="alert fade in alert-' + type + '">' +
                        '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' +
                        message +
                    '</div>';
        var newElement = $(html).appendTo(container);

        if (fade) {
            window.setTimeout(function () {
                $(newElement).alert().alert('close');
            }, fadeTimeout);
        };
    };

    my.alerts = {};

    my.alerts.success = function (message, fade) {
        appendAlert(message, 'success', fade);
    };

    my.alerts.error = function (message, fade) {
        appendAlert(message, 'danger', fade);
    };

    my.alerts.warning = function (message, fade) {
        appendAlert(message, 'warning', fade);
    };

    my.alerts.info = function (message, fade) {
        appendAlert(message, 'info', fade);
    };

    return my;
}(sordid || {});