using Backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Backend.Models.TConnectDialogCola;
using static Backend.Models.TConnectDialogPizza;
using static Backend.Models.TConnectDialogPolice;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models.Entities;

namespace Backend.Controllers
{
    [ApiController]
    [Route("strangeData")]
    public class strangeDataController : ControllerBase
    {
        private readonly ILogger<strangeDataController> _logger;
        private readonly AppDbContext _dbContext;

        public strangeDataController(ILogger<strangeDataController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost("SendCola")]
        public IActionResult SendCola([FromBody] TConnectDialogCola data)
        {
            ColaOrderEntity colaOrder = new ColaOrderEntity
            {
                Tasty = data.Tasty,
                Volume = data.Volume,
                Name = data.Name,
                Phone = data.Phone
            };

            _dbContext.ColaOrders.Add(colaOrder);
            _dbContext.SaveChanges();

            _logger.LogInformation("Cola order: {@Data}", data);
            return Ok();
        }

        [HttpPost("HelpPolice")]
        public IActionResult HelpPolice([FromBody] TConnectDialogPolice? data)
        {
            string? userAgent = Request.Headers["User-Agent"].ToString();
            bool isChrome = userAgent.Contains("Chrome") && !userAgent.Contains("Edg");
            if (isChrome)
            {
                return Ok(new { help = true, message = "You are using Chrome, which is allowed." });
            }
            else
            {
                return Ok(new { help = false, message = "You are not using Chrome, which is not allowed." });
            }
        }

        [HttpPost("SendPizza")]
        public IActionResult SendPizza([FromBody] TConnectDialogPizza data)
        {
            PizzaOrderEntity pizzaOrder = new PizzaOrderEntity
            {
                Size = data.Size,
                Options = data.Options != null ? string.Join(", ", data.Options) : null,
                Thickness = data.Thickness
            };
            _dbContext.PizzaOrders.Add(pizzaOrder);
            _dbContext.SaveChanges();

            _logger.LogInformation("Pizza order: {@Data}", data);
            return Ok();    
        }

        [HttpGet("GetDigitalList")]
        public IActionResult GetDigitalList([FromQuery] string? filter, [FromQuery] string? sorted)
        {
            List<string?> services = _dbContext.DigitalListItems.Select(x => x.Name).ToList();

            //Фильтрация
            List<string?> resultFiltered = services
                .Where(x => x.Contains(filter ?? string.Empty))
                .ToList();
            //Сортировка
            List<string?> resultSorted;
            if (sorted == "descending")
            {
                resultSorted = resultFiltered.OrderByDescending(x => x).ToList();
            }
            else
            {
                resultSorted = resultFiltered.OrderBy(x => x).ToList();
            }

            string html = "<ul>" +
                string.Join("", resultSorted.Select(x => $"<li>{x}</li>")) +
                "</ul>";
            return Content(html, "text/html");
        }
    }   
}
