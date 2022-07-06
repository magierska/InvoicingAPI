using InvoicingAPI.Commands;
using InvoicingAPI.Contracts.Invoices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvoicingAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class InvoicesController : ControllerBase
{
    private readonly IMediator mediator;

    public InvoicesController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<CreateInvoiceResponse>> CreateInvoiceAsync([FromBody] CreateInvoiceRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateInvoiceCommand(request.CustomerId!.Value, request.Description, request.Lines);
        var id = await mediator.Send(command, cancellationToken);

        return Ok(new CreateInvoiceResponse(id));
    }

    [HttpPatch("{invoiceId}/state")]
    public async Task<IActionResult> UpdateInvoiceStateAsync(Guid invoiceId, [FromBody] UpdateInvoiceStateRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateInvoiceStateCommand(invoiceId, request.CustomerId!.Value, request.State!.Value);
        await mediator.Send(command, cancellationToken);

        return Ok();
    }
}
