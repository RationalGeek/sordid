// jQuery plugin that transforms an input into a row of bubbles that you can click to set numeric values
(function ($) {

    $.fn.bubbleChooser = function (bubbleMax) {
        // Hide the input
        this.css('display', 'none');

        // Get the bubbles
        this.each(function () {
            var input = $(this);

            // Insert the bubbles
            var insertString = '<div class="bubbleChooser">';
            for (var i = 1; i <= bubbleMax; i++) {
                insertString += '<span class="bubbleChooser-bubble" data-bubbleval="' + i + '"></span>';
            }
            insertString += '</div>';
            var parent = input.parent();
            parent.append(insertString);

            // Attach click handler
            var bubbles = parent.find('.bubbleChooser-bubble');
            bubbles.click(function () {
                var bubbleval = $(this).data('bubbleval');
                // jQuery val() does not trigger change, so have to trigger it manually
                input.val(bubbleval).change();
            });

            input.change(function () {
                // Loop through the bubbles and set them to disabled if they are greater than the new value
                var bubbleval = input.val();
                bubbles.each(function () {
                    var bubble = $(this);
                    var bval = bubble.data('bubbleval');
                    if (bval > bubbleval) {
                        bubble.addClass('disabled');
                    }
                    else {
                        bubble.removeClass('disabled');
                    }
                });
            });

            // Trigger initial setup
            input.change();
        });
    };

}(jQuery));