using AutoMapper;
using Clients.Service.Connection;
using Clients.Service.DTOs;
using Clients.Service.Models;
using MongoDB.Driver;
using Microsoft.AspNetCore.JsonPatch;

namespace Clients.Service.Services
{
    public class ClientService
    {
        IMongoCollection<Client> clientCollection;
        IMapper mapper;
        public ClientService(ClientConnection connection, IMapper mapper)
        {
            this.clientCollection = connection.Collection;
            this.mapper = mapper;
        }

        public async Task<List<Client>> GetAll()
        {
            var clients = await clientCollection.FindAsync(filter: client => true);

            return clients.ToList();
        }

        public async Task<Client> GetById(string id)
        {
            var client = await clientCollection.FindAsync(filter: client => client.Id == id);

            return client.First();
        }

        public async Task<Client> Create(CreateClientDTO client)
        {
            Client newClient = mapper.Map<CreateClientDTO, Client>(client);
            await clientCollection.InsertOneAsync(document: newClient);

            return newClient;
        }

        public async Task<Client> Update(string id, UpdateClientDTO updatedClient)
        {
            return await clientCollection.FindOneAndUpdateAsync(
                filter: Builders<Client>.Filter.Where(rec => rec.Id == id),
                update: Builders<Client>.Update
                    .Set(rec => rec.Email, updatedClient.Email)
                    .Set(rec => rec.Phone, updatedClient.Phone)
                    .Set(rec => rec.FirstName, updatedClient.FirstName)
                    .Set(rec => rec.LastName, updatedClient.LastName)
                    .Set(rec => rec.UpdatedAt, DateTime.Now),
                options: new FindOneAndUpdateOptions<Client>()
                {
                    ReturnDocument = ReturnDocument.After,
                }
                );;
        }

        public async Task<Client> Delete(string id)
        {
            return await clientCollection.FindOneAndDeleteAsync(filter: client => client.Id == id);
        }
    }
}
