var sordid = function (my) {
    my.characterManage = {};

    var buildRanks = function () {
        return [
            { name: 'Superb (+5)', value: 5 },
            { name: 'Great (+4)', value: 4 },
            { name: 'Good (+3)', value: 3 },
            { name: 'Fair (+2)', value: 2 },
            { name: 'Average (+1)', value: 1 },
            { name: 'Mediocre (+0)', value: 0 }
        ];
    };

    // KO ViewModel initialization
    my.characterManage.initKnockout = function(viewModelRaw) {
        my.characterManage.viewModel = ko.mapping.fromJS(viewModelRaw);
        my.characterManage.viewModel.ranks = buildRanks();
        ko.applyBindings(my.characterManage.viewModel);
    };

    $(document).ready(function () {
        // Show / hide behavior for character sections
        var toggleSection = function (secId) {
            // Find the button with that section ID
            var allButtons = $('#character-sections .btn');
            var targetButton = $.grep(allButtons, function (b) { return b.hash == secId })[0];

            // Toggle the button activeness
            allButtons.removeClass('active');
            $(targetButton).addClass('active');

            // Show the section
            var sections = $('.char-section');
            sections.addClass('hidden');
            $(secId).removeClass('hidden');
        }

        $('#character-sections .btn').click(function (e) {
            toggleSection(e.target.hash);
        });

        // Save behavior
        $('#saveButton').click(function () {
            // TODO: Need dirty behavior to not save if not necessary, and prevent navigation if dirty

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

        $(document).ready(function () {
            if (location.hash.length > 0) {
                toggleSection(location.hash);
            }
        });
    });

    return my;
}(sordid || {});




