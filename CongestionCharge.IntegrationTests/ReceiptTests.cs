using CongestionCharge.Implementations;
using System;
using System.Text;
using Xunit;
using CongestionCharge.DTOs;
using System.Threading.Tasks;
using CongestionCharge.Interfaces;
using CongestionCharge.Core;
using CongestionCharge.Helpers;
using System.Linq;

namespace CongestionCharge.IntegrationTests
{
    public class ReceiptTests : IAsyncLifetime
    {
        private readonly ITimeZoneLogic _timeZoneLogic;
        private readonly IChargeEventAggregator _chargeEventAggregator;
        private readonly IReceiptPrinter _receiptPrinter;
        private Config _config;

        public ReceiptTests()
        {
            _timeZoneLogic = new TimeZoneLogic();
            _chargeEventAggregator = new ChargeEventAggregator();

            IReceiptFormatter receiptFormatter = new ReceiptFormatter();
            IRoundingLogic roundingLogic = new RoundingLogic();
            _receiptPrinter = new ReceiptPrinter(receiptFormatter, roundingLogic);
        }

        public async Task InitializeAsync()
        {
            _config = await ConfigReader.ReadAsync("../../../../config.json");
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        [Fact]
        public void Test1()
        {
            // Arrange
            var car = GetVehicle("Car");
            var chargeFinder = new ChargeFinder(car.Charges);
            var chargeEventBuilder = new ChargeEventBuilder(chargeFinder, _timeZoneLogic);

            // Act
            var chargeEvents = chargeEventBuilder.Build(
                from: new DateTime(2008, 04, 24, 11, 32, 00),
                to: new DateTime(2008, 04, 24, 14, 42, 00));

            var aggregatedChargeEvents = _chargeEventAggregator.Aggregate(chargeEvents);
            var receipt = _receiptPrinter.Print(aggregatedChargeEvents);

            // Assert
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Charge for 0h 28m (AM rate): £0.90");
            stringBuilder.AppendLine("Charge for 2h 42m (PM rate): £6.70");
            stringBuilder.AppendLine("Total Charge: £7.60");

            Assert.Equal(stringBuilder.ToString(), receipt);
        }

        [Fact]
        public void Test2()
        {
            // Arrange
            var motorbike = GetVehicle("Motorbike");
            var chargeFinder = new ChargeFinder(motorbike.Charges);
            var chargeEventBuilder = new ChargeEventBuilder(chargeFinder, _timeZoneLogic);

            // Act
            var chargeEvents = chargeEventBuilder.Build(
                from: new DateTime(2008, 04, 24, 17, 00, 00),
                to: new DateTime(2008, 04, 24, 22, 11, 00));

            var aggregatedChargeEvents = _chargeEventAggregator.Aggregate(chargeEvents);
            var receipt = _receiptPrinter.Print(aggregatedChargeEvents);

            // Assert
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Charge for 0h 0m (AM rate): £0.00");
            stringBuilder.AppendLine("Charge for 2h 0m (PM rate): £2.00");
            stringBuilder.AppendLine("Total Charge: £2.00");

            Assert.Equal(stringBuilder.ToString(), receipt);
        }

        [Fact]
        public void Test3()
        {
            // Arrange
            var van = GetVehicle("Van");
            var chargeFinder = new ChargeFinder(van.Charges);
            var chargeEventBuilder = new ChargeEventBuilder(chargeFinder, _timeZoneLogic);

            // Act
            var chargeEvents = chargeEventBuilder.Build(
                from: new DateTime(2008, 04, 25, 10, 23, 00),
                to: new DateTime(2008, 04, 28, 09, 02, 00));

            var aggregatedChargeEvents = _chargeEventAggregator.Aggregate(chargeEvents);
            var receipt = _receiptPrinter.Print(aggregatedChargeEvents);

            // Assert
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Charge for 3h 39m (AM rate): £7.30");
            stringBuilder.AppendLine("Charge for 7h 0m (PM rate): £17.50");
            stringBuilder.AppendLine("Total Charge: £24.80");

            Assert.Equal(stringBuilder.ToString(), receipt);
        }

        private Vehicle GetVehicle(string type)
        {
            return _config.Vehicles.First(vehicle => vehicle.Type == type);
        }
    }
}
