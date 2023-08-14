using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeerController : ControllerBase
    {
        
        private readonly ILogger<PeerController> _logger;

        public PeerController(ILogger<PeerController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/test")]
        public ActionResult<string> FizzBuzz([FromServices] IFizzBuzz fizzBuzz)
        {
            return $"Result: {fizzBuzz.CalculateResult()}";
        

        }
        [HttpGet]
        public  ActionResult<string> FizzBuzz()
        {
            var rand = new Random();
            if (rand.Next(2) == 0)
            {
                return $"{rand.Next(100)}:{rand.Next(100)}";
            }
            return $"{rand.Next(100)}:{rand.Next(100)}:{rand.Next(100)}";
        }
    }
}
