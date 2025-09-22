using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace examole.Serviese
{
    public class RoleService
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public RoleService(RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        public async Task<bool> CreateAsync(string roleVM)
        {
            try
            {
                var getRoleByName = await roleManager.FindByNameAsync(roleVM);
                if (getRoleByName is null)
                {
                    var role = mapper.Map<IdentityRole>(roleVM);
                    var result = await roleManager.CreateAsync(role);
                    return result.Succeeded;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }

}
