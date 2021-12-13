using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.DAL.Entities;
using Alien.DAL.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Services
{
    public class LobbyService : ILobbyService
    {
        private readonly ILobbyRepository _lobbyRepository;
        private readonly ILobbyPlayerRepository _lobbyPlayerRepository;
        private readonly IMapper _mapper;

        public LobbyService(ILobbyRepository lobbyRepository, IMapper mapper, ILobbyPlayerRepository lobbyPlayerRepository)
        {
            _lobbyRepository = lobbyRepository ??
                throw new ArgumentNullException(nameof(lobbyRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _lobbyPlayerRepository = lobbyPlayerRepository ??
                throw new ArgumentNullException(nameof(lobbyPlayerRepository));
        }

        public LobbyDto CreateLobby(CreateLobbyDto lobby)
        {
            if (lobby is null) throw new ArgumentNullException(nameof(lobby));
            LobbyEntity lobbyToCreate = _mapper.Map<LobbyEntity>(lobby);
            lobbyToCreate.Status = LobbyStatusEnum.Waiting;
            LobbyEntity createdLobby = _lobbyRepository.Create(lobbyToCreate);
            return _lobbyRepository.SaveChanges() ? _mapper.Map<LobbyDto>(createdLobby) : null;
        }

        public bool DeleteLobby(int lobbyId)
        {
            LobbyEntity lobbyFromRepo = Task.Run(async () => await _lobbyRepository.GetByKeyAsync(lobbyId)).Result;
            if (lobbyFromRepo is null) throw new ArgumentException($"Lobby with ID : \"{ lobbyId }\" does not exist!");
            _lobbyRepository.Delete(lobbyFromRepo);
            return _lobbyRepository.SaveChanges();
        }

        public async Task<LobbyDto> GetLobby(int lobbyId)
        {
            LobbyEntity lobbyFromRepo = await _lobbyRepository.GetByKeyAsync(lobbyId);
            return _mapper.Map<LobbyDto>(lobbyFromRepo);
        }

        public async Task<IEnumerable<LobbyDto>> GetLobbiesAsync()
        {
            IEnumerable<LobbyDto> lobbies = _mapper.Map<IEnumerable<LobbyDto>>(await _lobbyRepository.GetAllLobbiesWithPlayersAsync());
            return lobbies;
        }

        public bool UpdateLobby(LobbyDto lobby)
        {
            try
            {
                _lobbyRepository.Update(_mapper.Map<LobbyEntity>(lobby));
                _lobbyRepository.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return true;
        }

        public bool PlayerCanJoin(int lobbyId, Guid userId)
        {
            LobbyEntity lobby = Task.Run(async () => await _lobbyRepository.GetByKeyAsync(lobbyId)).Result;
            lobby.PartyPlayers = new Collection<LobbyPlayerEntity>(Task.Run(async () => await _lobbyPlayerRepository.GetLobbyPlayersAsync(lobbyId)).Result.ToList());
            if (lobby is null) return false;
            return lobby.PartyPlayers.Any(lb => lb.UserId == userId) || lobby.Status == LobbyStatusEnum.Waiting;
        }

        public bool UpdateHostIp(int lobbyId, string hostIp)
        {
            if (hostIp is null) throw new ArgumentNullException(nameof(hostIp));
            if (hostIp.Length <= 0) throw new ArgumentException($"Host IP can not be empty string! : \"{ hostIp }\"");

            LobbyEntity lobby = Task.Run(async () => await _lobbyRepository.GetByKeyAsync(lobbyId)).Result;
            if (lobby is null) throw new NullReferenceException($"There is no lobby with ID : \"{ lobbyId }\"");

            lobby.HostIp = hostIp;
            _lobbyRepository.Update(lobby);
            return _lobbyRepository.SaveChanges();
        }
    }
}
