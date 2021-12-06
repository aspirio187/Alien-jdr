using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.DAL.Entities;
using Alien.DAL.Interfaces;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly ITalentRepository _talentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CharacterService(ICharacterRepository characterRepository, IMapper mapper, ITalentRepository talentRepository, IUserRepository userRepository)
        {
            _characterRepository = characterRepository ??
                throw new ArgumentNullException(nameof(characterRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _talentRepository = talentRepository ??
                throw new ArgumentNullException(nameof(talentRepository));
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<bool> CreateCharacter(CharacterCreationDto character, Guid userId)
        {
            if (character is null) throw new ArgumentNullException(nameof(character));
            if (!await _userRepository.UserExists(userId)) return false;
            CharacterEntity characterToCreate = _mapper.Map<CharacterEntity>(character);
            characterToCreate.OwnerId = userId;
            characterToCreate.IsEditable = false;

            // TODO : Créer un personnage public si nécessaire et créer un personnage éditable

            TalentEntity talentFromRepo = await _talentRepository.GetTalentByNameAsync(character.Talent);

            if (talentFromRepo is null)
            {
                characterToCreate.Talents = new List<TalentEntity>() {
                    new TalentEntity()
                    {
                        Name = character.Talent
                    }};
            }
            else
            {
                characterToCreate.Talents = new List<TalentEntity>() { talentFromRepo };
            }

            try
            {
                CharacterEntity createdCharacter = _characterRepository.Create(characterToCreate);
                CharacterEntity editableCharacter = new CharacterEntity(createdCharacter);
                editableCharacter.IsEditable = true;
                CharacterEntity createdEditableCharacter = _characterRepository.Create(editableCharacter);
                if (createdCharacter is null) return false;
                if (editableCharacter is null) return false;

                return _characterRepository.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> DeleteCharacter(int characterId)
        {
            CharacterEntity characterFromRepo = await _characterRepository.GetByKeyAsync(characterId);
            if (characterFromRepo is null) return false;
            try
            {
                _characterRepository.Delete(characterFromRepo);
                return _characterRepository.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<IEnumerable<CharacterMiniatureDto>> GetCharactersMiniaturesAsync(Guid userId)
        {
            if (userId == Guid.Empty) throw new ArgumentException($"The user ID \"{userId}\" is empty!");
            IEnumerable<CharacterEntity> charactersFromRepo = await _characterRepository.GetUserCharactersAsync(userId);
            return _mapper.Map<IEnumerable<CharacterMiniatureDto>>(charactersFromRepo);
        }

        public CareerFromJsonDto[] GetCareersFromJson()
        {
            string file = File.ReadAllText("../../../../Alien.BLL/Static Data/Careers.json", Encoding.GetEncoding("iso-8859-1"));
            return JsonConvert.DeserializeObject<CareerFromJsonDto[]>(file.Trim());
        }

        public TalentFromJsonDto[] GetTalentsFromJson(string careerName)
        {
            string file = File.ReadAllText("../../../../Alien.BLL/Static Data/Talents.json", Encoding.GetEncoding("iso-8859-1"));
            return JsonConvert.DeserializeObject<TalentFromJsonDto[]>(file.Trim())
                .Where(t => t.Career.Equals(careerName) || t.Career.Equals("Général"))
                .ToArray();
        }

        public CareerFromJsonDto GetCareer(string careerName)
        {
            if (careerName is null) throw new ArgumentNullException(nameof(careerName));
            CareerFromJsonDto[] careers = GetCareersFromJson();
            return careers.FirstOrDefault(c => c.Name.Equals(careerName));
        }
    }
}
