using Alien.BLL.Interfaces;
using Alien.BLL.Services;
using Alien.DAL;
using Alien.DAL.Interfaces;
using Alien.DAL.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL
{
    public static class LayersConfiguration
    {
        //public static IContainerRegistry InjectNTier(this IContainerRegistry containerRegistry)
        //{
        //    containerRegistry.Register<IMapper>(ConfigureMapper);
        //    containerRegistry.RegisterScoped<AlienContext>();

        //    // Injection des Repository
        //    containerRegistry.RegisterScoped<ICharacterRepository, CharacterRepository>();
        //    containerRegistry.RegisterScoped<IEquipmentRepository, EquipmentRepository>();
        //    containerRegistry.RegisterScoped<ILobbyPlayerRepository, LobbyPlayerRepository>();
        //    containerRegistry.RegisterScoped<ILobbyRepository, LobbyRepository>();
        //    containerRegistry.RegisterScoped<INotificationRepository, NotificationRepository>();
        //    containerRegistry.RegisterScoped<IRoleRepository, RoleRepository>();
        //    containerRegistry.RegisterScoped<ITalentRepository, TalentRepository>();
        //    containerRegistry.RegisterScoped<IUserRepository, UserRepository>();
        //    containerRegistry.RegisterScoped<IWoundRepository, WoundRepository>();


        //    // Injection des services
        //    containerRegistry.RegisterScoped<ICharacterService, CharacterService>();
        //    containerRegistry.RegisterScoped<ILobbyService, LobbyService>();
        //    containerRegistry.RegisterScoped<ILobbyPlayerService, LobbyPlayerService>();
        //    containerRegistry.RegisterScoped<INotificationService, NotificationService>();
        //    containerRegistry.RegisterScoped<IUserService, UserService>();

        //    return containerRegistry;
        //}

        public static IMapper ConfigureMapper()
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));
            IMapper mapper = new Mapper(configuration);
            return mapper;
        }
    }
}
