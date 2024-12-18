using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly List<string> Summaries = new()
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };



        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Get([FromQuery] int? strategy)
        {
            if (strategy == null)
            {
                return Ok(Summaries);
            }
            if (strategy == 1)
            {
                var sortedList = Summaries.OrderBy(s => s).ToList();
                return Ok(sortedList);
            }
            if (strategy == -1)
            {
                var sortedList = Summaries.OrderByDescending(s => s).ToList();
                return Ok(sortedList);
            }
            return BadRequest("������������ �������� ��������� sortStrategy");
        }

        [HttpGet("{index}")]
        public IActionResult GetOne(int index)
        {
            if (index < 0)
            {
                return BadRequest("������ �� ����� ���� �������������");
            }
            if (index >= Summaries.Count)
            {
                return BadRequest("������ �� ��������� ������");
            }
            return Ok(Summaries[index]);
        }

        [HttpGet("find-by-name/{name}")]
        public IActionResult GetCount(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("��� �� ����� ���� ������");
            }
            return Ok(Summaries.Count(x => x == name));
        }

        [HttpPost]
        public IActionResult Add([FromBody] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("��� �� ����� ���� ������");
            }
            Summaries.Add(name);
            return Ok();
        }

        [HttpPut("{index}")]
        public IActionResult Update(int index, [FromBody] string name)
        {
            if (index < 0)
            {
                return BadRequest("������ �� ����� ���� �������������");
            }
            if (index >= Summaries.Count)
            {
                return BadRequest("����� ������ ��������!!!!");
            }
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("��� �� ����� ���� ������");
            }
            Summaries[index] = name;
            return Ok();
        }

        [HttpDelete("{index}")]
        public IActionResult Delete(int index)
        {
            if (index < 0)
            {
                return BadRequest("������ �� ����� ���� �������������");
            }
            if (index >= Summaries.Count)
            {
                return BadRequest("������ �� ��������� ������");
            }
            Summaries.RemoveAt(index);
            return Ok();
        }
    }
}