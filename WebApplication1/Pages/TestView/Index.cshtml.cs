using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Modules;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Pages.TestView
{
    public class IndexModel : PageModel
    {
        private readonly WebApplication1.Data.WebApplication1Context _context;

        public IndexModel(WebApplication1.Data.WebApplication1Context context)
        {
            _context = context;
        }

       public string SearchString { get; set; }
        public IList<TestClass> TestClass { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (string.IsNullOrEmpty(SearchString))
                TestClass = await _context.TestClasses
                    .Include(d => d.DeviceId)
                    .ToListAsync();//入口
            else
            {
                // Use LINQ to get list of genres.
                IQueryable<string> genreQuery = from d in _context.DeviceClasses
                                                orderby d.DeviceId
                                                select d.DeviceId;

                var test = from t in _context.TestClasses
                           select t;
                if (!string.IsNullOrEmpty(SearchString))
                {
                    //test = test.Where(s => s.DeviceId.Contains(SearchString));
                }
                TestClass = await test.ToListAsync();
            }
            TestClass = await _context.TestClasses
                .Include(t => t.DeviceId).ToListAsync();
        }
    }
}
