define('sordid-alerts', ['jquery'], function ($) {
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

    var success = function (message, fade) {
        appendAlert(message, 'success', fade);
    };

    var error = function (message, fade) {
        appendAlert(message, 'danger', fade);
    };

    var warning = function (message, fade) {
        appendAlert(message, 'warning', fade);
    };

    var info = function (message, fade) {
        appendAlert(message, 'info', fade);
    };

    return {
        success: success,
        error: error,
        warning: warning,
        info: info
    };
});