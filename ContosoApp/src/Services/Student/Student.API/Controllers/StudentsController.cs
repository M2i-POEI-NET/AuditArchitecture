using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Student.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentContext _context;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(StudentContext StudentContext, ILogger<StudentsController> logger)
        {
            _context = StudentContext;
            _logger = logger;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Model.Entities.Student>>> GetStudents()
        {
            _logger.LogWarning("still get list of students");
            return await _context.Students.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Model.Entities.Student>> GetAgence(int id)
        {
            var Student = await _context.Students.FindAsync(id);

            if (Student == null)
            {
                return NotFound();
            }

            return Student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutStudent(Model.Entities.Student Student)
        {
            _context.Students.Update(Student);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(Student.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Model.Entities.Student>> PostStudent(Model.Entities.Student Student)
        {
            _context.Students.Add(Student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudents", new { id = Student.ID }, Student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudents(int id)
        {
            var Student = await _context.Students.FindAsync(id);
            if (Student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(Student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.ID == id);
        }
    }
}