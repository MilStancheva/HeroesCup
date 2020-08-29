$(function () {
    $(document).ready(function () {
        $("#load-missions").click(function () {
            $("#missions-with-banner-partial").load("missions/load-missions", { loadRequest: true }, function () {
                $("#load-missions-container").hide(); 
            });
        });

        $("#load-missionideas").click(function () {
            $("#missionideas-partial").load("missions/load-missionideas", { loadRequest: true }, function () {
                $("#load-missionideas-container").hide();
            });         
        });

        $("#load-stories").click(function () {
            $("#stories-partial").load("missions/load-stories", { loadRequest: true }, function () {
                $("#load-stories-container").hide();
            });            
        });
    });
});
