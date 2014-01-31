define('sordid-util', [], function () {
    var tryParseInt = function (val, valIfNaN) {
        var parsed = parseInt(val);
        if (isNaN(parsed))
            return valIfNaN;
        return parsed;
    };

    return {
        tryParseInt: tryParseInt
    };
});