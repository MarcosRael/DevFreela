using DevFreela.Infrastruture.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using DevFreela.Core.Repositories;
using System.ComponentModel.DataAnnotations;
using DevFreela.Core.DTO;
using DevFreela.Core.Services;

namespace DevFreela.Application.Commands.FinishProject
{
    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IPaymentService _paymentservice;

        public FinishProjectCommandHandler(IProjectRepository projectRepository
                                            , IPaymentService paymentService)
        {  
            _projectRepository = projectRepository; 
            _paymentservice = paymentService;
        
        }
            
  
        public async Task<bool> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);

            project?.Finish();

            var paymentInfoDto = new PaymentInfoDTO(request.Id, request.CreditCardNumber, request.Cvv, request.ExperesAt, request.FullName);

            var result = await _paymentservice.ProcessPayment(paymentInfoDto);

            if (!result)
                project.SetPaymentPending();

            await _projectRepository.FinishAsync(project);
            
            return result;
        }
    }
}
