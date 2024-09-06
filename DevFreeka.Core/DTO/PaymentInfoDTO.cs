using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.DTO
{
    public class PaymentInfoDTO
    {
        public PaymentInfoDTO(int id, string creditCardNumber, string cvv, string experesAt, string fullName)
        {
            Id = id;
            CreditCardNumber = creditCardNumber;
            Cvv = cvv;
            ExperesAt = experesAt;
            FullName = fullName;
        }

        public int Id { get; set; }

        public string CreditCardNumber { get; set; }

        public string Cvv { get; set; }

        public string ExperesAt { get; set; }

        public string FullName { get; set; }
    }
}
