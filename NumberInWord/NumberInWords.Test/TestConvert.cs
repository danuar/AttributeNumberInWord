using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NumberInWords.Test
{
    [TestClass]
    public class TestConvert
    {
        [TestMethod]
        public void Test2()
        {
            const int n = 1402368;
            var s = RusNumber.Str(n).Trim();
            Assert.IsTrue(s.Contains("четыреста"));
            const double d = 1402368.5;
            var c = d.ToString("C");
            var a = d.ToString("N");
            Assert.AreEqual(s, "Один миллион четыреста две тысячи триста шестьдесят восемь");
            Assert.AreEqual(c, "1 402 368,50 ₽");
            Assert.AreEqual(a, "1 402 368,50");
        }
    }
}
