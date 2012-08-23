using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using CDO;

namespace CDOTest
{
    [TestFixture]
    class CloudeoServiceTest : AbstractCloudeoServiceTest
    {

        [Test]
        public void testGetVersion()
        {
            Responder<string> responder = createStringResponder(); 
            _service.getVersion(responder);
            Assert.IsTrue(awaitStringResult().Length > 4);
        }

    }
}