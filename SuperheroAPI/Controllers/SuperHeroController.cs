using Microsoft.AspNetCore.Mvc;

namespace SuperheroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heros = new List<SuperHero>
        {
              new SuperHero { Id = 1, Name = "s", FirstName ="d", LastName="d", Place="d"},
               new SuperHero { Id = 2, Name = "s", FirstName ="d", LastName="d", Place="d"}
        };
        private readonly DataContext context;

        public SuperHeroController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeros()
        {
            return Ok(await context.SuperHeroes.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHerosById(int id)
        {
            var hero =await context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest();

            return Ok(hero);
        }

        [HttpPost] 
        public async Task<ActionResult<SuperHero>> PostSuperHero([FromBody] SuperHero hero)
        {
            context.SuperHeroes.Add(hero);
            await context.SaveChangesAsync();
            return Ok(hero);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHero>> DeleteSuperHero(int id)
        {
            var hero = await context.SuperHeroes.FindAsync(id);
            context.SuperHeroes.Remove(hero);
            await context.SaveChangesAsync();

            return Ok("deleted successfully");
        }
        [HttpPut]
        public async Task<ActionResult<SuperHero>> UpdateSuperHero([FromBody] SuperHero request)
        {
            var hero = await context.SuperHeroes.FindAsync(request.Id);
            if (hero == null)
                return BadRequest();
            hero.Name = request.Name;
            hero.FirstName = request.LastName;
            hero.LastName = request.FirstName;
            hero.Place = request.Place;
            await context.SaveChangesAsync();
            return Ok(hero);

        }
    }
}
