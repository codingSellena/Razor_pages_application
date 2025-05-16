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

namespace WebApplication1.Pages.TestResult
{
    public class CreateModel : PageModel
    {
        private readonly WebApplication1.Data.WebApplication1Context _context;

        public CreateModel(WebApplication1.Data.WebApplication1Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var testlist = _context.TestClasses.Select(t => new { t.Id, DisplayText = $"{t.Id} -{t.Name} - {t.Percentage}-{t.DataTest}-{t.Device.Name}" });
            ViewData["TestItem"] = new SelectList(testlist, "Id", "DisplayText");
            //ViewData["TestId"] = new SelectList(_context.TestClasses, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public TestResultClass TestResultClass { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var testitem = await _context.TestClasses.
                FirstOrDefaultAsync(d => d.Id == TestResultClass.TestId);

            if (testitem == null)
            {
                // 处理找不到DeviceClass的情况
                ModelState.AddModelError("TestId", "找不到指定的测试条目。");
                var testlist = _context.TestClasses.Select(t => new { t.Id, DisplayText = $"{t.Id} -{t.Name} - {t.Percentage}-{t.DataTest}-{t.Device.Name}" });
                ViewData["TestItem"] = new SelectList(testlist, "Id", "DisplayText");
                return Page();
            }
            TestResultClass.testItem = testitem;
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return Page();
            }

            _context.TestResultClasses.Add(TestResultClass);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
