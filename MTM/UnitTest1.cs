using MTM_Warehouse.Entities;
using MTM_Warehouse.Services;

namespace MTM
{
    [TestFixture]
    public class WarehouseTests
    {
        
        [Test]
        [TestCase(1000, 800, 20)]
        [TestCase(15000, 6000, 60)]
        [TestCase(30000, 27000, 10)]
        public void CheckPercentage(double totalCapacity, double spaceavailable, double percentage)
        {
            var warehouseInfo = new WarehouseInfo
            {
                W_TotalCapacity = totalCapacity,
                W_SpaceAvailable = spaceavailable,
                W_PercentFull = percentage
            };

            WarehouseService service = new WarehouseService();

            var percentFull = service.W_Percentage(warehouseInfo);

            Assert.AreEqual(percentage, percentFull);
        }

        [Test]
        [TestCase(1000, 800)]
        [TestCase(15000, 10000)]
        [TestCase(25000, 5000)]
        public void CheckSpaceAvailable(double totalCapacity, double spaceavailable)
        {
            var warehouseInfo = new WarehouseInfo
            {
                W_TotalCapacity = totalCapacity,
                W_SpaceAvailable = spaceavailable
            };

            WarehouseService service = new WarehouseService();
            var spaceAvailable = service.W_SpaceAvailabe(warehouseInfo);

            Assert.AreEqual(spaceavailable, spaceAvailable);
        }
    }
}
