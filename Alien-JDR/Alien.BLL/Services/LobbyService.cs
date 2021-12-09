using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
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

        public async Task<IEnumerable<LobbyDto>> GetLobbiesAsync()
        {
            return _mapper.Map<IEnumerable<LobbyDto>>(await _lobbyRepository.GetAllAsync());
        }
    }
}
