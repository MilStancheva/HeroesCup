﻿using ClubsModule;
using HeroesCup.Modules.ClubsModule.Blocks;
using Piranha;
using Piranha.Extend;
using Piranha.Manager;
using Piranha.Security;
using System.Collections.Generic;

namespace HeroesCup.Modules.ClubsModule
{
    /// <summary>
    /// The identity module.
    /// </summary>
    public class Module : IModule
    {
        private readonly List<PermissionItem> _permissions = new List<PermissionItem>
        {
            new PermissionItem { Name = Permissions.Clubs, Title = "List Clubs", Category = "Clubs", IsInternal = true },
            new PermissionItem { Name = Permissions.ClubsAdd, Title = "Add Clubs", Category = "Clubs", IsInternal = true },
            new PermissionItem { Name = Permissions.ClubsDelete, Title = "Delete Clubs", Category = "Clubs", IsInternal = true },
            new PermissionItem { Name = Permissions.ClubsEdit, Title = "Edit Clubs", Category = "Clubs", IsInternal = true },
            new PermissionItem { Name = Permissions.ClubsSave, Title = "Save Clubs", Category = "Clubs", IsInternal = true },

            new PermissionItem { Name = Permissions.Heroes, Title = "List Heroes", Category = "Heroes", IsInternal = true },
            new PermissionItem { Name = Permissions.HeroesAdd, Title = "Add Heroes", Category = "Heroes", IsInternal = true },
            new PermissionItem { Name = Permissions.HeroesDelete, Title = "Delete Heroes", Category = "Heroes", IsInternal = true },
            new PermissionItem { Name = Permissions.HeroesEdit, Title = "Edit Heroes", Category = "Heroes", IsInternal = true },
            new PermissionItem { Name = Permissions.HeroesSave, Title = "Save Heroes", Category = "Heroes", IsInternal = true },
            new PermissionItem { Name = Permissions.HeroesAddCoordinator, Title = "Add Coordinator Heroes", Category = "Heroes", IsInternal = true },

            new PermissionItem { Name = Permissions.Missions, Title = "List Missions", Category = "Missions", IsInternal = true },
            new PermissionItem { Name = Permissions.MissionsAdd, Title = "Add Missions", Category = "Missions", IsInternal = true },
            new PermissionItem { Name = Permissions.MissionsDelete, Title = "Delete Missions", Category = "Missions", IsInternal = true },
            new PermissionItem { Name = Permissions.MissionsEdit, Title = "Edit Missions", Category = "Missions", IsInternal = true },
            new PermissionItem { Name = Permissions.MissionsSave, Title = "Save Missions", Category = "Missions", IsInternal = true },
            new PermissionItem { Name = Permissions.MissionsStars, Title = "Stars Missions", Category = "Missions", IsInternal = true },
            new PermissionItem { Name = Permissions.MissionsPublish, Title = "Publish Missions", Category = "Missions", IsInternal = true },


            new PermissionItem { Name = Permissions.Stories, Title = "List Stories", Category = "Stories", IsInternal = true },
            new PermissionItem { Name = Permissions.StoriesAdd, Title = "Add Stories", Category = "Stories", IsInternal = true },
            new PermissionItem { Name = Permissions.StoriesDelete, Title = "Delete Stories", Category = "Stories", IsInternal = true },
            new PermissionItem { Name = Permissions.StoriesEdit, Title = "Edit Stories", Category = "Stories", IsInternal = true },
            new PermissionItem { Name = Permissions.StoriesSave, Title = "Save Stories", Category = "Stories", IsInternal = true },
            new PermissionItem { Name = Permissions.StoriesPublish, Title = "Publish Stories", Category = "Stories", IsInternal = true },

            new PermissionItem { Name = Permissions.MissionIdeas, Title = "List Mission Ideas", Category = "Mission Ideas", IsInternal = true },
            new PermissionItem { Name = Permissions.MissionIdeasAdd, Title = "Add Mission Ideas", Category = "Mission Ideas", IsInternal = true },
            new PermissionItem { Name = Permissions.MissionIdeasDelete, Title = "Delete Mission Ideas", Category = "Mission Ideas", IsInternal = true },
            new PermissionItem { Name = Permissions.MissionIdeasEdit, Title = "Edit Mission Ideas", Category = "Mission Ideas", IsInternal = true },
            new PermissionItem { Name = Permissions.MissionIdeasSave, Title = "Save Mission Ideas", Category = "Mission Ideas", IsInternal = true },
            new PermissionItem { Name = Permissions.MissionIdeasPublish, Title = "Publish Mission Ideas", Category = "Mission Ideas", IsInternal = true },
        };

        /// <summary>
        /// Gets the Author
        /// </summary>
        public string Author => "Andrey Dautev";

        /// <summary>
        /// Gets the Name
        /// </summary>
        public string Name => "ClubsModule";

        /// <summary>
        /// Gets the Version
        /// </summary>
        public string Version => Utils.GetAssemblyVersion(GetType().Assembly);

        /// <summary>
        /// Gets the description
        /// </summary>
        public string Description => "Clubs Module";

        /// <summary>
        /// Gets the package url.
        /// </summary>
        public string PackageUrl => "https://www.nuget.org/packages/ClubsModule";

        /// <summary>
        /// Gets the icon url.
        /// </summary>
        public string IconUrl => "";

        /// <summary>
        /// Initializes the module.
        /// </summary>
        public void Init()
        {
            foreach (var permission in _permissions)
            {
                App.Permissions["Manager"].Add(permission);
            }

            Menu.Items.Insert(2, new MenuItem
            {
                InternalId = "ClubsModule",
                Name = "Клубове",
                Css = "fas fa-fish"
            });
            Menu.Items["ClubsModule"].Items.Add(new MenuItem
            {
                InternalId = "Clubs",
                Name = "Твоят клуб",
                Route = "~/manager/clubs",
                Policy = Permissions.Clubs,
                Css = "fas fa-brain"
            });

            Menu.Items["ClubsModule"].Items.Add(new MenuItem
            {
                InternalId = "Heroes",
                Name = "Герои",
                Route = "~/manager/heroes",
                Policy = Permissions.Heroes,
                Css = "fas fa-users"
            });

            Menu.Items["ClubsModule"].Items.Add(new MenuItem
            {
                InternalId = "Missions",
                Name = "Мисии",
                Route = "~/manager/missions",
                Policy = Permissions.Missions,
                Css = "far fa-calendar-alt"
            });

            Menu.Items["ClubsModule"].Items.Add(new MenuItem
            {
                InternalId = "Stories",
                Name = "Разкази",
                Route = "~/manager/stories",
                Policy = Permissions.Stories,
                Css = "fas fa-history"
            });

            Menu.Items["ClubsModule"].Items.Add(new MenuItem
            {
                InternalId = "MissionIdeas",
                Name = "Идеи за мисии",
                Route = "~/manager/missionideas",
                Policy = Permissions.MissionIdeas,
                Css = "far fa-lightbulb"
            });

            App.Blocks.Register<Clubs>();
        }
    }
}