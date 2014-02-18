define('sordid-characterManage', ['jquery', 'knockout', 'sordid-alerts', 'sordid-errors', 'sordid-koDirty'], function ($, ko, alerts, errors) {
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

    var viewModel = {};

    function computeHighConcept() {
        var aspects = viewModel.Character.Aspects();
        for (var index in aspects) {
            var aspect = aspects[index];
            var label = aspect.Aspect.HeadingLabel();
            if (label == 'High Concept')
                return aspect.Name();
        }
        return '';
    }

    // KO ViewModel initialization
    var initKnockout = function (viewModelRaw) {
        viewModel = ko.mapping.fromJS(viewModelRaw);
        viewModel.ranks = buildRanks();
        viewModel.dirty = ko.dirtyFlag(viewModel);
        viewModel.Character.HighConcept = ko.computed(computeHighConcept);
        $(document).trigger('sordid.ko.viewModelInit', viewModel);
        $(document).trigger('sordid.ko.viewModelChanged', viewModel);
        ko.applyBindings(viewModel);
    };

    $(document).ready(function () {
        // ************************************************
        // Show / hide behavior for character sections
        // ************************************************
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

            // Update the selector text (for extra small layout) to the name of the selected section
            $('#character-sections-small-label').text($(targetButton).text());
        }

        $('#character-sections .btn, #character-sections-small .dropdown-menu a').click(function (e) {
            toggleSection(e.target.hash);
        });

        // Show the correct tab if there is a hash in the URL
        $(document).ready(function () {
            var hash = "#section-basics"; // Default
            if (location.hash.length > 0) {
                hash = location.hash;
            }
            toggleSection(hash);
        });

        // ************************************************
        // Save behavior
        // ************************************************
        $('#saveButton').click(function () {
            $('#saveButton').addClass('disabled');
            $('#saveIcon').addClass('hidden');
            $('#pendingSaveIcon').removeClass('hidden');

            var viewModelJSON = JSON.stringify(ko.mapping.toJS(viewModel));
            $.ajax({
                url: '/Character/Save',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: viewModelJSON,
                success: function (data) {
                    // Reapply viewModel bindings
                    ko.mapping.fromJS(data, {}, viewModel);
                    $(document).trigger('sordid.ko.viewModelChanged', viewModel);

                    // Pop a saved message
                    alerts.success('<strong>Saved!</strong>', true);

                    // Reset dirty flag
                    viewModel.dirty.reset();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    errors.alertError(jqXHR, textStatus, errorThrown, 'Failed to save!');
                },
                complete: function () {
                    $('#saveButton').removeClass('disabled');
                    $('#saveIcon').removeClass('hidden');
                    $('#pendingSaveIcon').addClass('hidden');
                }
            });
        });

        // ************************************************
        // Delete behavior
        // ************************************************
        $('#confirmDeleteButton').click(function () {
            $('#deleteForm').submit();
        });

        // ************************************************
        // Print behavior
        // ************************************************
        function showPrintWindow() {
            var charId = viewModel.Character.Id();
            var url = '../Print/' + charId;
            window.open(url);
        }

        $('#printButton').click(function () {
            // If the charater is dirty, make sure they really want to print
            if (viewModel.dirty.isDirty()) {
                $('#printModal').modal('show');
            }
            else {
                showPrintWindow();
            }
        });

        $('#confirmPrintButton').click(function () {
            $('#printModal').modal('hide');
            showPrintWindow();
        });

        // ************************************************
        // Dirty warning
        // ************************************************
        $(window).bind('beforeunload', function () {
            if (viewModel.dirty.isDirty()) {
                return 'You have unsaved changed. Are you sure you want to leave?';
            }
        });
    });

    // Test alerts
    //$(document).ready(function () {
    //    alerts.error('Test error');
    //    alerts.warning('Test warning');
    //    alerts.info('Test info');
    //    alerts.success('Test success');
    //});

    return {
        initKnockout: initKnockout,
        viewModel: function () { return viewModel; }
    };
});

