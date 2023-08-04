using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reto.API.Models.Request.Account;
using Reto.API.Models.Response.Account;
using Reto.Application.Features.Account.Commands;
using Reto.Application.Features.Account.Commands.Delete;
using Reto.Application.Features.Account.Queries.GetAll;
using Reto.Application.Features.Account.Queries.GetById;
using Reto.Application.Models;

namespace Reto.API.Controllers
{
    [Route("api/cuentas")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public AccountController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountRequest request)
        {
            AddUpdateAccountCommand addUpdateAccountCommand = _mapper.Map<AddUpdateAccountCommand>(request);

            GenericResponse<AddUpdateAccountCommandResponse> addUpdateAccountCommandResponse =
                await _sender.Send(addUpdateAccountCommand);

            GenericResponse<AccountResponse> response = _mapper.Map<GenericResponse<AccountResponse>>(addUpdateAccountCommandResponse);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAccountRequest request)
        {
            AddUpdateAccountCommand addUpdateAccountCommand = _mapper.Map<AddUpdateAccountCommand>(request);
            addUpdateAccountCommand.AccountId = id;

            GenericResponse<AddUpdateAccountCommandResponse> addUpdateAccountCommandResponse =
                await _sender.Send(addUpdateAccountCommand);

            GenericResponse<AccountResponse> response = _mapper.Map<GenericResponse<AccountResponse>>(addUpdateAccountCommandResponse);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            GenericResponse<GetAccountQueryResponse> getAccountCommandResponse =
                await _sender.Send(new GetAccountQuery { AccountId = id });

            GenericResponse<AccountResponse> response = _mapper.Map<GenericResponse<AccountResponse>>(getAccountCommandResponse);

            return Ok(response);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {

            GenericResponse<List<GetAccountQueryResponse>> getAllAccountsResponse =
                await _sender.Send(new GetAllAccountsQuery());


            GenericResponse<List<AccountResponse>> response = _mapper.Map<GenericResponse<List<AccountResponse>>>(getAllAccountsResponse);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            GenericResponse<object> deleteAccountResponse =
                await _sender.Send(new DeleteAccountCommand
                {
                    AccountId = id
                });


            GenericResponse<object> response = _mapper.Map<GenericResponse<object>>(deleteAccountResponse);

            return Ok(response);
        }

    }
}
