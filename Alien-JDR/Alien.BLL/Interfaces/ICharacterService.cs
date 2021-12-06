﻿using Alien.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Interfaces
{
    public interface ICharacterService
    {
        Task<IEnumerable<CharacterMiniatureDto>> GetCharactersMiniaturesAsync(Guid userId);
        Task<bool> CreateCharacter(CharacterCreationDto character, Guid userId);
        Task<bool> DeleteCharacter(int characterId);
        CareerFromJsonDto[] GetCareersFromJson();
        TalentFromJsonDto[] GetTalentsFromJson(string careerName);
        CareerFromJsonDto GetCareer(string careerName);
    }
}
