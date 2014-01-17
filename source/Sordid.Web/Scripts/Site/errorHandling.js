define('sordid-errors', ['jquery','sordid-alerts'], function ($, alerts) {
    var alertError = function (jqXHR, textStatus, thrownError, message) {
        var errorText = JSON.parse(jqXHR.responseText).message;
        if (!message)
            message = 'Error talking to server!';
        alerts.error('<strong>' + message + '</strong><br/>Message: ' + errorText, false);
    };

    // Global error handler.  Can be overridden by individual jQuery.ajax calls.
    $.ajaxSetup({
        error: function (jqXHR, textStatus, thrownError) {
            alertError(jqXHR, textStatus, thrownError);
        }
    });

    return {
        alertError : alertError
    };
});
