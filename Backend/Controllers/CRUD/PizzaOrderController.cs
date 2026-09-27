using Backend.Data;
using Backend.Models.DTO;
using Backend.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.CRUD
{
    [ApiController]
    [Route("pizzaOrders")]
    public class PizzaOrderController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public PizzaOrderController(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _dbContext.PizzaOrders.ToList();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _dbContext.PizzaOrders.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult AddNewPizzaOrder([FromBody] PizzaOrderDto item)
        {
            PizzaOrderEntity pizzaOrderEntity = new PizzaOrderEntity
            {
                Size = item.Size,
                Options = item.Options,
                Thickness = item.Thickness
            };
            _dbContext.PizzaOrders.Add(pizzaOrderEntity);
            _dbContext.SaveChanges();
            return Ok(pizzaOrderEntity);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePizzaOrder([FromBody] PizzaOrderDto item, int id)
        {
            var existingItem = _dbContext.PizzaOrders.FirstOrDefault(x => x.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }
            existingItem.Size = item.Size;
            existingItem.Options = item.Options;
            existingItem.Thickness = item.Thickness;

            _dbContext.SaveChanges();
            return Ok(existingItem);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingItem = _dbContext.PizzaOrders.FirstOrDefault(x => x.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }
            _dbContext.PizzaOrders.Remove(existingItem);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
