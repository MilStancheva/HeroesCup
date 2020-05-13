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

        public static string[] All()
        {
            return new[]
            {
                Heroes,
                HeroesAdd,
                HeroesDelete,
                HeroesEdit,
                HeroesSave
            };
        }
    }
}
