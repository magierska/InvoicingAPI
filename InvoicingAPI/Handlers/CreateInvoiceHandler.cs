using AutoMapper;
using InvoicingAPI.Commands;
using InvoicingAPI.Domain.Abstractions;
using InvoicingAPI.Domain.Entities.Invoices;
using MediatR;

namespace InvoicingAPI.Handlers;

public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceCommand, Guid>
{
    private readonly IMapper mapper;
    private readonly IInvoicingDbRepository repository;

    public CreateInvoiceHandler(IMapper mapper, IInvoicingDbRepository repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<Guid> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken = default)
    {
        var invoice = mapper.Map<Invoice>(request);

        await repository.CreateInvoiceAsync(invoice, cancellationToken);

        return invoice.Id;
    }
}
