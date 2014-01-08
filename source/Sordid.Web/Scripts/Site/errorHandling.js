var sordid = function (my) {
    my.errorHandling = {};

    my.errorHandling.alertError = function (jqXHR, textStatus, thrownError, message) {
        var errorText = JSON.parse(jqXHR.responseText).message;
        if (!message)
            message = 'Error talking to server!';
        sordid.alerts.error('<strong>' + message + '</strong><br/>Message: ' + errorText, false);
    };

    // Global error handler.  Can be overridden by individual jQuery.ajax calls.
    $.ajaxSetup({
        error: function (jqXHR, textStatus, thrownError) {
            my.errorHandling.alertError(jqXHR, textStatus, thrownError);
        }
    });

    return my;
}(sordid || {});