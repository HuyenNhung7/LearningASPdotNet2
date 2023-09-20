using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningASPdotNet2.DTOs
{
    public record struct CharacterDto(
            string Name, 
            BackpackDto Backpack, 
            List<WeaponDto> Weapons,
            List<FactionDto> Factions
        );
}