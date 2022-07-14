using Microsoft.AspNetCore.Mvc;

namespace Api_Game.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        [HttpGet]
        public ActionResult<int> PostDiceRoll(Game2 game)
        {   
            
            return 0;
        }


        [HttpGet("{id}")]
        public ActionResult<bool> AcceptOrDecline(Game2 game)
        {
            if (game.Status == true)
            {
                game.InitiatingPlayer = game.Oponent;
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }


    //[HttpPost]
    //public void PostMove()
    //{
    //    return;
    //    // in client
    //}


    //[HttpPut("{id}")]
    //public int[] RequestGame(Game2 game)
    //{
    //    if (game.Status == true)
    //    {
    //        return (int)game.InitiatingPlayer! && (int)game.Oponent;
    //    }
    //}


    //[HttpDelete("{id}")]
    //public void EndGame(int id)
    //{
    //    return;
    //    // client
    //}
}

