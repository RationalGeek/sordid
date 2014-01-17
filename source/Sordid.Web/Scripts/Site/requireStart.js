(function () {
    var root = this;

    define('jquery', [], function () { return root.jQuery; });
    define('knockout', [], function () { return root.ko; });
})();