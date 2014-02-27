$(document).ready(function () {
    var hideWarningDialog = $.cookie('hideAlphaWarning');
    if (hideWarningDialog != 1)
    {
        var warningDialog = $('#alphaWarning');
        warningDialog.show();
        warningDialog.on('closed.bs.alert', function () {
            $.cookie('hideAlphaWarning', 1);
        });
    }
});