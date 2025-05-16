using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Modules;

namespace WebApplication1.Pages.TestResult
{
    public class IndexModel : PageModel
    {
        private readonly WebApplication1.Data.WebApplication1Context _context;

        public IndexModel(WebApplication1.Data.WebApplication1Context context)
        {
            _context = context;
        }

        public IList<TestResultClass> TestResultClass { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string QueryType { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public async Task OnGetAsync()
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from d in _context.TestResultClasses
                                            orderby d.Id.ToString()
                                            select d.Id.ToString();

            var testresult = from t in _context.TestResultClasses
                             select t;

            if (!string.IsNullOrEmpty(SearchString))//搜索栏不为空
                testresult = testresult.Where(s => s.Id.ToString().Contains(SearchString));
            else if (string.IsNullOrEmpty(SearchString) && QueryType == "TestId")
                testresult = testresult.Where(s => s.TestId == Id);

            TestResultClass = await testresult
                            .Include(t => t.testItem)
                            .ToListAsync();
        }
    }
}
