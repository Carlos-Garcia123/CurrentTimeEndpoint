using Microsoft.AspNetCore.Mvc;
using TimeTestProject.Controllers;

namespace TimeProjectUnitTest
{
    public class Tests
    {
        public class TimeControllerTests
        {
            private TimeController _controller;

            [SetUp]
            public void Setup()
            {
                _controller = new TimeController();
            }

            [Test]
            public void GetTime_ReturnsNotNull()
            {
                var result = _controller.GetCurrentTime();
                Assert.IsNotNull(result);
            }

            [Test]
            public void GetTime_ReturnsDateTime()
            {
                var result = _controller.GetCurrentTime() as OkObjectResult;
                // Assert
                Assert.IsNotNull(result);
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.IsNotNull(result.Value);
            }

            [Test]
            public void GetTime_ReturnsCurrentTimeWithTimeZone()
            {
                var result = _controller.GetCurrentTime("Europe/London") as OkObjectResult;
                // Assert
                Assert.IsNotNull(result);
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.IsNotNull(result.Value);
            }

            [Test]
            public void GetTime_ReturnsCurrentTimeWithFakeTimeZone()
            {
                string invalidTimezone = "Invalid Timezone";
                var result = _controller.GetCurrentTime(invalidTimezone) as BadRequestObjectResult;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(400, result.StatusCode);
                Assert.AreEqual("Invalid timezone provided.", result.Value);
            }
        }
    }
}