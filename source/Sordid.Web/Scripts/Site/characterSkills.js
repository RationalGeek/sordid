﻿var sordid = function (my) {
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

        $('#section-skills .skillDropArea').droppable({
            activeClass: 'dropActive',
            hoverClass: 'dropHover',
            drop: function (event, ui) {
                var draggedElem = $(ui.draggable);
                var dropTarget = $(this);

                // Remove the dragged element from the original parent
                // and put it in the new element it was dropped to
                // Then reset the top and left so that it snaps in where
                // it is supposed to
                draggedElem.remove();
                dropTarget.append(draggedElem);
                draggedElem.css({ left: 0, top: 0 });

                // Keep the list alphabetized
                var sortedList = dropTarget.children().sort(function (a, b) {
                    var vA = a.innerText;
                    var vB = b.innerText;
                    return (vA < vB) ? -1 : (vA > vB) ? 1 : 0;
                });
                dropTarget.append(sortedList);

                // All this mucking about makes them no longer draggable, so redo it
                my.characterSkills.initDraggables();

                // Update the KO viewModel with the right data
                var viewModelItem = ko.dataFor(draggedElem.get(0));
                var targetRank = dropTarget.data('rank');
                viewModelItem.Rank = targetRank;
            }
        });
    });

    return my;
}(sordid || {});




