using Backend.Data;
using Backend.Models.DTO;
using Backend.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.CRUD
{
    [ApiController]
    [Route("mails")]
    public class MailsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public MailsController(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _dbContext.Mails.ToList();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _dbContext.Mails.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult AddNewMail([FromBody] MailDto item)
        {
            MailEntity mailEntity = new MailEntity
            {
                RequiredTask = item.RequiredTask,
                Phone = item.Phone,
                Name = item.Name
            };
            _dbContext.Mails.Add(mailEntity);
            _dbContext.SaveChanges();
            return Ok(mailEntity);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMail([FromBody] MailDto item, int id)
        {
            var mailEntity = _dbContext.Mails.FirstOrDefault(x => x.Id == id);
            if (mailEntity == null)
            {
                return NotFound();
            }

            mailEntity.RequiredTask = item.RequiredTask;
            mailEntity.Phone = item.Phone;
            mailEntity.Name = item.Name;

            _dbContext.SaveChanges();
            return Ok(mailEntity);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMail(int id)
        {
            var mailEntity = _dbContext.Mails.FirstOrDefault(x => x.Id == id);
            if (mailEntity == null)
            {
                return NotFound();
            }
            _dbContext.Mails.Remove(mailEntity);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
