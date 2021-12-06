using Alien.BLL.Interfaces;
using Alien.BLL.Services;
using Alien.DAL;
using Alien.DAL.Interfaces;
using Alien.DAL.Repositories;
using AutoMapper;
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
            containerRegistry.Register<IMapper>(ConfigureMapper);
            containerRegistry.RegisterScoped<AlienContext>();

            // Injection des Repository
            containerRegistry.RegisterScoped<IUserRepository, UserRepository>();
            containerRegistry.RegisterScoped<ITalentRepository, TalentRepository>();
            containerRegistry.RegisterScoped<ICharacterRepository, CharacterRepository>();

            // Injection des services
            containerRegistry.RegisterScoped<IUserService, UserService>();
            containerRegistry.RegisterScoped<ICharacterService, CharacterService>();

            return containerRegistry;
        }

        public static IMapper ConfigureMapper()
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));
            IMapper mapper = new Mapper(configuration);
            return mapper;
        }
    }
}
