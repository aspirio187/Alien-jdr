using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.DAL.Entities;
using Alien.DAL.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Services
{
    public class LobbyPlayerService : ILobbyPlayerService
    {
        private readonly ILobbyPlayerRepository _lobbyPlayerRepository;
        private readonly ILobbyRepository _lobbyRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public LobbyPlayerService(ILobbyPlayerRepository lobbyPlayerRepository, IMapper mapper, ILobbyRepository lobbyRepository, ICharacterRepository characterRepository, IUserRepository userRepository)
        {
            _lobbyPlayerRepository = lobbyPlayerRepository ??
                throw new ArgumentNullException(nameof(lobbyPlayerRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _lobbyRepository = lobbyRepository ??
                throw new ArgumentNullException(nameof(lobbyRepository));
            _characterRepository = characterRepository ??
                throw new ArgumentNullException(nameof(characterRepository));
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));
        }

        public bool CreateLobbyCreator(CreateLobbyPlayerDto lobbyPlayer)
        {
            if (lobbyPlayer is null) throw new ArgumentNullException(nameof(lobbyPlayer));
            lobbyPlayer.IsCreator = true;
            LobbyPlayerEntity lobbyCreated = _lobbyPlayerRepository.Create(_mapper.Map<LobbyPlayerEntity>(lobbyPlayer));
            return _lobbyPlayerRepository.SaveChanges();
        }

        public async Task<bool> IsUserCreator(Guid userId, int lobbyId)
        {
            IEnumerable<LobbyPlayerEntity> users = await _lobbyPlayerRepository.GetAllAsync();
            return users.FirstOrDefault(u => u.lobbyId == lobbyId && u.UserId == userId && u.IsCreator == true) is not null;
        }

        public async Task<LobbyPlayerDto> GetLobbyPlayerAsync(Guid userId, int lobbyId)
        {
            IEnumerable<LobbyPlayerEntity> lobbyPlayers = await _lobbyPlayerRepository.GetAllAsync();
            LobbyPlayerEntity lobbyPlayer = lobbyPlayers.FirstOrDefault(u => u.lobbyId == lobbyId && u.UserId == userId && u.IsCreator == false);
            if (lobbyPlayer is null) throw new NullReferenceException($"There is no user with \"{userId}\" in lobby \"{lobbyId}\"");
            if(lobbyPlayer.CharacterId is not null) lobbyPlayer.Character = await _characterRepository.GetByKeyAsync((int)lobbyPlayer.CharacterId);
            lobbyPlayer.User = await _userRepository.GetByKeyAsync(lobbyPlayer.UserId);
            return _mapper.Map<LobbyPlayerDto>(lobbyPlayer);
        }

        public bool CreateLobbyPlayer(CreateLobbyPlayerDto lobbyPlayer)
        {
            if (lobbyPlayer is null) throw new ArgumentNullException(nameof(lobbyPlayer));
            lobbyPlayer.IsCreator = false;
            LobbyPlayerEntity lobbyCreate = _lobbyPlayerRepository.Create(_mapper.Map<LobbyPlayerEntity>(lobbyPlayer));
            return _lobbyPlayerRepository.SaveChanges();
        }
    }
}
