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
using Microsoft.Extensions.DependencyInjection;

namespace Alien.BLL
{
    public static class LayersConfiguration
    {
        public static IServiceCollection InjectNTier(this IServiceCollection services)
        {
            services.AddScoped<IMapper>(_ => ConfigureMapper());
            services.AddScoped<AlienContext>();

            // Injection des Repository
            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            services.AddScoped<ILobbyPlayerRepository, LobbyPlayerRepository>();
            services.AddScoped<ILobbyRepository, LobbyRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ITalentRepository, TalentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWoundRepository, WoundRepository>();


            // Injection des services
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<ILobbyService, LobbyService>();
            services.AddScoped<ILobbyPlayerService, LobbyPlayerService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        public static IMapper ConfigureMapper()
        {
            MapperConfiguration configuration =
                new MapperConfiguration(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));
            IMapper mapper = new Mapper(configuration);
            return mapper;
        }
    }
}