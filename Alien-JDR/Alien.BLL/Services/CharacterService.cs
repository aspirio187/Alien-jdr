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
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;

        public CharacterService(ICharacterRepository characterRepository, IMapper mapper)
        {
            _characterRepository = characterRepository ??
                throw new ArgumentNullException(nameof(characterRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public CharacterService()
        {

        }

        public async Task<IEnumerable<CharacterMiniatureDto>> GetCharactersMiniaturesAsync(Guid userId)
        {
            if (userId == Guid.Empty) throw new ArgumentException($"The user ID \"{userId}\" is empty!");
            IEnumerable<CharacterEntity> charactersFromRepo = await _characterRepository.GetUserCharactersAsync(userId);
            return _mapper.Map<IEnumerable<CharacterMiniatureDto>>(charactersFromRepo);
        }
    }
}
