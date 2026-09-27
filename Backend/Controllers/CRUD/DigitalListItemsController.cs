using Backend.Data;
using Backend.Models.DTO;
using Backend.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Backend.Controllers.CRUD
{
    [ApiController]
    [Route("digitalListItems")]
    public class DigitalListItemsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public DigitalListItemsController(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _dbContext.DigitalListItems.ToList();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _dbContext.DigitalListItems.FirstOrDefault(x => x.Id == id);
            if(item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult AddNewService([FromBody] DigitalListItemsDto item)
        {
            DigitalListItemEntity digitalListItemEntity = new DigitalListItemEntity
            {
                Name = item.Name
            };
            _dbContext.DigitalListItems.Add(digitalListItemEntity);
            _dbContext.SaveChanges();
            return Ok(digitalListItemEntity);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateById([FromBody] DigitalListItemsDto item, int id)
        {
            var existingItem = _dbContext.DigitalListItems.FirstOrDefault(x => x.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }
            existingItem.Name = item.Name;
            _dbContext.SaveChanges();
            return Ok(existingItem);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            var existingItem = _dbContext.DigitalListItems.FirstOrDefault(x => x.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }
            _dbContext.DigitalListItems.Remove(existingItem);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
