var sordid = function (my) {
    my.characterSkills = {};

    $(document).ready(function () {
        var draggableOptions = { containment: '#section-skills', stack: '#section-skills .skill', revert: 'invalid' };
        $('#section-skills .skill').draggable(draggableOptions);

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
                sortedList.draggable(draggableOptions);
            }
        });
    });

    return my;
}(sordid || {});




