using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Modules;

namespace WebApplication1.Pages.TestReportView
{
    public class GenerateModel : PageModel
    {
        private readonly WebApplication1Context _context;

        public GenerateModel(WebApplication1Context context)
        {
            _context = context;
        }

        public IList<TestReportClass> TestReportClass { get; set; } = default!;
        public TestReportClass newTestReport { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string QueryType { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty] public DeviceClass DeviceInfo { get; set; } // 添加 Device 信息属性
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
                    newTestReport = new TestReportClass
                    {
                       
                        TestTime = DateTime.Now,
                        TestResults = new List<TestResultClass>() // 初始化测试结果列表
                    };
                    if(await _context.DeviceClasses.FirstOrDefaultAsync(d => d.DeviceId == DeviceId.ToString())!=null)
                        DeviceInfo = await _context.DeviceClasses.FirstOrDefaultAsync(d => d.DeviceId == DeviceId.ToString());
                    foreach (int TestId in testIdsList)
                    {

                        RandomTestResult = WebApplication1.Modules.TestResultClass.GenerateRandomTestResult(DeviceId, TestId);
                        // 查找与 TestId 具有相同 Id 号的 TestClasses 对象
                        if (_context.TestClasses.Where(tc => tc.Id == TestId).FirstOrDefault() != null)
                        {
                            RandomTestResult.testItem = _context.TestClasses.Where(tc => tc.Id == TestId).FirstOrDefault();
                        }
                    
                        _context.TestResultClasses.Add(RandomTestResult);
                        newTestReport.TestResults.Add(RandomTestResult);
                    }
                    // 将 TestReport 添加到数据集中
                    _context.TestReportClass.Add(newTestReport);

                    await _context.SaveChangesAsync();
                }
               
                TestReportClass = await _context.TestReportClass.ToListAsync();
            }
        }
    }
}
