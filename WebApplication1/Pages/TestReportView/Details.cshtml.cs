using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Modules;

namespace WebApplication1.Pages.TestReportView
{
    public class DetailsModel : PageModel
    {
        private readonly WebApplication1.Data.WebApplication1Context _context;

        public DetailsModel(WebApplication1.Data.WebApplication1Context context)
        {
            _context = context;
        }

        public TestReportClass TestReportClass { get; set; } = default!;
        [BindProperty] public DeviceClass DeviceInfo { get; set; } // 添加 Device 信息属性
        public TestReportClass TestReport { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var testreportclass = await _context.TestReportClass
                .Include(tr => tr.TestResults)
                .ThenInclude(tr => tr.testItem)
                 .ThenInclude(ti => ti.Device)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (testreportclass != null && testreportclass.TestResults.Any())
            {
                // 获取第一个 TestResult 的 DeviceId
                var deviceId = testreportclass.TestResults.First().testItem.DeviceId;

                // 检查 DeviceId 是否存在
                if (await _context.DeviceClasses.FirstOrDefaultAsync(d => d.DeviceId == deviceId.ToString()) != null)
                {
                    // 获取设备信息
                    DeviceInfo = await _context.DeviceClasses.FirstOrDefaultAsync(d => d.DeviceId == deviceId.ToString());
                }
            }

            if (testreportclass == null)
            {
                return NotFound();
            }
            else
            {
                TestReportClass = testreportclass;
            }
            return Page();
        }
    }
}
