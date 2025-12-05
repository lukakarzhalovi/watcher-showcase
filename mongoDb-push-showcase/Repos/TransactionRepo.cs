using mongoDb_push_showcase.Entities;
using MongoDB.Driver;

namespace mongoDb_push_showcase.Repos;

public class TransactionRepo(IMongoDatabase mongoDatabase)
{
    public async Task<bool> UpdateTransactions(Guid id)
    {
        try
        {
            var collection = mongoDatabase.GetCollection<Transaction>("Transactions");

            var filter = Builders<Transaction>.Filter.Eq(el => el.Id, id);
            var update = Builders<Transaction>.Update.Set(el => el.DateDeleted, DateTime.Now);

            var result = await collection.UpdateOneAsync(filter, update);
            
            return result.ModifiedCount > 0;
        }
        catch (Exception e)
        {
            throw; // TODO handle exception
        }
    }
    
}