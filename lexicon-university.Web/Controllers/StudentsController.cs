using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using lexicon_university.Web.Filters;

namespace lexicon_university.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly LexiconUniversityContext _context;
        private readonly IMapper mapper;

        public StudentsController(LexiconUniversityContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            //var model = _context.Student/*.AsNoTracking()*/
            //     .OrderByDescending(s => s.Id)
            //     .Select(s => new StudentIndexViewModel
            //     {
            //         Id = s.Id,
            //         Avatar = s.Avatar,
            //         FullName = s.FullName,
            //         City = s.Address.City
            //     })
            // .Take(5);

            // Accessing Shadow Property
            var res = _context.Student.Where(s => EF.Property<DateTime>(s, "Edited") >= DateTime.Now.AddDays(-1));

            var model = mapper.ProjectTo<StudentIndexViewModel>(_context.Student)
               .OrderByDescending(s => s.Id)
               .Take(5);

            return View(await model.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await mapper.ProjectTo<StudentDetailsViewModel>(_context.Student)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateIsValid] // refactor code, divide for modules
        public async Task<IActionResult> Create(StudentCreateViewModel viewModel)
        {
            // Option 3 
            var student = mapper.Map<Student>(viewModel);
            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Avatar,FirstName,LastName,Email")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Avatar");
            ModelState.Remove("FirstName");
            ModelState.Remove("LastName");
            ModelState.Remove("Email");
            ModelState.Remove("Address");
            ModelState.Remove("Enrollments");
            ModelState.Remove("Courses");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.Id == id);
        }
    }
}
