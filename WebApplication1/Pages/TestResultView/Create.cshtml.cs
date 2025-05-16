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

namespace WebApplication1.Pages.TestResultView
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
            // 初始化TestResultClass
            TestResultClass = new TestResultClass { TestDate = DateTime.Now, TableDataArray = new double[15]  };
            ViewData["TestId"] = new SelectList(_context.TestClasses, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public TestResultClass TestResultClass { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TestResultClasses.Add(TestResultClass);
            await _context.SaveChangesAsync();
            /*
            // 查找相关的 DeviceClass 和 TestClass 实例
            var device = await _context.DeviceClasses
                .FirstOrDefaultAsync(d => d.Id == TestResultClass.testItem.DeviceId);
            var test = await _context.TestClasses
                .FirstOrDefaultAsync(t => t.Id == TestResultClass.TestId);

            if (device == null || test == null)
            {
                return NotFound("相关的 DeviceClass 或 TestClass 未找到。");
            }

            // 创建 TestReportClass 实例
            var testReport = new TestReportClass(device, test, TestResultClass);

            // 保存 TestReportClass 实例
            _context.TestReportClasses.Add(testReport);
            */
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");

        }
    }
}
