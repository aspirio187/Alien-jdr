using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.DAL.Entities;
using Alien.DAL.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Services
{
    public class LobbyService : ILobbyService
    {
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IMapper _mapper;

        public LobbyService(ILobbyRepository lobbyRepository, IMapper mapper)
        {
            _lobbyRepository = lobbyRepository ??
                throw new ArgumentNullException(nameof(lobbyRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public LobbyDto CreateLobby(CreateLobbyDto lobby)
        {
            if (lobby is null) throw new ArgumentNullException(nameof(lobby));
            LobbyEntity createdLobby = _lobbyRepository.Create(_mapper.Map<LobbyEntity>(lobby));
            return _lobbyRepository.SaveChanges() ? _mapper.Map<LobbyDto>(createdLobby) : null;
        }

        public bool DeleteLobby(int lobbyId)
        {
            LobbyEntity lobbyFromRepo = Task.Run(async () => await _lobbyRepository.GetByKeyAsync(lobbyId)).Result;
            if (lobbyFromRepo is null) throw new ArgumentException($"Lobby with ID : \"{ lobbyId }\" does not exist!");
            _lobbyRepository.Delete(lobbyFromRepo);
            return _lobbyRepository.SaveChanges();
        }

        public async Task<IEnumerable<LobbyDto>> GetLobbiesAsync()
        {
            return _mapper.Map<IEnumerable<LobbyDto>>(await _lobbyRepository.GetAllAsync());
        }
    }
}
