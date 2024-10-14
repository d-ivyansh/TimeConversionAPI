using Microsoft.AspNetCore.Mvc;

namespace app_service_task.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeConversionController : ControllerBase
    {
        [HttpGet("convert")]
        public IActionResult ConvertTime(string istTime)
        {
            try
            {
                // Parse the input IST time
                DateTime istDateTime = DateTime.Parse(istTime);

                // Define the IST timezone
                TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

                // Convert input time to UTC
                DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(istDateTime, istTimeZone);

                // Define AEDT timezone
                TimeZoneInfo aedtTimeZone = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time"); // AEDT

                // Define EST timezone
                TimeZoneInfo estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"); // EST

                // Convert UTC to AEDT
                DateTime aedtDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, aedtTimeZone);

                // Convert UTC to EST
                DateTime estDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, estTimeZone);

                // Prepare response
                var response = new
                {
                    IST = istDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    UTC = utcDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    AEDT = aedtDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    EST = estDateTime.ToString("yyyy-MM-dd HH:mm:ss")
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
