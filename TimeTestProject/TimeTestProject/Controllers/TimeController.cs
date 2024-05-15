using Microsoft.AspNetCore.Mvc;

namespace TimeTestProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCurrentTime(string? timezone = null)
        {
            DateTime utcTime = DateTime.UtcNow;
            string currentTimeUtc = utcTime.ToString("yyyy-MM-ddTHH:mm:ssZ");

            if (!string.IsNullOrEmpty(timezone))
            {
                try
                {
                    TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                    DateTime adjustedTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZoneInfo);
                    string adjustedTimeStr = adjustedTime.ToString("yyyy-MM-ddTHH:mm:ss");
                    return Ok(new { currentTimeUtc, adjustedTime = adjustedTimeStr });
                }
                catch (TimeZoneNotFoundException)
                {
                    return BadRequest("Invalid timezone provided.");
                }
                catch (InvalidTimeZoneException)
                {
                    return BadRequest("Invalid timezone provided.");
                }
                catch (Exception)
                {
                    return StatusCode(500, "Internal server error.");
                }
            }

            return Ok(new { currentTimeUtc });
        }
    }
}
