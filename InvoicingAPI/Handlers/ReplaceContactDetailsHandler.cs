using AutoMapper;
using InvoicingAPI.Commands;
using InvoicingAPI.Domain.Abstractions;
using InvoicingAPI.Domain.Entities.Customers;
using MediatR;

namespace InvoicingAPI.Handlers;

public class ReplaceContactDetailsHandler : IRequestHandler<ReplaceContactDetailsCommand>
{
    private readonly IMapper mapper;
    private readonly IInvoicingDbRepository repository;

    public ReplaceContactDetailsHandler(IMapper mapper, IInvoicingDbRepository repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<Unit> Handle(ReplaceContactDetailsCommand request, CancellationToken cancellationToken = default)
    {
        var contactDetails = mapper.Map<IEnumerable<ContactDetail>>(request.ContactDetails);

        await repository.ReplaceContactDetailsAsync(request.CustomerId, contactDetails, cancellationToken);

        return Unit.Value;
    }
}
