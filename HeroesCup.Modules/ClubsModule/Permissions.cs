namespace ClubsModule
{
    /// <summary>
    /// The available Clubs permissions.
    /// </summary>
    public static class Permissions
    {
        public const string Heroes = "HeroesCupHeroes";
        public const string HeroesAdd = "HeroesCupHeroesAdd";
        public const string HeroesDelete = "HeroesCupHeroesDelete";
        public const string HeroesEdit = "HeroesCupHeroesEdit";
        public const string HeroesSave = "HeroesCupHeroesSave";
        public const string HeroesAddCoordinator = "HeroesCupHeroesAddCoordinator";

        public const string Clubs = "HeroesCupClubs";
        public const string ClubsAdd = "HeroesCupClubsAdd";
        public const string ClubsDelete = "HeroesCupClubsDelete";
        public const string ClubsEdit = "HeroesCupClubsEdit";
        public const string ClubsSave = "HeroesCupClubsSave";

        public const string Missions = "HeroesCupMissions";
        public const string MissionsAdd = "HeroesCupMissionsAdd";
        public const string MissionsDelete = "HeroesCupMissionsDelete";
        public const string MissionsEdit = "HeroesCupMissionsEdit";
        public const string MissionsSave = "HeroesCupMissionsSave";
        public const string MissionsStars = "HeroesCupMissionsStars";

        public static string[] All()
        {
            return new[]
            {
                Clubs,
                ClubsAdd,
                ClubsDelete,
                ClubsEdit,
                ClubsSave,
                Heroes,
                HeroesAdd,
                HeroesDelete,
                HeroesEdit,
                HeroesSave,
                HeroesAddCoordinator,
                Missions,
                MissionsAdd,
                MissionsDelete,
                MissionsEdit,
                MissionsSave,
                MissionsStars
            };
        }
    }
}