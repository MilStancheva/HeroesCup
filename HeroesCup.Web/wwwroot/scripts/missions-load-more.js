$(function () {
    $(document).ready(function () {
        $("#load-missions").click(function () {
            $("#missions-with-banner-partial").load("missions/load-missions", { loadRequest: true });
            $(window).ready(function () {
                $("#load-missions-container").hide();           
            });
        });

        $("#load-missionideas").click(function () {
            $("#missionideas-partial").load("missions/load-missionideas", { loadRequest: true });
            $(window).ready(function () {
                $("#load-missionideas-container").hide();
            });          
        });

        $("#load-stories").click(function () {
            $("#stories-partial").load("missions/load-stories", { loadRequest: true });
            $(window).ready(function () {
                $("#load-stories-container").hide();
            });            
        });
    });
});
