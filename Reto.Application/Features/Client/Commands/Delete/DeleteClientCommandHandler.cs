using MediatR;
using Reto.Application.Models;
using Reto.Domain.Exceptions;
using Reto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Client.Commands.Delete
{
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, GenericResponse<object>>
    {
        private readonly IGenericRepository<Domain.Entities.Client> _clientRepository;

        public DeleteClientCommandHandler(IGenericRepository<Domain.Entities.Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<GenericResponse<object>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetById(request.ClientId) ?? throw new BusinessException("Cliente no encontrado");
            await _clientRepository.Delete(client.ClientId);

            return new GenericResponse<object>
            {
                Message = "Cliente eliminado exitosamente!",
                Status = "Success"
            };
        }
    }
}
