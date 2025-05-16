using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Modules;

namespace WebApplication1.Pages.Test
{
    public class EditModel : PageModel
    {
        private readonly WebApplication1.Data.WebApplication1Context _context;

        public EditModel(WebApplication1.Data.WebApplication1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public TestClass TestClass { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testclass =  await _context.TestClasses.FirstOrDefaultAsync(m => m.Id == id);
            if (testclass == null)
            {
                return NotFound();
            }
            TestClass = testclass;
            TestClass.DecomposeMode();

            var deviceList = _context.DeviceClasses.Select(d => new { d.Id, DisplayText = $"{d.DeviceId} -{d.Name} - {d.Manufacturer}-{d.Place}-{d.DateTime}-{d.TelephoneNumber}" });
            ViewData["Device"] = new SelectList(deviceList, "Id", "DisplayText");

            ViewData["DeviceId"] = new SelectList(_context.DeviceClasses, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            TestClass.CombineDataAndUnit();
            if (!ModelState.IsValid)
            {
                var deviceList = _context.DeviceClasses.Select(d => new { d.Id, DisplayText = $"{d.DeviceId} -{d.Name} - {d.Manufacturer}-{d.Place}-{d.DateTime}-{d.TelephoneNumber}" });
                ViewData["Device"] = new SelectList(deviceList, "Id", "DisplayText");

                return Page();
            }

            _context.Attach(TestClass).State = EntityState.Modified;

            try
            {

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestClassExists(TestClass.Id))
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

        private bool TestClassExists(int id)
        {
            return _context.TestClasses.Any(e => e.Id == id);
        }
    }
}
