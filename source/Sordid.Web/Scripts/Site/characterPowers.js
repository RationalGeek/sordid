define('sordid-characterPowers', ['jquery', 'knockout'], function ($, ko) {
    $(document).ready(function () {
        $('#powersAddButton').click(function () {
            alert('Add button clicked!');
        });
    });

    ko.bindingHandlers.powerInit = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            viewModel.editMode = ko.observable(false);
        }
    };

    ko.bindingHandlers.powerEdit = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            $(element).click(function () {
                viewModel.editMode(true);
            });
        }
    };

    ko.bindingHandlers.powerCompleteEdit = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            $(element).click(function () {
                viewModel.editMode(false);
            });
        }
    };

    ko.bindingHandlers.powerDelete = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            $(element).click(function () {
                alert('Delete button clicked!');
            });
        }
    };
});
