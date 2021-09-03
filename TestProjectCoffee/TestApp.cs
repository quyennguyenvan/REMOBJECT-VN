using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coffe_Order;

namespace TestProjectCoffee
{
    [TestClass]
    public class TestApp
    {
        private readonly Coffe_Order.Repositories.Coffee.Coffee _coffee;

        public TestApp()
        {
            _coffee = new Coffe_Order.Repositories.Coffee.Coffee();
        }

        [TestMethod]
        public void TestMethod1()
        {
            bool result = _middlewareRequestHandle.OnActionExecutionAsync
        }
    }
}
