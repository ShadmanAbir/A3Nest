using A3Nest.Application.DTOs;
using A3Nest.Application.Commands.Properties;
using A3Nest.Application.Queries.Properties;

namespace A3Nest.Application.Interfaces;

public interface IPropertyService
{
    Task<PropertyDto> GetPropertyAsync(int id);
    Task<IEnumerable<PropertyDto>> GetPropertiesAsync();
    Task<IEnumerable<PropertyDto>> GetPropertiesByOwnerAsync(int ownerId);
    Task<PropertyDto> CreatePropertyAsync(CreatePropertyCommand command);
    Task<PropertyDto> UpdatePropertyAsync(UpdatePropertyCommand command);
    Task DeletePropertyAsync(int id);
    Task<IEnumerable<PropertyDto>> SearchPropertiesAsync(SearchPropertiesQuery query);
}