using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Data;
using WebApplication1.Modules;

namespace WebApplication1.Pages.DeviceView
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
            return Page();
        }

        [BindProperty]
        public DeviceClass DeviceClass { get; set; } = default!;


        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // 合并数据部分和单位部分
           
            _context.DeviceClasses.Add(DeviceClass);
            await _context.SaveChangesAsync();
            // 设置 DeviceId 并更新记录
            DeviceClass.DeviceId = DeviceClass.Id.ToString(); 
            _context.DeviceClasses.Update(DeviceClass);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        // 获取设备详细信息的方法
        /*
        public IActionResult OnGetDeviceDetails(int id)
        {
            var device = _context.DeviceClasses
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
                .FirstOrDefault();

            if (device == null)
            {
                return NotFound();
            }
            return new JsonResult(device);
        }
        */

    }
}
