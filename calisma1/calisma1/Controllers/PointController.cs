using Microsoft.AspNetCore.Mvc;
using calisma1.Interfaces;
using calisma1.Models;
using System.Threading.Tasks;

namespace calisma1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]  // Route güncellemesi
    public class PointController : ControllerBase
    {
        private readonly IPointService _pointService;

        public PointController(IPointService pointService)
        {
            _pointService = pointService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _pointService.GetAllAsync();
            if (response.Success)
            {
                return Ok(response.Value);
            }
            return BadRequest(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _pointService.GetByIdAsync(id);
            if (response.Success)
            {
                return Ok(response.Value);
            }
            return NotFound(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Point point)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Model doğrulama
            }
            var response = await _pointService.CreateAsync(point);
            if (response.Success)
            {
                return CreatedAtAction(nameof(Get), new { id = response.Value.Id }, response.Value);
            }
            return BadRequest(response.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Point point)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Model doğrulama
            }
            var response = await _pointService.UpdateAsync(id, point);
            if (response.Success)
            {
                return Ok(response.Value);  // Güncellenen veriyi döndür
            }
            return BadRequest(response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _pointService.DeleteAsync(id);
            if (response.Success)
            {
                return NoContent();
            }
            return BadRequest(response.Message);
        }
    }
}