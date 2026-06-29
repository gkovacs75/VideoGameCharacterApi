using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoGameCharacterApi.DTOs;
using VideoGameCharacterApi.Models;
using VideoGameCharacterApi.Services;

namespace VideoGameCharacterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameCharactersController(IVideoGameCharacterService service) : ControllerBase
    {
        [HttpGet(Name = "GetCharacters")]
        public async Task<ActionResult<List<CharacterDto>>> GetCharacters() => Ok(await service.GetAllCharactersAsync());


        [HttpGet("{id}", Name = "GetCharacter")]
        public async Task<ActionResult<CharacterDto>> GetCharacter(int id)
        {
            var c = await service.GetCharacterByIdAsync(id);

            if (c == null)
            {
                return NotFound($"Character with ID {id} was not found");
            }
            else
            {
                return Ok(c);
            }
        }

        [HttpPost(Name = "AddCharacter")]
        public async Task<ActionResult<CharacterDto>> AddCharacter(CharacterDto characterDTO)
        {
            var newCharacter = await service.AddCharacterAsync(characterDTO);

            return CreatedAtAction(nameof(GetCharacter), new { id = newCharacter.Id }, newCharacter);
        }

        [HttpPut("{id}", Name = "UpdateCharacter")]
        public async Task<ActionResult<CharacterDto>> UpdateCharacter(int id, CharacterDto characterDTO)
        {
            var updatedCharacter = await service.UpdateCharacterAsync(id, characterDTO);
            if (updatedCharacter == null)
            {
                return NotFound($"Character with ID {id} was not found");
            }
            else
            {
                return Ok(updatedCharacter);
            }
        }

        //[HttpPut("{id}", Name = "DeleteCharacter")]
        //public async Task<ActionResult> DeleteCharacter(int id)
        //{
        //    var deleted = await service.DeleteCharacterAsync(id);
        //    if (!deleted)
        //    {
        //        return NotFound($"Character with ID {id} was not found");
        //    }
        //    else
        //    {
        //        return NoContent();
        //    }
        //}
    }
}
