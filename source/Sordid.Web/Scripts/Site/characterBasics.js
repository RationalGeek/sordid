define('sordid-characterBasics', ['jquery', 'knockout', 'sordid-characterManage'], function ($, ko, charMan) {
    $(document).on('sordid.ko.viewModelInit', function (event, viewModel) {
        viewModel.Character.PowerLevelId.subscribe(function (newValue) {
            // Find the selected PowerLevel and copy appropriate values onto Character
            var powerLevels = viewModel.PowerLevels();
            for (var i = 0; i < powerLevels.length; i++) {
                var powerLevel = powerLevels[i];
                if (powerLevel.Id() == newValue) {
                    viewModel.Character.MaxSkillPoints(powerLevel.SkillPoints());
                    viewModel.Character.BaseRefresh(powerLevel.BaseRefresh());
                    break;
                }
            }
        });
    });
});
