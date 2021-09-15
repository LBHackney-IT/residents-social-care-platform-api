using ResidentsSocialCarePlatformApi.V1.UseCase;
using NUnit.Framework;

namespace ResidentsSocialCarePlatformApi.Tests.V1.UseCase
{
    [TestFixture]
    public class ThrowOpsErrorUseCaseTests
    {
        [Test]
        public void ThrowsTestOpsErrorException()
        {
            var ex = Assert.Throws<TestOpsErrorException>(ThrowOpsErrorUseCase.Execute);

            Assert.That(ex.Message, Is.EqualTo("This is a test exception to test our integrations"));
        }
    }
}
