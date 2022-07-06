using AutoMapper;
using InvoicingAPI.Commands;
using InvoicingAPI.Domain.Abstractions;
using InvoicingAPI.Domain.Entities.Customers;
using MediatR;

namespace InvoicingAPI.Handlers;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Guid>
{
    private readonly IMapper mapper;
    private readonly IInvoicingDbRepository repository;

    public CreateCustomerHandler(IMapper mapper, IInvoicingDbRepository repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken = default)
    {
        var customer = mapper.Map<Customer>(request);

        await repository.AddCustomerAsync(customer, cancellationToken);

        return customer.Id;
    }
}
