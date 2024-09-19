using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsInfoAPI.Models;

namespace StudentsInfoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIControllerController : ControllerBase
    {
        protected readonly StudentDbContext _context;
        public StudentAPIControllerController(StudentDbContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<ActionResult<Student>>  GetAllStudents()
        {
            List<Student> students = await _context.Students.ToListAsync();
            return Ok(students);
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<Student>> GetStudentByID(int ID)
        {
            Student std = await _context.Students.FirstOrDefaultAsync(x => x.ID == ID);
            if (std != null) return Ok(std);
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student std)
        {
            await _context.Students.AddAsync(std);
            await _context.SaveChangesAsync();
            return Ok(std);
        }


        [HttpPut("{ID}")]
        public async Task<ActionResult<Student>> UpdateStudent(int ID, Student std)
        {
            if (ID != std.ID) return BadRequest();
            _context.Entry(std).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(std);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult<Student>> DeleteStudent(int ID)
        {
            Student std = await _context.Students.FindAsync(ID);
            if (std == null) return NotFound();

            _context.Students.Remove(std);
            await _context.SaveChangesAsync();
            return Ok(std);
        }
    }
}
