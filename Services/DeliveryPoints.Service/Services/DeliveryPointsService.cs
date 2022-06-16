using AutoMapper;
using DeliveryPoints.Service.Dtos;
using DeliveryPoints.Service.Models;
using MongoDB.Driver;

namespace DeliveryPoints.Service.Services
{
    public class DeliveryPointsService
    {
        IMongoCollection<DeliveryPoint> deliveryPointsCollection;
        IMapper mapper;

        public DeliveryPointsService(
            IMongoCollection<DeliveryPoint> deliveryPointsCollection,
            IMapper mapper)
        {
            this.deliveryPointsCollection = deliveryPointsCollection;
            this.mapper = mapper;
        }

        public async Task<List<DeliveryPoint>> GetAll()
        {
            var deliveryPoints = await deliveryPointsCollection.FindAsync(point => true);
            
            return deliveryPoints.ToList();
        }

        public async Task<DeliveryPoint> GetById(string id)
        {
            var client = await deliveryPointsCollection.FindAsync(filter: delivery => delivery.Id == id);

            return client.First();
        }

        public async Task<DeliveryPoint> Create(CreateDeliveryPointDto DeliveryPoint)
        {
            DeliveryPoint newDeliveryPoint = mapper.Map<CreateDeliveryPointDto, DeliveryPoint>(DeliveryPoint);
            await deliveryPointsCollection.InsertOneAsync(document: newDeliveryPoint);

            return newDeliveryPoint;
        }

        public async Task<DeliveryPoint> Update(
            string id,
            UpdateDeliveryPointDto updatedDeliveryPoint)
        {
            return await deliveryPointsCollection.FindOneAndUpdateAsync(
                filter: Builders<DeliveryPoint>.Filter.Where(rec => rec.Id == id),
                update: Builders<DeliveryPoint>.Update
                    .Set(rec => rec.UpdatedAt, DateTime.Now)
                    .Set(rec => rec.Name, updatedDeliveryPoint.Name),
                options: new FindOneAndUpdateOptions<DeliveryPoint>()
                {
                    ReturnDocument = ReturnDocument.After,
                }
                );
        }

        public async Task<DeliveryPoint> Delete(string id)
        {
            return await deliveryPointsCollection.FindOneAndDeleteAsync(
                filter: DeliveryPoint => DeliveryPoint.Id == id
                );
        }
    } 
}
