using InvoicingAPI.Commands;
using InvoicingAPI.Domain.Abstractions;
using InvoicingAPI.Domain.Entities.Invoices;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace InvoicingAPI.Handlers;

public class UpdateInvoiceStateHandler : IRequestHandler<UpdateInvoiceStateCommand>
{
    private readonly IInvoicingDbRepository repository;

    public UpdateInvoiceStateHandler(IInvoicingDbRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Unit> Handle(UpdateInvoiceStateCommand request, CancellationToken cancellationToken = default)
    {
        var invoice = await repository.GetInvoiceAsync(request.InvoiceId, request.CustomerId, cancellationToken);
        ValidateStateChange(invoice.State, request.State);

        await repository.UpdateStateAsync(request.InvoiceId, request.CustomerId, request.State, cancellationToken);

        return Unit.Value;
    }

    private static void ValidateStateChange(InvoiceState previousState, InvoiceState newState)
    {
        switch ((previousState, newState))
        {
            case (InvoiceState.Draft, InvoiceState.Issued):
            case (InvoiceState.Draft, InvoiceState.Deleted):
            case (InvoiceState.Issued, InvoiceState.Paid):
            case (InvoiceState.Issued, InvoiceState.Cancelled):
                return;
            default:
                throw new ValidationException($"Invoice state cannot be changed from {previousState} to {newState}.");
        }
    }
}
