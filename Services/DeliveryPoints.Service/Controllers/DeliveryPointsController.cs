using DeliveryPoints.Service.Dtos;
using DeliveryPoints.Service.Models;
using DeliveryPoints.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryPoints.Service.Controllers
{
    [ApiController]
    [Route("deliveryPoints")]
    public class DeliveryPointsController : ControllerBase
    {
        DeliveryPointsService deliveryPointsService;
        ILogger<DeliveryPointsController> logger;
        readonly ErrorMessage generalErrorMessage =
            new ErrorMessage { Message = "Ha ocurrido un error intentelo de nuevo más tarde" };

        public DeliveryPointsController(DeliveryPointsService deliveryPointsService, ILogger<DeliveryPointsController> logger)
        {
            this.deliveryPointsService = deliveryPointsService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<DeliveryPoint>>> GetAll()
        {
            try { 
                return await deliveryPointsService.GetAll();
            }catch(Exception e)
            {
                logger.LogCritical($"Crital error on Getting All Delivery Points exception: {e}");

                return BadRequest(generalErrorMessage);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryPoint>> GetById(string id)
        {
            try
            {
                return Ok(await deliveryPointsService.GetById(id));
            }
            catch (InvalidOperationException)
            {
                return NotFound(value: new { message = $"DeliveryPoint with id {id} not found" });
            }
            catch (Exception e)
            {
                logger.LogCritical($"Crital error on Getting Delivery Point with id {id} exception: {e}");

                return BadRequest(generalErrorMessage);
            }

        }

        [HttpPost]
        public async Task<ActionResult<DeliveryPoint>> Create(CreateDeliveryPointDto DeliveryPoint)
        {
            try
            {
                DeliveryPoint createdDeliveryPoint = await deliveryPointsService.Create(DeliveryPoint);

                return CreatedAtAction(nameof(GetById), new { id = createdDeliveryPoint.Id }, createdDeliveryPoint);
            }
            catch (Exception e)
            {
                logger.LogCritical($"Crital error on Create Delivery Point exception: {e}");

                return BadRequest(generalErrorMessage);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DeliveryPoint>> Update(
            string id,
            UpdateDeliveryPointDto DeliveryPoint)
        {
            try
            {
                return await deliveryPointsService.Update(id, DeliveryPoint);
            }
            catch (Exception e)
            {
                logger.LogCritical($"Crital error on Update Delivery Point with id {id} exception: {e}");

                return BadRequest(generalErrorMessage);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeliveryPoint>> Delete(string id)
        {
            try
            {
                return Ok(await deliveryPointsService.Delete(id));
            }
            catch (Exception e)
            {
                logger.LogCritical($"Crital error on Delete Delivery Point with id {id} exception: {e}");

                return BadRequest(generalErrorMessage);
            }
        }
    }
}
