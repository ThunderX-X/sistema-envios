using Microsoft.AspNetCore.Mvc;
using Shipments.Service.Dtos;
using Shipments.Service.Exceptions;
using Shipments.Service.Models;
using Shipments.Service.Services;

namespace Shipments.Service.Controllers
{
    [ApiController]
    [Route("shipments")]
    public class ShipmentsController : ControllerBase
    {
        ShippingService shippingService;
        ILogger<ShipmentsController> logger;
        readonly ErrorMessage generalErrorMessage = 
            new ErrorMessage { Message = "Ha ocurrido un error intentelo de nuevo más tarde" };

    public ShipmentsController(ShippingService shippingService, ILogger<ShipmentsController> logger)
        {
            this.shippingService = shippingService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Shipping>>> GetAll()
        {
            try { 
                return await shippingService.GetAll();
            }catch(Exception e)
            {
                logger.LogCritical($"Critical error on get all shipments exception: {e}");
                
                return BadRequest(generalErrorMessage);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Shipping>> Create(CreateShippingDto newShipping)
        {
            try
            {
                return await shippingService.Create(shipping: newShipping);
            }
            catch (ClientNotFoundException ex)
            {
                return BadRequest(new ErrorMessage{ Message = ex.Message });
            }
            catch (DeliveryPointNotFoundException ex)
            {
                return BadRequest(new ErrorMessage { Message = ex.Message });
            }
            catch (Exception e)
            {
                logger.LogCritical($"Critical error on update shipping exception: {e}");

                return BadRequest(generalErrorMessage);
            }

        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Shipping>> Update(string id, UpdateShippingDto updatedShipping)
        {
            try { 
                return await shippingService.Update(id: id, updatedShipping: updatedShipping);
            }
            catch(Exception e)
            {
                logger.LogCritical($"Critical error on update shipping with id {id} exception: {e}");

                return BadRequest(generalErrorMessage);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Shipping>> Delete(string id)
        {
            try
            {
                return await shippingService.Delete(id);
            }
            catch (Exception e)
            {
                logger.LogCritical($"Critical error on delete shipping with id {id} exception: {e}");

                return BadRequest(generalErrorMessage);
            }            
        }
    }
}
