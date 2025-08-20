using A3Nest.Application.DTOs;
using A3Nest.Application.Commands.Properties;
using A3Nest.Application.Queries.Properties;
using A3Nest.Application.Interfaces;

namespace A3Nest.Application.Services;

public class PropertyService : IPropertyService
{
    public Task<PropertyDto> GetPropertyAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PropertyDto>> GetPropertiesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PropertyDto>> GetPropertiesByOwnerAsync(int ownerId)
    {
        throw new NotImplementedException();
    }

    public Task<PropertyDto> CreatePropertyAsync(CreatePropertyCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<PropertyDto> UpdatePropertyAsync(UpdatePropertyCommand command)
    {
        throw new NotImplementedException();
    }

    public Task DeletePropertyAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PropertyDto>> SearchPropertiesAsync(SearchPropertiesQuery query)
    {
        throw new NotImplementedException();
    }
}