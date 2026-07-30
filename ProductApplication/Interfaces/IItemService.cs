using ProductApplication.Interfaces;

using ProductDomain.Entities;
using ProductApplication.DTOs;

public interface IItemService
{
    Task<IEnumerable<ItemDto>> GetByProductIdAsync(int productId);

    Task<ItemDto> CreateAsync(CreateItemDto dto);

    Task UpdateAsync(int id, UpdateItemDto dto);

    Task DeleteAsync(int id);
}