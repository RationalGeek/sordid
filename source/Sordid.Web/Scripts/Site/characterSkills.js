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
                }
            }
        });
    });

    return my;
}(sordid || {});




