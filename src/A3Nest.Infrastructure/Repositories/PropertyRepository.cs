using A3Nest.Application.Interfaces;
using A3Nest.Application.DTOs;
using A3Nest.Application.Commands.Properties;
using A3Nest.Application.Queries.Properties;

namespace A3Nest.Infrastructure.Repositories;

public class PropertyRepository : IPropertyService
{
    private readonly IUnitOfWork _unitOfWork;

    public PropertyRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PropertyDto> GetPropertyAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PropertyDto>> GetPropertiesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PropertyDto>> GetPropertiesByOwnerAsync(int ownerId)
    {
        throw new NotImplementedException();
    }

    public async Task<PropertyDto> CreatePropertyAsync(CreatePropertyCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task<PropertyDto> UpdatePropertyAsync(UpdatePropertyCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task DeletePropertyAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PropertyDto>> SearchPropertiesAsync(SearchPropertiesQuery query)
    {
        throw new NotImplementedException();
    }
}