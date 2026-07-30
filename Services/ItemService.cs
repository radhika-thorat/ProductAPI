using AutoMapper;
using ProductApplication.DTOs;
using ProductApplication.Interfaces;
using ProductDomain.Entities;
using ProductDomain.Exceptions;

namespace Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ItemService(
        IItemRepository itemRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _itemRepository = itemRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ItemDto>> GetByProductIdAsync(int productId)
    {
        var items = await _itemRepository.GetByProductIdAsync(productId);

        return _mapper.Map<IEnumerable<ItemDto>>(items);
    }

    public async Task<ItemDto> CreateAsync(CreateItemDto dto)
    {
        var product = await _productRepository.GetByIdAsync(dto.ProductId);

        if (product == null)
            throw new NotFoundException("Product not found.");

        var item = _mapper.Map<Item>(dto);

        await _itemRepository.AddAsync(item);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ItemDto>(item);
    }

    public async Task UpdateAsync(int id, UpdateItemDto dto)
    {
        var item = await _itemRepository.GetByIdAsync(id);

        if (item == null)
            throw new NotFoundException("Item not found.");

        item.Quantity = dto.Quantity;

        _itemRepository.Update(item);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _itemRepository.GetByIdAsync(id);

        if (item == null)
            throw new NotFoundException("Item not found.");

        _itemRepository.Delete(item);

        await _unitOfWork.SaveChangesAsync();
    }
}