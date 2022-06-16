using AutoMapper;
using MongoDB.Driver;
using Shipments.Service.Dtos;
using Shipments.Service.Exceptions;
using Shipments.Service.Models;
using Shipments.Service.Proxies;

namespace Shipments.Service.Services
{
    public class ShippingService
    {
        IMongoCollection<Shipping> shippingCollection;
        IMapper mapper;
        ClientsProxy clientsProxy;
        DeliveryPointsProxy deliveryPointsProxy;

        public ShippingService(
            IMongoCollection<Shipping> shippingCollection,
            IMapper mapper,
            ClientsProxy clientsProxy,
            DeliveryPointsProxy deliveryPointsProxy
            )
        {
            this.shippingCollection = shippingCollection;
            this.mapper = mapper;
            this.clientsProxy = clientsProxy;
            this.deliveryPointsProxy = deliveryPointsProxy;
        }

        public async Task<List<Shipping>> GetAll()
        {
            var shipments = await shippingCollection.FindAsync(shipping => true);
            return shipments.ToList();
        }

        public async Task<Shipping> Create(CreateShippingDto shipping)
        {
            await VerifyClientExist(shipping.Client);
            Shipping newShipping = mapper.Map<CreateShippingDto, Shipping>(shipping);
            await shippingCollection.InsertOneAsync(newShipping);

            return newShipping;
        }

        public async Task<Shipping> Update(string id, UpdateShippingDto updatedShipping)
        {
            await VerifyDeliveryPointExist(updatedShipping.DeliveryPointId);

            return await shippingCollection.FindOneAndUpdateAsync(
                filter: Builders<Shipping>.Filter.Where(shipping => shipping.Id == id),
                update: Builders<Shipping>.Update
                            .Set(s => s.DeliveredAt, updatedShipping.DeliveredAt)
                            .Set(s => s.UpdatedAt, DateTime.Now)
                            .Set(s => s.DeliveryPointId, updatedShipping.DeliveryPointId)
                            .Set(s => s.VehicleId, updatedShipping.VehicleId),
                options: new FindOneAndUpdateOptions<Shipping>
                {
                    ReturnDocument = ReturnDocument.After
                });
        }

        public async Task<Shipping> Delete(string id)
        {
            return await shippingCollection.FindOneAndDeleteAsync(
                filter: Builders<Shipping>.Filter.Where(shipping => shipping.Id == id)
            );
        }

        private async Task VerifyClientExist(string id)
        {
            bool exist = await clientsProxy.ExistClientAsync(id);
            if(!exist)
                throw new ClientNotFoundException($"Cliente con {id} no ha sido encontrado");
        }

        private async Task VerifyDeliveryPointExist(string? id)
        {
            if (id == null) return;
            bool exist = await deliveryPointsProxy.ExistDeliveryPointAsync(id);
            if (!exist)
                throw new DeliveryPointNotFoundException($"Punto de entrega con {id} no encontrado");
        }
    }
}
