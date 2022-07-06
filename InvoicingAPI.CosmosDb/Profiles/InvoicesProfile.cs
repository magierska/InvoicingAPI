using AutoMapper;
using InvoicingAPI.CosmosDb.Models;

namespace InvoicingAPI.CosmosDb.Profiles;

public class InvoicesProfile : Profile
{
    public InvoicesProfile()
    {
        CreateMap<Domain.Entities.Invoices.Invoice, Models.Invoices.Invoice>()
            .ForMember(x => x.EntityType, opt => opt.MapFrom(_ => EntityType.Invoice))
            .ReverseMap();
        CreateMap<Domain.Entities.Invoices.InvoiceLine, Models.Invoices.InvoiceLine>().ReverseMap();
    }
}
