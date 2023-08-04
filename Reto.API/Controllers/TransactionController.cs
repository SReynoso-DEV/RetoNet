using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reto.API.Filters;
using Reto.API.Models.Request.Transaction;
using Reto.API.Models.Response.Transaction;
using Reto.Application.Features.Transaction.Commands;
using Reto.Application.Models;

namespace Reto.API.Controllers
{
    [Route("api/movimientos")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public TransactionController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransactionRequest request)
        {
            AddTransactionCommand addTransactionCommand = _mapper.Map<AddTransactionCommand>(request);
            GenericResponse<AddTransactionCommandResponse> addTransactionCommandResponse =
                await _sender.Send(addTransactionCommand);

            GenericResponse<CreateTransactionResponse> response = 
                _mapper.Map<GenericResponse<CreateTransactionResponse>>(addTransactionCommandResponse);

            return Ok(response);
        }
    }
}
