using System.Threading.Tasks;

namespace HeroesCup.Identity
{
    public interface IHeroesCupIdentitySeed
    {
        Task SeedRolesAsync();
        Task SeedIdentityAsync();
    }
}