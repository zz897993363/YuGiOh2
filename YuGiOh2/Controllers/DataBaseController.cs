using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YuGiOh2.Base;
using YuGiOh2.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YuGiOh2.Controllers
{
    [Route("[controller]")]
    public class DataBaseController : Controller
    {
        DBContext DBContext;

        public DataBaseController(DBContext context)
        {
            DBContext = context;
        }

        [HttpGet("getdecks")]
        public IActionResult GetDecks()
        {
            return new JsonResult(DuelUtils.GetAllDecks());
        }

        [HttpPost("addcard")]
        public IActionResult AddCard([FromBody]Models.Card value)
        {
            DBContext.Card.Add(value);
            try
            {
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
