using AspNetPokeAPi.DTO;
using AspNetPokeAPi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AspNetPokeAPi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly DBContext DBContext;

        public PokemonController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetPokemons")]
        public async Task<ActionResult<List<PokemonDTO>>> Get()
        {
            var List = await DBContext.Pokemons.Select(
                s => new PokemonDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Image = s.Image,
                    Description = s.Description,
                    CreateDate = s.CreateDate
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("GetPokemonById")]
        public async Task<ActionResult<PokemonDTO>> GetPokemonById(int Id)
        {
            PokemonDTO Pokemon = await DBContext.Pokemons.Select(
                    s => new PokemonDTO
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Image = s.Image,
                        Description = s.Description,
                        CreateDate = s.CreateDate
                    })
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (User == null)
            {
                return NotFound();
            }
            else
            {
                return Pokemon;
            }
        }

        [HttpPost("InsertPokemon")]
        public async Task<HttpStatusCode> InsertPokemon(PokemonDTO Pokemon)
        {
            var entity = new Pokemon()
            {
                Id = Pokemon.Id,
                Name = Pokemon.Name,
                Image = Pokemon.Image,
                Description = Pokemon.Description,
                CreateDate = DateTime.Now.Date
            };

            DBContext.Pokemons.Add(entity);
            await DBContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdatePokemon")]
        public async Task<HttpStatusCode> UpdatePokemon(PokemonDTO Pokemon)
        {
            var entity = await DBContext.Pokemons.FirstOrDefaultAsync(s => s.Id == Pokemon.Id);

            entity.Name = Pokemon.Name;
            entity.Image = Pokemon.Image;
            entity.Description = Pokemon.Description;
            entity.CreateDate = Pokemon.CreateDate;

            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeletePokemon/{Id}")]
        public async Task<HttpStatusCode> DeletePokemon(int Id)
        {
            var entity = new Pokemon()
            {
                Id = Id
            };
            DBContext.Pokemons.Attach(entity);
            DBContext.Pokemons.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
