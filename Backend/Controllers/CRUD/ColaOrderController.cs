using Backend.Data;
using Backend.Models.DTO;
using Backend.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.CRUD
{
    [ApiController]
    [Route("colaOrders")]
    public class ColaOrderController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public ColaOrderController(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _dbContext.ColaOrders.ToList();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _dbContext.ColaOrders.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult AddNewColaOrder([FromBody] ColaOrderDto item)
        {
            ColaOrderEntity colaOrderEntity = new ColaOrderEntity
            {
                Tasty = item.Tasty,
                Volume = item.Volume,
                Name = item.Name,
                Phone = item.Phone
            };
            _dbContext.ColaOrders.Add(colaOrderEntity);
            _dbContext.SaveChanges();
            return Ok(colaOrderEntity);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateColaOrder([FromBody] ColaOrderDto item, int id)
        {
            var existingItem = _dbContext.ColaOrders.FirstOrDefault(x => x.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }
            existingItem.Tasty = item.Tasty;
            existingItem.Volume = item.Volume;
            existingItem.Name = item.Name;
            existingItem.Phone = item.Phone;

            _dbContext.SaveChanges();
            return Ok(existingItem);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingItem = _dbContext.ColaOrders.FirstOrDefault(x => x.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }
            _dbContext.ColaOrders.Remove(existingItem);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
