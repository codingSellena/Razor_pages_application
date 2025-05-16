using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Modules;

namespace WebApplication1.Pages.DeviceView
{
    public class IndexModel : PageModel
    {
        private readonly WebApplication1.Data.WebApplication1Context _context;

        public IndexModel(WebApplication1.Data.WebApplication1Context context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }//索引用的字符串
        public IList<DeviceClass> DeviceClass { get; set; } = default!;
        /*
        public async Task<IActionResult> OnPostAddTestItemAsync(int deviceId, string name, double percentage, string mode, double dataHigh, double dataLow, double dataTest)
        {
            var device = await _context.DeviceClasses.FindAsync(deviceId);
            if (device == null)
            {
                return NotFound();
            }
            
            var newTestItem = new TestClass
            {
                DeviceId = deviceId,
                Name = name,
                Percentage = percentage,
                Mode = mode,
                DataHigh = dataHigh,
                DataLow = dataLow,
                DataTest = dataTest
            };
            
            _context.TestClasses.Add(newTestItem);
            await _context.SaveChangesAsync();
            
            return new JsonResult(new { success = true, data = newTestItem });
        }
        */
        /*
        public IActionResult OnPostAddTestItem([FromBody] TestClass newItem)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, message = "Invalid data" });
            }

            var device = DeviceClass.FirstOrDefault(d => d.DeviceId == newItem.DeviceId.ToString());
            if (device != null)
            {
                if (device.TestItems == null)
                {
                    device.TestItems = new List<TestClass>();
                }
                device.TestItems.Add(newItem);
            }

            return new JsonResult(new { success = true, data = newItem });
        }
        */

        public async Task OnGetAsync()
        {
            
            if (string.IsNullOrEmpty(SearchString))
                DeviceClass = await _context.DeviceClasses
                    .Include(d=>d.TestItems)
                    .ToListAsync();//入口
            else
            {

                var device = from d in _context.DeviceClasses
                             select d;
                if (!string.IsNullOrEmpty(SearchString))
                {
                    device = device.Where(s => s.DeviceId.Contains(SearchString));
                }
                DeviceClass = await device
                    .Include(d => d.TestItems)
                    .ToListAsync();
            }

        }
        /*
        [BindProperty]
        public TestResultClass RandomTestResultClass { get; set; }

        public async Task<IActionResult> OnPostGenerateTestResultAsync(int testId)
        {
            var random = new Random();
            RandomTestResultClass = new TestResultClass
            {
                TestId = testId,
                R = random.NextDouble(),
                Temperature = random.NextDouble(),
                Humidity = random.NextDouble()

            };

            _context.TestResultClasses.Add(RandomTestResultClass);
            await _context.SaveChangesAsync();

            return new JsonResult(RandomTestResultClass);
        }
        */
    }
}
