define('sordid-characterAspects', ['jquery', 'knockout'], function ($, ko) {

    // The accordion sort-of works without this, but opening one section
    // doesn't collapse the other section unless this is done

    ko.bindingHandlers.collapseAspect = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            $(element).collapse({ parent: '#aspectAccordion', toggle: false });
        }
    };

});
