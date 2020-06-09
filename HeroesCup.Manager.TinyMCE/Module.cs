using Piranha;
using Piranha.Extend;
using System.Reflection;

namespace HeroesCup.Manager.TinyMCE
{
    public class Module : IModule
    {
        public string Author => "Milena Stancheva";

        public string Name => "HeroesCup.Manager.TinyMCE";

        public string Version => Utils.GetAssemblyVersion(GetType().Assembly);

        public string Description => "Tiny MCE WYSIWYG Editor for HeroesCup Piranha CMS.";

        public string PackageUrl => "https://www.nuget.org/packages/HeroesCup.Manager.TinyMCE";

        public string IconUrl => "";

        internal static readonly Assembly Assembly;

        static Module()
        {
            // Get assembly information
            Assembly = typeof(Module).GetTypeInfo().Assembly;
        }

        public void Init() { }
    }
}
