define('sordid-characterPowers', ['jquery', 'knockout', 'sordid-characterManage'], function ($, ko, charMan) {
    var alreadyLoadedPowers = false;

    ko.bindingHandlers.powerDelete = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            $(element).click(function () {
                alert('Delete button clicked!');
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
                        // TODO: Clicking directly on Name text does not fire click event
                        modalContentsContainer.find('.list-item-power').click(function (item) {
                            $(item.target).toggleClass('active');
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
                var itemToAdd = {};
                itemToAdd.Id = parseInt(item.find('input[name=id]').val());
                itemToAdd.Cost = parseInt(item.find('.cost').text());
                itemToAdd.Name = item.find('.name').text();
                charMan.viewModel().Character.Powers.push(itemToAdd);
                clearSelection();
            });

            addPowerModal.modal('hide');
        });
    });
});
