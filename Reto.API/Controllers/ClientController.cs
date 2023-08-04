using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reto.API.Models;
using Reto.Application.Features.Client.Commands;
using Reto.Application.Features.Client.Queries;
using Reto.Application.Features.Client.Queries.GetAll;
using Reto.Application.Features.Client.Queries.GetById;
using Reto.Application.Models;

namespace Reto.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ISender _sender;

        public ClientController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClientRequest request)
        {
            AddUpdateClientCommand addUpdateClientCommand = new()
            {
                Address = request.Address,
                Age = request.Age,
                Gender = request.Gender,
                Identification = request.Identification,
                Name = request.Name,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
                Status = true
            };

            var addUpdateClientCommandResponse = await _sender.Send(addUpdateClientCommand);

            return Ok(addUpdateClientCommandResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Create(int id, UpdateClientRequest request)
        {
            AddUpdateClientCommand addUpdateClientCommand = new()
            {
                Address = request.Address,
                Age = request.Age,
                Gender = request.Gender,
                Identification = request.Identification,
                Name = request.Name,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
                Status = request.Status,
                ClientId = id
            };

            GenericResponse<AddUpdateClientCommandResponse> addUpdateClientCommandResponse = await _sender.Send(addUpdateClientCommand);

            return Ok(addUpdateClientCommandResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            GenericResponse<GetClientQueryResponse> getClientCommandResponse =
                await _sender.Send(new GetClientQuery { Id = id });

            return Ok(getClientCommandResponse);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {

            GenericResponse<List<GetClientQueryResponse>> getAllClientsResponse =
                await _sender.Send(new GetAllClientsQuery());

            return Ok(getAllClientsResponse);
        }
    }
}
