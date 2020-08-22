$(function () {
    $(document).ready(function () {
        $("#load-missions").click(function () {
            $("#missions-with-banner-partial").load("missions/load-missions", { loadRequest: true });
            $("#load-missions").hide();
        });

        $("#load-missionideas").click(function () {
            $("#missionideas-partial").load("missions/load-missionideas", { loadRequest: true });
            $("#load-missionideas").hide();
        });

        $("#load-stories").click(function () {
            $("#stories-partial").load("missions/load-stories", { loadRequest: true });
            $("#load-stories").hide();
        });
    });
});
