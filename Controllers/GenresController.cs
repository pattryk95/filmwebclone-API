using Microsoft.AspNetCore.Mvc;

namespace filmwebclone_API.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        public GenresController()
        {

        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok();
        }
    }
}
