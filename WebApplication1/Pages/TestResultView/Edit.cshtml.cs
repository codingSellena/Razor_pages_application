using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Modules;

namespace WebApplication1.Pages.TestResultView
{
    public class EditModel : PageModel
    {
        private readonly WebApplication1Context _context;

        public EditModel(WebApplication1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public TestResultClass TestResultClass { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TestResultClass = await _context.TestResultClasses
                .Include(t => t.testItem)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (TestResultClass == null)
            {
                return NotFound();
            }
            Console.WriteLine($"You have {TestResultClass.TableDataArray.Length} elements.");
            ViewData["TestId"] = new SelectList(_context.TestClasses, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["TestId"] = new SelectList(_context.TestClasses, "Id", "Id");
                return Page();
            }

            _context.Attach(TestResultClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestResultClassExists(TestResultClass.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TestResultClassExists(int id)
        {
            return _context.TestResultClasses.Any(e => e.Id == id);
        }
    }
}
