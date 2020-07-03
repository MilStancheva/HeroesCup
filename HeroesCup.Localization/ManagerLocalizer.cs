using Microsoft.Extensions.Localization;

namespace HeroesCup.Localization
{
    public class ManagerLocalizer
    {
        public IStringLocalizer<HeroesCup.Localization.Club> Club { get; private set; }

        public IStringLocalizer<HeroesCup.Localization.Hero> Hero { get; private set; }

        public IStringLocalizer<HeroesCup.Localization.Mission> Mission { get; private set; }

        public IStringLocalizer<HeroesCup.Localization.Story> Story { get; private set; }

        public IStringLocalizer<HeroesCup.Localization.General> General { get; private set; }

        public IStringLocalizer<HeroesCup.Localization.MissionIdea> MissionIdea { get; private set; }

        public ManagerLocalizer(IStringLocalizer<HeroesCup.Localization.Club> club,
            IStringLocalizer<HeroesCup.Localization.Hero> hero,
            IStringLocalizer<HeroesCup.Localization.Mission> mission,
            IStringLocalizer<HeroesCup.Localization.Story> story,
             IStringLocalizer<HeroesCup.Localization.General> general,
             IStringLocalizer<HeroesCup.Localization.MissionIdea> missionIdea)
        {
            Club = club;
            Hero = hero;
            Mission = mission;
            Story = story;
            General = general;
            MissionIdea = missionIdea;
        }
    }
}