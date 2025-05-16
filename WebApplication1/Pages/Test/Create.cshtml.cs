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
    public class CreateModel : PageModel
    {
        private readonly WebApplication1.Data.WebApplication1Context _context;

        public CreateModel(WebApplication1.Data.WebApplication1Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            //自定义下拉框内容
            var deviceList = _context.DeviceClasses.Select(d => new { d.Id, DisplayText = $"{d.DeviceId} -{d.Name} - {d.Manufacturer}-{d.Place}-{d.DateTime}-{d.TelephoneNumber}" });
            ViewData["Device"] = new SelectList(deviceList, "Id", "DisplayText");
            //ViewBag.DeviceId 提交的是Id，显示的是设备名
            //ViewData["DeviceId"] = new SelectList(_context.DeviceClasses, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public TestClass TestClass { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var device = await _context.DeviceClasses.
                FirstOrDefaultAsync(d => d.DeviceId == TestClass.DeviceId.ToString());

            if (device == null)
            {
                // 处理找不到DeviceClass的情况
                ModelState.AddModelError("DeviceId", "找不到指定的设备。");
                var deviceList = _context.DeviceClasses.Select(d => new { d.Id, DisplayText = $"{d.DeviceId} -{d.Name} - {d.Manufacturer}-{d.Place}-{d.DateTime}-{d.TelephoneNumber}" });
                ViewData["Device"] = new SelectList(deviceList, "Id", "DisplayText");
                return Page(); 
            }
            TestClass.Device = device;
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                var deviceList = _context.DeviceClasses.Select(d => new { d.Id, DisplayText = $"{d.DeviceId} -{d.Name} - {d.Manufacturer}-{d.Place}-{d.DateTime}-{d.TelephoneNumber}" });
                ViewData["Device"] = new SelectList(deviceList, "Id", "DisplayText");

                return Page();
            }

            TestClass.CombineDataAndUnit();
            _context.TestClasses.Add(TestClass);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
       

    }
}
