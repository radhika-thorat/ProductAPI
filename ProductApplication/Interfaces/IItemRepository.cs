using ProductApplication.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductDomain.Entities;

public interface IItemRepository
{
    Task<IEnumerable<Item>> GetByProductIdAsync(int productId);

    Task<Item?> GetByIdAsync(int id);

    Task AddAsync(Item item);

    void Update(Item item);

    void Delete(Item item);
}
