using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.DAL.Interfaces;
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

        public CharacterService(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository ??
                throw new ArgumentNullException(nameof(characterRepository));
        }

        public CharacterService()
        {

        }

        public async Task<IEnumerable<CharacterMiniatureDto>> GetCharactersMiniaturesAsync()
        {
            var charactersFromRepo = _characterRepository.GetAllAsync();
        }
    }
}
