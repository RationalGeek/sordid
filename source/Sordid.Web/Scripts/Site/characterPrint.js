// Make the Stunts & Powers panel align to the bottom of the Misc Stats panel
// Event can't be document ready because layout hasn't happened yet when that fires
$(window).load(function () {
    var powersPanel = $('#powersPanel');
    var miscStatsPanel = $('#miscStatsPanel');
    
    // Get the difference in bottoms
    var powerBottom = powersPanel.position().top + powersPanel.height();
    var miscStatsBottom = miscStatsPanel.position().top + miscStatsPanel.height();

    // Calculate diff, and increase height of one or the other panel
    var diff = miscStatsBottom - powerBottom;
    if (diff > 0)
    {
        // Increase the height of powers panel
        powersPanel.height(powersPanel.height() + diff);
    }
    else
    {
        // Increase the height of misc stats panel
        miscStatsPanel.height(miscStatsPanel.height() - diff);
    }
});