var sordid = function (my) {
    my.characterSkills = {};

    my.characterSkills.initDraggables = function () {
        $('#section-skills .skill').draggable({
            containment: '#section-skills',
            stack: '#section-skills .skill',
            revert: 'invalid'
        });
    };

    $(document).ready(function () {
        my.characterSkills.initDraggables();

        // TODO: Bug: dragging and dropping within same area should snap back with no result, but it doesn't

        $('#section-skills .skillDropArea').droppable({
            activeClass: 'dropActive',
            hoverClass: 'dropHover',
            drop: function (event, ui) {
                // Set the new rank value on the viewModel, and KO binding takes care of the rest...

                var draggedElem = $(ui.draggable);
                var viewModelItem = ko.dataFor(draggedElem.get(0));
                var dropTarget = $(this);
                var targetRank = dropTarget.data('rank');
                viewModelItem.Rank(targetRank);
            }
        });
    });

    return my;
}(sordid || {});




