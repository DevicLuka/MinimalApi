using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebUI.Features.Motorbikes.Models;

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
        public ActionResult<Motorbike> CreateMotorbike(MotorbikeCreateModel motorbikeModel)
        {
            var newMotorbike = new Motorbike
            {
                TeamName = motorbikeModel.TeamName,
                Speed = motorbikeModel.Speed,
                MelfunctionChance = motorbikeModel.MelfunctionChance
            };
            _context.Motorbikes.Add(newMotorbike);
            _context.SaveChanges();
            return Ok(newMotorbike);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<Motorbike> UpdateMotorbike(MotorbikeUpdateModel motorbikeModel)
        {
            var dbMotorbike = _context.Motorbikes.FirstOrDefault(x => x.Id == motorbikeModel.Id);

            if (dbMotorbike == null)
            {
                return NotFound($"Motorbike with id: {motorbikeModel.Id} isn't found.");
            }

            dbMotorbike.TeamName = motorbikeModel.TeamName;
            dbMotorbike.Speed = motorbikeModel.Speed;
            dbMotorbike.MelfunctionChance = motorbikeModel.MelfunctionChance;
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
