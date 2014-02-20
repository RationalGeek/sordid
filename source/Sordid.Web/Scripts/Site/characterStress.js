define('sordid-characterStress', ['jquery', 'knockout', 'sordid-characterManage'], function ($, ko, charMan) {
    $(document).on('sordid.ko.viewModelBound', function (event, viewModel) {
        $('.stressBubbleChooser').bubbleChooser(8);
    });

    ko.bindingHandlers.stressDelete = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            $(element).click(function () {
                var itemToDelete = ko.dataFor(element);
                charMan.viewModel().Character.Consequences.remove(itemToDelete);
            });
        }
    };

    $(document).ready(function () {
        $('#addConsequenceButton').click(function () {
            var newCons = {
                Id:           ko.observable(0),
                Type:         ko.observable(''),
                StressType:   ko.observable(''),
                StressAmount: ko.observable(-2),
                UserCreated:  ko.observable(true)
            };
            charMan.viewModel().Character.Consequences.push(newCons);
        });
    });
});
