using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public DetailsModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Students
                //Adds Enrolments navigation property:
                .Include(s => s.Enrollments)
                //Adds Enrolments.Courses navigation property:
                .ThenInclude(e => e.Course)
                // improves performance in scenarios where the entities returned are not updated in the current context
                .AsNoTracking()
                //Returns Null if nothing is found or first record otherwise:
                //It is a better alternative than:
                //SingleOrDefaultAsync - Throws an exception if there's more than one entity that satisfies the query filter
                //FindAsync - Finds an entity with the primary key (PK), BUT:
                //you can't call Include with FindAsync, thus any related data cannot be retrieved
                .FirstOrDefaultAsync(m => m.ID == id);
                    

            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
