var sordid = function (my) {
    my.characterManage = {};

    // KO ViewModel initialization
    my.characterManage.initKnockout = function(viewModelRaw) {
        my.characterManage.viewModel = ko.mapping.fromJS(viewModelRaw);
        ko.applyBindings(my.characterManage.viewModel);
        my.characterSkills.initDraggables();
    };

    $(document).ready(function () {
        // Set show/hide behavior for character sections
        $('#character-sections .btn').click(function (e) {
            // Toggle the button activeness
            var allButtons = $('#character-sections .btn');
            allButtons.removeClass('active');
            $(e.target).addClass('active');

            // Show the section
            var sections = $('.char-section');
            sections.addClass('hidden');
            $(e.target.hash).removeClass('hidden');
        });

        // Save behavior
        $('#saveButton').click(function () {
            $('#saveButton').addClass('disabled');
            $('#saveIcon').addClass('hidden');
            $('#pendingSaveIcon').removeClass('hidden');

            var viewModelJSON = JSON.stringify(ko.mapping.toJS(my.characterManage.viewModel));
            $.ajax({
                url: '/Character/Save',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: viewModelJSON,
                success: function (data) {
                    // Reapply viewModel bindings
                    ko.mapping.fromJS(data, {}, my.characterManage.viewModel);
                    my.characterSkills.initDraggables();

                    // Pop a saved message
                    sordid.alerts.success('<strong>Saved!</strong>', true);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    sordid.errorHandling.alertError(jqXHR, textStatus, errorThrown, 'Failed to save!');
                },
                complete: function () {
                    $('#saveButton').removeClass('disabled');
                    $('#saveIcon').removeClass('hidden');
                    $('#pendingSaveIcon').addClass('hidden');
                }
            });
        });
    });

    return my;
}(sordid || {});




