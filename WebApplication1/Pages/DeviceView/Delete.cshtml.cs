using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Modules;

namespace WebApplication1.Pages.DeviceView
{
    public class DeleteModel : PageModel
    {
        private readonly WebApplication1.Data.WebApplication1Context _context;

        public DeleteModel(WebApplication1.Data.WebApplication1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public DeviceClass DeviceClass { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceclass = await _context.DeviceClasses.FirstOrDefaultAsync(m => m.Id == id);

            if (deviceclass == null)
            {
                return NotFound();
            }
            else
            {
                DeviceClass = deviceclass;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceclass = await _context.DeviceClasses.FindAsync(id);
            if (deviceclass != null)
            {
                DeviceClass = deviceclass;
                _context.DeviceClasses.Remove(DeviceClass);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
