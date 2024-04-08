using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactAspCrud.Models;

namespace ReactAspCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentDbContext _studentDbContext;

        public StudentController(StudentDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            if (_studentDbContext == null)
            {
                return NotFound();
            }
            return await _studentDbContext.Student.ToListAsync();
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<Student>> Getstudent(int id)
        {
            if (_studentDbContext.Student == null)
            {
                return NotFound();
            }
            var brand = await _studentDbContext.Student.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return brand;
        }



        [HttpPost]
        public async Task<ActionResult<Student>> AddStudent(Student student)
        {
            _studentDbContext.Student.Add(student);
            await _studentDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStudents), new { id = student.id }, student);

        }


        [HttpPatch]
        [Route("UpdateStudent/{id}")]
        public async Task<Student> UpdateStudent(Student objStudent)
        {
            _studentDbContext.Entry(objStudent).State = EntityState.Modified;
            await _studentDbContext.SaveChangesAsync();
            return objStudent;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {

            if (_studentDbContext == null)
            {
                return NotFound();
            }
            var brand = await _studentDbContext.Student.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            _studentDbContext.Student.Remove(brand);
            await _studentDbContext.SaveChangesAsync();
            return Ok();

        }
    }

}
