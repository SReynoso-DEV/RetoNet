using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reto.API.Models.Response.Report;
using Reto.Application.Features.Report.Query;
using Reto.Application.Models;

namespace Reto.API.Controllers
{
    [Route("api/reportes")]
    [ApiController]
    public class ReportController : ControllerBase
    {

        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public ReportController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> PerDates(int clientId, DateTime startDate, DateTime endDate)
        {

            GenericResponse<List<PerDatesQueryResponse>> perDateQueryResponse =
                await _sender.Send(new PerDatesQuery
                {
                    ClientId = clientId,
                    StartDate = startDate,
                    EndDate = endDate
                });

            GenericResponse<List<PerDatesResponse>> response = _mapper.Map<GenericResponse<List<PerDatesResponse>>>(perDateQueryResponse);

            return Ok(response);
        }

    }
}
