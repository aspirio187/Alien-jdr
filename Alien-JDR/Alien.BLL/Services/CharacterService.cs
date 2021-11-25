﻿using Alien.BLL.Dtos;
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
        private readonly IMapper _mapper;

        public CharacterService(ICharacterRepository characterRepository, IMapper mapper)
        {
            _characterRepository = characterRepository ??
                throw new ArgumentNullException(nameof(characterRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public bool CreateCharacter(CharacterCreationDto character)
        {
            if (character is null) throw new ArgumentNullException(nameof(character));
            CharacterEntity characterToCreate = _mapper.Map<CharacterEntity>(character);
            try
            {
                CharacterEntity createdCharacter = _characterRepository.Create(characterToCreate);
                if (createdCharacter is null) return false;
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

        public TalentFromJsonDto[] GetTalentsFromJson()
        {
            string file = File.ReadAllText("../../../../Alien.BLL/Static Data/Talents.json", Encoding.GetEncoding("iso-8859-1"));
            return JsonConvert.DeserializeObject<TalentFromJsonDto[]>(file.Trim());
        }
    }
}
