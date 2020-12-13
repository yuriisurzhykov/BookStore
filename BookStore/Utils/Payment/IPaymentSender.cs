using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Utils.Payment
{
    interface IPaymentSender
    {
        string TrySendReceipt(EmailContext context, bool isModelValid);
    }

    interface IPaymentSendCallback
    {
        void OnSuccessful(EmailContext context, string message);
        void OnFailed(string message);
    }
}
