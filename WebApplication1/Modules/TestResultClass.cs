using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Modules
{

    public class TestResultClass
    {
        public TestResultClass()
        {
            int rows = 5;
            int cols = 3;
            TableDataArray = new double[rows * cols];
            for (int i = 0; i < rows * cols; i++)
            {
                TableDataArray[i] = 0;
            }
            TestId = -1;
            SecondaryVoltage = -1;
            Temperature = -1;
            Humidity = -1;
            TestDate = DateTime.Now;
            TanFai = -1;
            R = -1;
        }


        public int Id { get; set; }
        public int TestId { get; set; }//与Test相关联的外键
        [Display(Name = "二次电压(U)")]
        public double SecondaryVoltage { get; set; }
        [Display(Name = "温度(摄氏度)")]
        public double Temperature { get; set; }
        [Display(Name = "湿度(%)")]
        public double Humidity { get; set; }
        [Display(Name = "测试时间")]
        [DisplayFormat(DataFormatString = "{0:yyyyMMdd}", ApplyFormatInEditMode = true)]
        public DateTime TestDate { get; set; }
        [Display(Name = "tan φ")]
        public double TanFai { get; set; }
        [Display(Name = "r(%)")]
        public double R { get; set; }
        public double[] TableDataArray { get; set; }

        [NotMapped]
        public double[,] TableData
        {
            get
            {
                if (TableDataArray == null) 
                    return null;
                if(TableDataArray.Length<15)
                {
                    TableDataArray = new double[15];
                    for (int i = 0; i < TableDataArray.Length; i++)
                        TableDataArray[i] = 0;
                }    
                int rows = 5; // 定义行数
                int cols = 3; // 定义列数
                double[,] result = new double[rows, cols];

                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < cols; j++)
                        result[i, j] = TableDataArray[i * cols + j];

                return result;
            }
            set
            {
                int rows = value.GetLength(0);
                int cols = value.GetLength(1);
                TableDataArray = new double[rows * cols];

                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < cols; j++)
                        TableDataArray[i * cols + j] = value[i, j];
            }
        }
        // 外键
        public int TestReportId { get; set; }
        // 外键，指向测试报告
        public TestReportClass TestReport { get; set; } // 导航属性，指向测试报告
        //导航属性
        [ValidateNever]
        public TestClass testItem { get; set; }

        public static TestResultClass GenerateRandomTestResult(int DeviceId, int TestId)
        {
            var random = new Random();

            TestResultClass RandomTestResult = new TestResultClass
            {
                
                TestId = TestId,
                SecondaryVoltage = Math.Round(random.NextDouble() * 100, 2),
                Temperature = Math.Round(random.Next(-10, 40) + random.NextDouble(), 2),
                Humidity = Math.Round(random.NextDouble() * 100, 2), 
                TestDate = DateTime.Now,
                TanFai = Math.Round(random.NextDouble(), 2), 
                R = Math.Round(random.NextDouble() * 100, 2),
                TableDataArray = Enumerable.Range(0, 15).Select(_ => Math.Round(random.NextDouble() * 100, 2)).ToArray()
            };

            return RandomTestResult;
        }



    }
}
