using KEZDOCSAPATXI.Models;
using KEZDOCSAPATXI.Services;
using Microsoft.AspNetCore.Mvc;

namespace KEZDOCSAPATXI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LineupController(IRatingGeneratorService service) : ControllerBase
    {
        private readonly IRatingGeneratorService _generatorService = service;
        [HttpPost]
        public ActionResult<Lineup> GenerateResult([FromBody] List<Player> players)
        {
            try
            {
                return Ok(_generatorService.GenerateAllLineups(players));
            }
            catch (Exception) { return BadRequest(); }
        }
    }
}
