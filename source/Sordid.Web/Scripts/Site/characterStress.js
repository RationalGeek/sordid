define('sordid-characterStress', ['jquery'], function ($) {
    $(document).on('sordid.ko.viewModelBound', function (event, viewModel) {
        $('.stressBubbleChooser').bubbleChooser(8);
    });
});
