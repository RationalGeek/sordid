define('sordid-characterPowers', ['jquery', 'knockout'], function ($, ko) {
    $(document).ready(function () {
        $('#powersAddButton').click(function () {
            alert('Add button clicked!');
        });
    });

    ko.bindingHandlers.powerEdit = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            $(element).click(function () {
                alert('Edit button clicked!');
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
