using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PocChangeTracker.Context;
using PocChangeTracker.Entities;

namespace PocChangeTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly AplicationContext _context;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(AplicationContext context, ILogger<ClientsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Clients);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Client newClient)
        {
            _context.Clients.Add(newClient);

            _logger.LogInformation($"ModifiedEntries: {_context.ChangeTracker.Entries().FirstOrDefault(e => e.State == EntityState.Added)}");

            _context.SaveChanges();

            return Ok(newClient);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Client modifiedClient)
        {
            var client = _context.Clients.FirstOrDefault(x => x.Id == id);

            if(client == null)
                return UnprocessableEntity();

            client.Update(modifiedClient);

            _context.Clients.Update(modifiedClient);

            PrintChanges();

            _context.SaveChanges();

            return Ok();
        }

        private void PrintChanges()
        {
            var entrieModified = _context.ChangeTracker.Entries().FirstOrDefault(x => x.State == EntityState.Modified);

            foreach (var prop in entrieModified.Properties)
            {
                var currentValue = prop.CurrentValue!.ToString();
                var originalValue = prop.OriginalValue!.ToString();

                _logger.LogInformation($"Property: {prop.Metadata.Name} - IsModified: {prop.IsModified} - CurrentValue: {prop.CurrentValue} - OriginalValue {prop.OriginalValue}");
            }
        }
    }
}
