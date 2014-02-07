define('sordid-characterNotes', ['jquery'], function ($) {
    // http://stackoverflow.com/questions/16877259/knockout-js-binding-handler-for-wysihtml5
    ko.bindingHandlers.wysihtml5 = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            var control = $(element).wysihtml5({
                "events": {
                    "change": function () {
                        var observable = valueAccessor();
                        observable(control.getValue());
                    },
                "size": 'xs'
                }
            }).data("wysihtml5").editor;
        },
        update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            var content = valueAccessor();

            if (content != undefined) {
                var control = $(element).data("wysihtml5").editor;
                control.setValue(content());
            }
        }
    };
});
