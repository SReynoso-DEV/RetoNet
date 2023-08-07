using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reto.API.Models.Request.Client;
using Reto.API.Models.Response.Client;
using Reto.Application.Features.Client.Commands;
using Reto.Application.Features.Client.Commands.Delete;
using Reto.Application.Features.Client.Queries;
using Reto.Application.Features.Client.Queries.GetAll;
using Reto.Application.Features.Client.Queries.GetById;
using Reto.Application.Models;

namespace Reto.API
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public ClientController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClientRequest request)
        {
            AddUpdateClientCommand addUpdateClientCommand = _mapper.Map<AddUpdateClientCommand>(request);

            GenericResponse<ClientCommandQueryResponse> addUpdateClientCommandResponse = 
                await _sender.Send(addUpdateClientCommand);

            GenericResponse<ClientResponse> response = _mapper.Map<GenericResponse<ClientResponse>>(addUpdateClientCommandResponse);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateClientRequest request)
        {
            AddUpdateClientCommand addUpdateClientCommand = _mapper.Map<AddUpdateClientCommand>(request);
            addUpdateClientCommand.ClientId = id;

            GenericResponse<ClientCommandQueryResponse> addUpdateClientCommandResponse = await _sender.Send(addUpdateClientCommand);

            GenericResponse<ClientResponse> response = _mapper.Map<GenericResponse<ClientResponse>>(addUpdateClientCommandResponse);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            GenericResponse<GetClientQueryResponse> getClientCommandResponse =
                await _sender.Send(new GetClientQuery { Id = id });

            GenericResponse<ClientResponse> response = _mapper.Map<GenericResponse<ClientResponse>>(getClientCommandResponse);

            return Ok(response);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {

            GenericResponse<List<GetClientQueryResponse>> getAllClientsResponse =
                await _sender.Send(new GetAllClientsQuery());


            GenericResponse<List<ClientResponse>> response = _mapper.Map<GenericResponse<List<ClientResponse>>>(getAllClientsResponse);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            GenericResponse<object> deleteClientResponse =
                await _sender.Send(new DeleteClientCommand
                {
                    ClientId = id
                });


            GenericResponse<object> response = _mapper.Map<GenericResponse<object>>(deleteClientResponse);

            return Ok(response);
        }
    }
}
