using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reto.API.Models.Request.Person;
using Reto.API.Models.Request.Person;
using Reto.API.Models.Response.Person;
using Reto.Application.Features.Person.Commands;
using Reto.Application.Features.Person.Commands.AddUpdate;
using Reto.Application.Features.Person.Query;
using Reto.Application.Models;

namespace Reto.API.Controllers
{
    [Route("api/personas")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public PersonController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePersonRequest request)
        {
            AddUpdatePersonCommand addUpdatePersonCommand = _mapper.Map<AddUpdatePersonCommand>(request);

            GenericResponse<PersonCommandQueryResponse> addUpdatePersonCommandResponse =
                await _sender.Send(addUpdatePersonCommand);

            GenericResponse<PersonResponse> response = _mapper.Map<GenericResponse<PersonResponse>>(addUpdatePersonCommandResponse);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePersonRequest request)
        {
            AddUpdatePersonCommand addUpdatePersonCommand = _mapper.Map<AddUpdatePersonCommand>(request);
            addUpdatePersonCommand.PersonId = id;

            GenericResponse<PersonCommandQueryResponse> addUpdatePersonCommandResponse =
                await _sender.Send(addUpdatePersonCommand);

            GenericResponse<PersonResponse> response = _mapper.Map<GenericResponse<PersonResponse>>(addUpdatePersonCommandResponse);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            GenericResponse<List<PersonCommandQueryResponse>> addUpdatePersonCommandResponse =
                await _sender.Send(new GetAllPersonsQuery());

            GenericResponse<List<PersonResponse>> response = _mapper.Map<GenericResponse<List<PersonResponse>>>(addUpdatePersonCommandResponse);

            return Ok(response);
        }
    }
}
