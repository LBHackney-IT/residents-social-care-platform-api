using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MosaicResidentInformationApi.V1.Controllers;
using NUnit.Framework;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MosaicResidentInformationApi.Tests.V1.Controllers
{
    [TestFixture]
    public class MosaicControllerTests
    {
        private MosaicController _classUnderTest;


        [SetUp]
        public void SetUp()
        {
            _classUnderTest = new MosaicController();
        }

        [Test]
        public void ViewRecordTests()
        {
            //TODO
        }

        [Test]
        public void ListContacts()
        {
            //TODO
        }

    }
}
