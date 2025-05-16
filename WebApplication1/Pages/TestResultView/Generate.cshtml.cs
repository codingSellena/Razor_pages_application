using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Modules;

namespace WebApplication1.Pages.TestResultView
{
    public class GenerateModel : PageModel
    {

        private readonly WebApplication1Context _context;

        public GenerateModel(WebApplication1Context context)
        {
            _context = context;
        }

        public IList<TestResultClass> TestResultClass { get; set; } = default!;
        //[BindProperty(SupportsGet = true)]
        //public string SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string QueryType { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public int DeviceId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TestIds { get; set; }

        [BindProperty]
        public TestResultClass RandomTestResult { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (QueryType == "DeviceIdAndTestIds")
            {
                var query = _context.TestResultClasses.AsQueryable();

                if (DeviceId != 0)
                {
                    query = query.Where(tr => tr.testItem.DeviceId == DeviceId);
                }

                if (!string.IsNullOrEmpty(TestIds))
                {
                    var testIdsList = TestIds.Split(',').Select(int.Parse).ToList();
                    query = query.Where(tr => testIdsList.Contains(tr.TestId));
                    foreach (int TestId in testIdsList)
                    {
                        
                        RandomTestResult = WebApplication1.Modules.TestResultClass.GenerateRandomTestResult(DeviceId, TestId);
                        // 查找与 TestId 具有相同 Id 号的 TestClasses 对象
                        if(_context.TestClasses.Where(tc => tc.Id == TestId).FirstOrDefault()!=null)
                            RandomTestResult.testItem = _context.TestClasses.Where(tc => tc.Id == TestId).FirstOrDefault();
                        _context.TestResultClasses.Add(RandomTestResult);
                    }

                }

                await _context.SaveChangesAsync();
                TestResultClass = await query.ToListAsync();
            }
        }
    }
}
