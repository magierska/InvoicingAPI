using AutoMapper;

namespace InvoicingAPI.Profiles;

public class InvoicesProfile : Profile
{
    public InvoicesProfile()
    {
        CreateMap<Commands.CreateInvoiceCommand, Domain.Entities.Invoices.Invoice>()
            .ForMember(x => x.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(x => x.Date, opt => opt.MapFrom(_ => DateTimeOffset.UtcNow))
            .ForMember(x => x.State, opt => opt.MapFrom(_ => Domain.Entities.Invoices.InvoiceState.Draft));
        CreateMap<Contracts.Invoices.InvoiceLine, Domain.Entities.Invoices.InvoiceLine>();
    }
}
