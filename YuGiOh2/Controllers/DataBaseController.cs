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

        // GET: api/<controller>
        //[HttpGet]
        //public int Get()
        //{
        //    Models.Card card = new Models.Card();
        //    card.ATK = 3000;
        //    card.Attribute = (int)Base.Attribute.Light;
        //    card.CardType = (int)CardType.Normal;
        //    card.DEF = 2500;
        //    card.Level = 8;
        //    card.MonsterType = (int)MonsterType.Dragon;
        //    card.Name = "Blue-Eyes White Dragon";
        //    card.Password = "89631139";
        //    card.SummonedAttribute = (int)SummonedAttribute.Divine;
        //    card.Text = "This legendary dragon is a powerful engine of destruction. Virtually invincible, very few have faced this awesome creature and lived to tell the tale.";
        //    DBContext.Card.Add(card);
        //    return DBContext.SaveChanges();



        //}

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
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
