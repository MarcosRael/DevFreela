using DevFreela.Core.DTO;
using DevFreela.Core.Services;
using System.Threading.Tasks;

namespace DevFreela.Infrastruture.Payments
{
    public class PaymentService : IPaymentService
    {
        public Task<bool> ProcessPayment(PaymentInfoDTO paymentInfoDTO)
        {
            throw new NotImplementedException();
        }
    }
}
