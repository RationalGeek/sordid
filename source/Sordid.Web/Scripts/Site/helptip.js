define('sordid-helptip', ['jquery'], function ($) {
    $(document).ready(function () {
        $('.help-popover-target').popover({
            trigger: 'hover',
            html: 'true',
            delay: { show: 100, hide: 1500 }
        });
    });
});