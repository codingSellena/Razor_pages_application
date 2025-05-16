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
    public class IndexModel : PageModel
    {
        private readonly WebApplication1.Data.WebApplication1Context _context;

        public IndexModel(WebApplication1.Data.WebApplication1Context context)
        {
            _context = context;
        }
        [BindProperty(SupportsGet = true)]
        public ICollection<TestReportClass> TestResultItems { get; set; }
        public IList<TestReportClass> TestReportClass { get;set; } = default!;

        public async Task OnGetAsync()
        {
            TestReportClass = await _context.TestReportClass
                .Include(tr => tr.TestResults)
                .ThenInclude(tr => tr.testItem)
                .ThenInclude(ti => ti.Device)
                .ToListAsync();
        }
    }
}
