using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MosaicResidentInformationApi.V1.Controllers;
using MosaicResidentInformationApi.V1.UseCase;
using NUnit.Framework;

namespace MosaicResidentInformationApi.Tests.V1.Controllers
{

    [TestFixture]
    public class HealthCheckControllerTests
    {
        private HealthCheckController _classUnderTest;


        [SetUp]
        public void SetUp()
        {
            _classUnderTest = new HealthCheckController();
        }

        [Test]
        public void ReturnsResponseWithStatus()
        {
            var response = _classUnderTest.HealthCheck() as OkObjectResult;

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(200);
            response.Value.Should().BeEquivalentTo(new Dictionary<string, object> { { "success", true } });

        }

        [Test]
        public void ThrowErrorThrows()
        {
            var testDelegate = new Action(_classUnderTest.ThrowError);
            testDelegate.Should().Throw<TestOpsErrorException>();
        }
    }
}
