using System.Globalization;

namespace HeroesCup.Web.Models
{
    public interface IHeroesCupPost
    {
        string CurrentUrlBase { get; set; }

        CultureInfo SiteCulture { get; set; }
    }
}
