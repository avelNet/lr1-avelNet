using Backend.Data;
using Backend.Models;
using Backend.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using static Backend.Models.TConnectDialog;

namespace Backend.Controllers
{
    [ApiController]
    [Route("mail")]
    public class MailController : ControllerBase
    {
        private readonly ILogger<MailController> _logger;
        private readonly AppDbContext _dbContext;

        public MailController(ILogger<MailController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _dbContext = appDbContext;
        }

        [HttpPost("Send")]
        public IActionResult Send([FromBody] TConnectDialog dialog)
        {
            MailEntity mailEn = new MailEntity
            {
                RequiredTask = dialog.RequiredTask != null ? string.Join(", ", dialog.RequiredTask) : null,
                Phone = dialog.Phone,
                Name = dialog.Name
            };
            _dbContext.Mails.Add(mailEn);
            _dbContext.SaveChanges();

            _logger.LogInformation("Received dialog: {@Dialog}", dialog);
            return Ok();
        }
    }
}
