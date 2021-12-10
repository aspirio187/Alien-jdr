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
    public class LobbyPlayerService : ILobbyPlayerService
    {
        private readonly ILobbyPlayerRepository _lobbyPlayerRepository;
        private readonly IMapper _mapper;

        public LobbyPlayerService(ILobbyPlayerRepository lobbyPlayerRepository, IMapper mapper)
        {
            _lobbyPlayerRepository = lobbyPlayerRepository ??
                throw new ArgumentNullException(nameof(lobbyPlayerRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public bool CreateLobbyCreator(CreateLobbyPlayerDto lobbyPlayer)
        {
            if (lobbyPlayer is null) throw new ArgumentNullException(nameof(lobbyPlayer));
            lobbyPlayer.IsCreator = true;
            LobbyPlayerEntity lobbyCreated = _lobbyPlayerRepository.Create(_mapper.Map<LobbyPlayerEntity>(lobbyPlayer));
            return _lobbyPlayerRepository.SaveChanges();
        }
    }
}
