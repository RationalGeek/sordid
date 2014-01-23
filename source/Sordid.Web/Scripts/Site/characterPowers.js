define('sordid-characterPowers', ['jquery', 'knockout'], function ($, ko) {
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

        //addPowerModal.on('hidden.bs.modal', function (e) {
        //    alert('modal has hidden!');
        //});

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
                    },
                });

                alreadyLoadedPowers = true;
            }
        });

        $('#addPowerModalAddButton').click(function () {
            // TODO: Respond to add being clicked

            addPowerModal.modal('hide');
        });
    });
});
