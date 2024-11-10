using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lexicon_university.Core.Entities;
using lexicon_university.Persistance.Data;
using lexicon_university.Web.Models.ViewModels;
using Bogus.DataSets;
using AutoMapper;

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

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create(StudentCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //var student = new Student
                //{
                //    Avatar = "https://thispersondoesnotexist.com/",
                //    FirstName = viewModel.FirstName,
                //    LastName = viewModel.LastName,
                //    Email = viewModel.Email,
                //    Address = new Core.Entities.Address
                //    {
                //        Street = viewModel.Street,
                //        ZipCode = viewModel.ZipCode,
                //        City = viewModel.City
                //    }
                //};

             
                var student = mapper.Map<Student>(viewModel);
                student.Avatar = "https://thispersondoesnotexist.com/";

                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
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
