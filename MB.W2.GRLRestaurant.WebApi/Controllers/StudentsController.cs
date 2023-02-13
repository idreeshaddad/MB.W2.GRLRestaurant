using Microsoft.AspNetCore.Mvc;

namespace MB.W2.GRLRestaurant.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> GetStudents()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string GetStudentById(int id)
        {
            return "value";
        }

        [HttpPost]
        public void CreateStudent([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void EditStudentById(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void DeleteStudentById(int id)
        {
        }
    }
}
