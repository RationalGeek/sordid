define('sordid-characterPowers', ['jquery', 'knockout', 'sordid-characterManage', 'sordid-util'], function ($, ko, charMan, util) {
    var alreadyLoadedPowers = false;

    ko.bindingHandlers.powerDelete = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            $(element).click(function () {
                var itemToDelete = ko.dataFor(element);
                charMan.viewModel().Character.Powers.remove(itemToDelete);
            });
        }
    };

    ko.bindingHandlers.powerGlyph = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            var typeName = valueAccessor().TypeName();
            var glyphName = '';
            if (typeName == 'Stock') {
                glyphName = 'glyphicon-book';
            }
            if (typeName == 'Custom') {
                glyphName = 'glyphicon-cog';
            }
            if (glyphName != '') {
                $(element).addClass(glyphName);
            }
        }
    };

    $(document).ready(function () {
        var addStockPowerModal = $('#addStockPowerModal');
        var addCustomPowerModal = $('#addCustomPowerModal');
        var customPowerNameInput = addCustomPowerModal.find('#powerName');
        var customPowerCostInput = addCustomPowerModal.find('#powerCost');
        var loadingContainer = addStockPowerModal.find('.loadingContainer');
        var modalContentsContainer = addStockPowerModal.find('.modalContentsContainer');

        var clearSelection = function () {
            addStockPowerModal.find('.list-item-power').removeClass('active');
        };

        addStockPowerModal.on('show.bs.modal', function (e) {
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

        addCustomPowerModal.on('show.bs.modal', function (e) {
            customPowerNameInput.val('');
            customPowerCostInput.val(0);
        });

        var createPowerViewModel = function (id, name, cost, typeName) {
            var power = { Power: {} };
            power.Power.Id = ko.observable(id);
            power.Power.Name = ko.observable(name);
            power.Power.Cost = ko.observable(cost);
            power.Power.TypeName = ko.observable(typeName);
            return power;
        };

        $('#addStockPowerModalAddButton').click(function () {
            // Get everything that is selected
            var selectedItems = addStockPowerModal.find('.list-item-power.active');
            selectedItems.each(function () {
                var item = $(this);
                var itemToAdd = createPowerViewModel(
                    parseInt(item.find('input[name=id]').val()),
                    item.find('.name').text(),
                    parseInt(item.find('.cost').text()),
                    'Stock'
                    );
                charMan.viewModel().Character.Powers.push(itemToAdd);
                clearSelection();
            });

            addStockPowerModal.modal('hide');
        });

        $('#addCustomPowerModalAddButton').click(function () {
            var itemToAdd = createPowerViewModel(
                0,
                customPowerNameInput.val(),
                util.tryParseInt(customPowerCostInput.val(), 0),
                'Custom'
                );
            charMan.viewModel().Character.Powers.push(itemToAdd);
            addCustomPowerModal.modal('hide');
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
