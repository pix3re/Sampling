namespace SamplingTests
{
    [TestClass]
    public class UnitTest1
    {
        private const string pExpected = "Hello world!";

        [TestMethod]
        public void TestMethod1()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                Sampling.Program.Main();

                var vResult = sw.ToString().Trim();
                Assert.AreEqual(pExpected, vResult);
            }
        }
    }
}