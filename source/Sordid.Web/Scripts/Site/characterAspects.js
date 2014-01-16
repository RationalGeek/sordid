var sordid = function (my) {
    my.characterAspects = {};

    ko.bindingHandlers.collapseAspect = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            $(element).collapse({parent: '#aspectAccordion', toggle: false});
        }
    };

    return my;
}(sordid || {});