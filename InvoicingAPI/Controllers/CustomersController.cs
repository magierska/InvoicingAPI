using InvoicingAPI.Commands;
using InvoicingAPI.Contracts.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvoicingAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly IMediator mediator;

    public CustomersController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<CreateCustomerResponse>> CreateCustomerAsync([FromBody] CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCustomerCommand(request.FirstName, request.LastName, request.Address, request.ContactDetails);
        var id = await mediator.Send(command, cancellationToken);

        return Ok(new CreateCustomerResponse(id));
    }

    [HttpPatch("{customerId}/contactDetails")]
    public async Task<IActionResult> ReplaceContactDetailsAsync(Guid customerId, [FromBody] ReplaceContactDetailsRequest request, CancellationToken cancellationToken)
    {
        var command = new ReplaceContactDetailsCommand(customerId, request.ContactDetails);
        await mediator.Send(command, cancellationToken);

        return Ok();
    }
}
