using AutoMapper;
using InvoicingAPI.CosmosDb.Models;

namespace InvoicingAPI.CosmosDb.Profiles;

public class CustomersProfile : Profile
{
    public CustomersProfile()
    {
        CreateMap<Domain.Entities.Customers.Customer, Models.Customers.Customer>()
            .ForMember(x => x.CustomerId, opt => opt.MapFrom(o => o.Id))
            .ForMember(x => x.EntityType, opt => opt.MapFrom(_ => EntityType.Customer));
    }
}
