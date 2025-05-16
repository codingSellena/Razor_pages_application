namespace WebApplication1.Modules
{
    public class TestReportClass
    {
        public int Id { get; set; }

        public int TestReportId { get; set; } // 主键
        public ICollection<TestResultClass> TestResults { get; set; }


        public DateTime TestTime { get; set; }
        // 无参数构造函数
        public TestReportClass() { TestReportId = Id+1; }

    }
}
