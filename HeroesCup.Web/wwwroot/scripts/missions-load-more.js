$(function () {
    $(document).ready(function () {
        $("#load-missions").click(function () {
            $("#missions-partial").load("missions/load-missions", { loadRequest: true });
        });

        $("#load-missionideas").click(function () {
            $("#missionideas-partial").load("missions/load-missionideas", { loadRequest: true });
        });
    });
});
