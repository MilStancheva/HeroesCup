using HeroesCup.Modules.ClubsModule.Blocks;
using Piranha;
using Piranha.Extend;

namespace HeroesCup.Modules.ClubsModule
{
    /// <summary>
    /// The identity module.
    /// </summary>
    public class Module : IModule
    {
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
            App.Blocks.Register<Clubs>();
        }
    }
}