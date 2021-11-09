using CongestionCharge.DTOs;
using CongestionCharge.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace CongestionCharge.Implementations
{
    public class ReceiptPrinter : IReceiptPrinter
    {
        private readonly IRoundingLogic _roundingLogic;
        private readonly IReceiptFormatter _receiptFormatter;

        public ReceiptPrinter(IReceiptFormatter receiptFormatter, IRoundingLogic roundingLogic)
        {
            _receiptFormatter = receiptFormatter;
            _roundingLogic = roundingLogic;
        }

        public string Print(IEnumerable<ChargeEvent> chargeEvents)
        {
            var totalAmount = 0.00m;
            var stringBuilder = new StringBuilder();

            foreach (var chargeEvent in chargeEvents)
            {
                var amount = _roundingLogic.Round(chargeEvent.Amount);

                stringBuilder.Append($"Charge for {_receiptFormatter.Format(chargeEvent.Duration)} ");
                stringBuilder.Append($"({chargeEvent.Name} rate): ");
                stringBuilder.AppendLine(_receiptFormatter.Format(amount));

                totalAmount += amount;
            }

            stringBuilder.AppendLine($"Total Charge: {_receiptFormatter.Format(totalAmount)}");
            return stringBuilder.ToString();
        }
    }
}