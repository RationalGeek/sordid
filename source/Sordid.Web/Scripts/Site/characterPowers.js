define('sordid-characterPowers', ['jquery', 'knockout', 'sordid-characterManage'], function ($, ko, charMan) {
    var alreadyLoadedPowers = false;

    ko.bindingHandlers.powerDelete = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            $(element).click(function () {
                var itemToDelete = ko.dataFor(element);
                charMan.viewModel().Character.Powers.remove(itemToDelete);
            });
        }
    };

    $(document).ready(function () {
        var addPowerModal = $('#addPowerModal');
        var loadingContainer = addPowerModal.find('.loadingContainer');
        var modalContentsContainer = addPowerModal.find('.modalContentsContainer');

        var clearSelection = function () {
            $('#addPowerModal .list-item-power').removeClass('active');
        };

        addPowerModal.on('show.bs.modal', function (e) {
            if (!alreadyLoadedPowers) {
                // Begin ajax request for dialog content
                $.ajax({
                    url: addPowerDialogUrl,
                    type: 'GET',
                    success: function (data) {
                        // Hide the loading spinner
                        loadingContainer.hide();

                        // Insert resulting HTML into the DOM, and show it
                        modalContentsContainer.html(data);
                        modalContentsContainer.show();

                        // Link up selection behavior on list items
                        modalContentsContainer.find('li.list-item-power').click(function (item) {
                            $(this).toggleClass('active');
                        });
                    },
                });

                alreadyLoadedPowers = true;
            }
            clearSelection();
        });

        $('#addPowerModalAddButton').click(function () {
            // Get everything that is selected
            var selectedItems = $('#addPowerModal .list-item-power.active');
            selectedItems.each(function () {
                var item = $(this);
                var itemToAdd = { Power: {} };
                itemToAdd.Power.Id = ko.observable(parseInt(item.find('input[name=id]').val()));
                itemToAdd.Power.Cost = ko.observable(parseInt(item.find('.cost').text()));
                itemToAdd.Power.Name = ko.observable(item.find('.name').text());
                charMan.viewModel().Character.Powers.push(itemToAdd);
                clearSelection();
            });

            addPowerModal.modal('hide');
        });
    });

    $(document).on('sordid.ko.viewModelInit', function (event, viewModel) {
        // Adds a sorted computed to the viewModel so that the UI
        // can be bound to that instead, which ensures the list remains
        // sorted when displayed to user
        viewModel.Character.PowersSorted = ko.computed(function () {
            return viewModel.Character.Powers().sort(function (a, b) {
                var aName = a.Power.Name();
                var bName = b.Power.Name();

                return aName === bName ? 0 :
                    aName > bName ? 1 : -1;
            });
        });

        viewModel.Character.TotalPowerCost = ko.computed(function () {
            var sum = 0;
            for (var i = 0; i < viewModel.Character.Powers().length; i++) {
                sum += viewModel.Character.Powers()[i].Power.Cost();
            }
            return sum;
        });
    });
});
