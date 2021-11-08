using Alien.BLL.Interfaces;
using Alien.DAL;
using Alien.DAL.Interfaces;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL
{
    public static class LayersConfiguration
    {
        public static IContainerRegistry InjectNTier(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterScoped<AlienContext>();

            // Injection des Repository
            containerRegistry.RegisterScoped<IUserRepository, IUserRepository>();

            // Injection des services
            containerRegistry.RegisterScoped<IUserService, IUserService>();

            return containerRegistry;
        }
    }
}
