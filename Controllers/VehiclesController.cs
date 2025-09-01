using Microsoft.AspNetCore.Mvc;
using VehicleApi.Models;

namespace VehicleApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private static readonly List<Vehicle> _vehicles = new();
        private static int _nextId = 1;

        // GET: api/vehicle?make=Toyota&year=2020
        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> GetAll([FromQuery] string? make, [FromQuery] int? year)
        {
            IEnumerable<Vehicle> query = _vehicles;

            if (!string.IsNullOrWhiteSpace(make))
                query = query.Where(v => v.Make.Equals(make, StringComparison.OrdinalIgnoreCase));

            if (year.HasValue)
                query = query.Where(v => v.Year == year.Value);

            return Ok(query);
        }

        // GET: api/vehicle/5
        [HttpGet("{id:int}")]
        public ActionResult<Vehicle> GetById(int id)
        {
            var v = _vehicles.FirstOrDefault(x => x.Id == id);
            if (v == null) return NotFound();
            return Ok(v);
        }

        // POST: api/vehicle
        [HttpPost]
        public ActionResult<Vehicle> Create([FromBody] Vehicle input)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            input.Id = _nextId++;
            _vehicles.Add(input);

            return CreatedAtAction(nameof(GetById), new { id = input.Id }, input);
        }

        // PUT: api/vehicle/5
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] Vehicle update)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var v = _vehicles.FirstOrDefault(x => x.Id == id);
            if (v == null) return NotFound();

            v.Make = update.Make;
            v.Model = update.Model;
            v.Year = update.Year;
            v.Color = update.Color;
            v.Vin = update.Vin;

            return Ok(v);
        }

        // DELETE: api/vehicle/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var v = _vehicles.FirstOrDefault(x => x.Id == id);
            if (v == null) return NotFound();

            _vehicles.Remove(v);
            return NoContent();
        }
    }
}
