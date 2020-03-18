using Detego.RfidReader.Protocol;
using Detego.RfidReader.Protocol.Models;
using Microsoft.AspNetCore.Mvc;

namespace Detego.RfidReader.WebApi.Controllers
{
    [ApiController]
    [Route(Uris.About)]
    public class AboutController
    {
        [HttpGet]
        public About Get()
        {
            return new About
            {
                Description = "Detego.RfidReader - АПИ для вакансии Detego"
            };
        }
    }
}