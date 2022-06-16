using Clients.Service.DTOs;
using Clients.Service.Models;
using Clients.Service.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Clients.Service.Controllers
{
    [ApiController]
    [Route("clients")]
    public class ClientController : ControllerBase
    {
        ClientService clientService;
        ILogger<ClientController> logger;
        readonly ErrorMessage generalErrorMessage =
            new ErrorMessage { Message = "Ha ocurrido un error intentelo de nuevo más tarde" };
        public ClientController(ClientService clientService, ILogger<ClientController> logger)
        {
            this.clientService = clientService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> GetAll()
        {
            try
            {
                return await clientService.GetAll();
            }
            catch (Exception e)
            {
                logger.LogCritical($"Crital error on Getting All Clients exception: {e}");

                return BadRequest(generalErrorMessage);
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetById(string id)
        {
            try
            {
                return Ok(await clientService.GetById(id));
            } catch (InvalidOperationException)
            {
                return NotFound(value: new { message = $"Client with id {id} not found" });
            } catch (Exception e)
            {
                logger.LogCritical($"Crital error on Getting Client with id {id} exception: {e}");

                return BadRequest(generalErrorMessage);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Client>> Create(CreateClientDTO client)
        {
            try
            {
                Client createdClient = await clientService.Create(client);

                return CreatedAtAction(nameof(GetById), new { id = createdClient.Id }, createdClient);
            }
            catch (Exception e)
            {
                logger.LogCritical($"Crital error on Creating Client exception: {e}");

                return BadRequest(generalErrorMessage);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Client>> Update(
            string id,
            UpdateClientDTO client )
        {
            try
            {
                return await clientService.Update(id, client);
            }
            catch (Exception e)
            {
                logger.LogCritical($"Crital error on Update Client with id {id} exception: {e}");

                return BadRequest(generalErrorMessage);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Client>> Delete(string id)
        {
            try
            {
                return Ok(await clientService.Delete(id));
            }
            catch (Exception e)
            {
                logger.LogCritical($"Crital error on Delete Client with id {id} exception: {e}");

                return BadRequest(generalErrorMessage);
            }
        }
    }
}
