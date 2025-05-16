using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Modules;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Pages.TestView
{
    public class CreateModel : PageModel
    {
        private readonly WebApplication1.Data.WebApplication1Context _context;

        public CreateModel(WebApplication1.Data.WebApplication1Context context)
        {
            _context = context;
        }
        [BindProperty] 
        public DeviceClass Device { get; set; }
        public SelectList DeviceItems { get; set; }//用来选的设备列表
        public IActionResult OnGet()
        {
            ViewData["DeviceId"] = new SelectList(_context.DeviceClasses, "Id", "Id");
            // 获取所有的 TestItem 并创建一个 SelectList

            DeviceItems = new SelectList(_context.DeviceClasses.Select(t => new { t.Id, Name = $"{t.Name} (ID: {t.Id})" }), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public TestClass TestClass { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var device = await _context.DeviceClasses.
                FirstOrDefaultAsync(d => d.Id == TestClass.DeviceId);
            
            if (device == null)
            {
                // 处理找不到DeviceClass的情况
                ModelState.AddModelError("DeviceId", "找不到指定的设备。");
                return Page(); // 或者返回其他适当的错误处理结果
            }
            TestClass.Device = device;

            
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors); 
                foreach (var error in errors) {
                    Console.WriteLine(error.ErrorMessage); 
                }
                // 如果 ModelState 无效，重新获取 TestItem 列表
                DeviceItems = new SelectList(_context.DeviceClasses.Select(d => new { d.Id, Name = $"{d.Name} (ID: {d.Id})" }), "Id", "Name");
                return Page();
            }
            
            _context.TestClasses.Add(TestClass);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        // 获取设备详细信息的方法
        public async Task<IActionResult> OnGetDeviceDetailsAsync(int id)
        {
            var device = await _context.DeviceClasses
                .Where(d => d.Id == id)
                .Select(d => new
                {
                    d.Name,
                    d.Manufacturer,
                    d.Place,
                    d.Description,
                    d.DateTime,
                    d.TelephoneNumber
                })
                .FirstOrDefaultAsync();

            if (device == null)
            {
                return NotFound();
            }

            return new JsonResult(device);
        }

    }
}
