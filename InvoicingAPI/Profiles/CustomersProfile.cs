using AutoMapper;

namespace InvoicingAPI.Profiles;

public class CustomersProfile : Profile
{
    public CustomersProfile()
    {
        CreateMap<Commands.CreateCustomerCommand, Domain.Entities.Customers.Customer>()
            .ForMember(x => x.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));
        CreateMap<Contracts.Customers.ContactDetail, Domain.Entities.Customers.ContactDetail>();
    }
}
