using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ApiRoulette.DTO;
using ApiRoulette.Models;
using ApiRoulette.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiRoulette.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiRouletteController : ControllerBase
    {
        private IRouletteService rouletteService;

        public ApiRouletteController(IRouletteService rouletteService)
        {
            this.rouletteService = rouletteService;
        }

        //<sumary>
        //Creation New Rulette
        //</sumary>
        [HttpPost]
        public IActionResult NewRulette()
        {
            Roulette roulette = rouletteService.create();
            return Ok(roulette);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(rouletteService.GetAll());
        }

        //<sumary>
        //Open Roulette Id
        //</sumary>
        [HttpPut("{id}/open")]
        public IActionResult Open([FromRoute(Name = "id")] string id)
        {
            try
            {
                rouletteService.Open(id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(405);
            }
        }

        //<sumary>
        //Close Best on a Rulette
        //</sumary>
        [HttpPut("{id}/close")]
        public IActionResult Close([FromRoute(Name ="id")]string id)
        {
            try
            {
                Roulette roulette = rouletteService.Close(id);
                return Ok(roulette);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(405);
            }
        }
        //<sumary>
        //Lets place a bet
        //</sumary>
        [HttpPost("{id}/bet")]
        public IActionResult Bet([FromHeader(Name ="user-id")] string UserId,[FromRoute(Name = "id")] 
                                            string id, [FromBody] BetRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    error = true,
                    msg = "Bad Request"
                });
            }

            try
            {
                Roulette roulette = rouletteService.Bet(id, UserId, request.position, request.money);
                return Ok(roulette);
            } 
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(405);
            }
        }

    }
}
