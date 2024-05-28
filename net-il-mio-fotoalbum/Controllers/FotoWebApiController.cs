using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Data;

namespace net_il_mio_fotoalbum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotoWebApiController : ControllerBase
    {



        [HttpPost]
        public IActionResult Create()
        {

            using FotoDbContext db = new FotoDbContext();

            return null;
        }

    }
}
