using Settings;
using Domain;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace Services;

public class CustomerCollectionService
{
    private readonly IMongoCollection<Customer> _customerCollection;

    public CustomerCollectionService(IOptions<HotelDatabaseSettings> hotelSettings)
    {
        var mongoClient = new MongoClient(hotelSettings.Value.ConnectionString);
        var mongoDatabase = new MongoClient().GetDatabase(hotelSettings.Value.DatabaseName);
        _customerCollection = mongoDatabase.GetCollection<Customer>(
            hotelSettings.Value.CustomerCollectionName
        );
    }

    public async Task<List<Customer>> GetCustomersAsync() =>
        await _customerCollection.Find(_ => true).ToListAsync();

    public async Task<Customer?> GetCustomer(int id) =>
        await _customerCollection.Find(customer => customer.Id == id).FirstOrDefaultAsync();

    public async Task CreateCustomer(Customer customer) =>
        await _customerCollection.InsertOneAsync(customer);

    public async Task RemoveCustomer(int id) =>
        await _customerCollection.DeleteOneAsync(customer => customer.Id == id);

    public async Task UpdateCustomer(int id, Customer customer) =>
        await _customerCollection.ReplaceOneAsync(customer => customer.Id == id, customer);
}
