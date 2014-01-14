var sordid = function (my) {
    my.characterSkills = {};

    ko.bindingHandlers.draggableSkill = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            $(element).draggable({
                containment: '#section-skills',
                stack: '#section-skills .skill',
                revert: 'invalid'
            });
        }
    };

    var validateSkills = function () {
        // If the total count of skills at one rank
        // are more than the count of the level below,
        // that level should be highlighted

        // Count up the skills in each rank
        var viewModel = sordid.characterManage.viewModel;
        var skills = viewModel.Character.Skills();
        var rankCounts = $.map(new Array(viewModel.ranks.length), function(n) { return 0; });
        for (var i = 0; i < skills.length; i++) {
            var rank = skills[i].Rank();
            rankCounts[rank] += 1;
        }

        // Highlight the offending sections
        var sections = $('.skillDropArea');
        sections.removeClass('skillSectionWarning');
        for (var i = rankCounts.length - 1; i >= 2; i--) {
            var currCount = rankCounts[i];
            var belowCount = rankCounts[i - 1];
            if (currCount > belowCount) {
                var section = $('#skillPanel-rank' + i);
                $(section).addClass('skillSectionWarning');
            }
        }
    };

    $(document).ready(function () {
        $('#section-skills .skillDropArea').droppable({
            activeClass: 'dropActive',
            hoverClass: 'dropHover',
            drop: function (event, ui) {
                var draggedElem = $(ui.draggable);
                var viewModelItem = ko.dataFor(draggedElem.get(0));
                var targetRank = $(this).data('rank');

                if (targetRank == viewModelItem.Rank()) {
                    // Rank didn't change.  We want the item to snap back into the list so reset its position
                    draggedElem.css({ top: 0, left: 0 });
                }
                else {
                    // Rank did change.  Update the viewModel and KO bindings takes care of the rest.
                    viewModelItem.Rank(targetRank);
                    validateSkills();
                }
            }
        });
    });

    $(document).ready(function () {
        validateSkills();
    });

    return my;
}(sordid || {});




