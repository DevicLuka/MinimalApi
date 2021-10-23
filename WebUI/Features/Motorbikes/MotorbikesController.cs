using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebUI.Features.Motorbikes
{
    [Route("api/motorbikes")]
    [ApiController]
    public class MotorbikesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MotorbikesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Motorbike>> GetMotorbikes()
        {
            var motorbikes = _context.Motorbikes.ToList();
            return Ok(motorbikes);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Motorbike> GetMotorbike(int id)
        {
            var dbMotorbike = _context.Motorbikes.FirstOrDefault(x => x.Id == id);

            if (dbMotorbike == null)
            {
                return NotFound();
            }

            return Ok(dbMotorbike);
        }

        [HttpPost]
        public ActionResult<Motorbike> CreateMotorbike(Motorbike motorbike)
        {
            var newMotorbike = new Motorbike
            {
                TeamName = motorbike.TeamName,
                Speed = motorbike.Speed,
                MelfunctionChance = motorbike.MelfunctionChance
            };
            _context.Motorbikes.Add(motorbike);
            _context.SaveChanges();
            return Ok(newMotorbike);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<Motorbike> UpdateMotorbike(Motorbike motorbike)
        {
            var dbMotorbike = _context.Motorbikes.FirstOrDefault(x => x.Id == motorbike.Id);

            if (dbMotorbike == null)
            {
                return NotFound($"Motorbike with id: {motorbike.Id} isn't found.");
            }

            dbMotorbike.TeamName = motorbike.TeamName;
            dbMotorbike.Speed = motorbike.Speed;
            dbMotorbike.MelfunctionChance = motorbike.MelfunctionChance;
            _context.SaveChanges();

            return Ok(dbMotorbike);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteCar(int id)
        {
            var dbMotorbike = _context.Motorbikes.FirstOrDefault(x => x.Id == id);
            if (dbMotorbike == null)
            {
                return NotFound($"Motorbike with id: {id} isn't found.");
            }
            _context.Remove(dbMotorbike);
            _context.SaveChanges();
            return Ok($"Motorbike with id: {id} was successfuly deleted");
        }
    }
}
