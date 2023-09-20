using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LearningASPdotNet2.Data;
using LearningASPdotNet2.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearningASPdotNet2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TlouController : ControllerBase
    {
        private readonly DataContext _context;

        public TlouController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacterById(int id) 
        {
            var character = await _context.Characters
                    .Include(c => c.Backpack)
                    .Include(c => c.Weapons)
                    .Include(c => c.Factions)
                    .FirstOrDefaultAsync(c => c.Id == id);
            return Ok(character);
        }

        [HttpPost]
        public async Task<ActionResult<List<Character>>> CreateCharacter (CharacterDto request)
        {
            var newCharacter = new Character
            {
                Name = request.Name
            };

            var backpack = new Backpack { Description = request.Backpack.Description, Character = newCharacter};
            var weapons = request.Weapons.Select(w => new Weapon {Name = w.Name, Character = newCharacter}).ToList();
            var factions = request.Factions.Select(w => new Faction {Name = w.Name, Characters = new List<Character> {newCharacter}}).ToList();

            newCharacter.Backpack = backpack;
            newCharacter.Weapons = weapons;
            newCharacter.Factions = factions;

            _context.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();
            return Ok(await _context.Characters.Include(c => c.Backpack).Include(c => c.Weapons).ToListAsync());
        }
    }
}