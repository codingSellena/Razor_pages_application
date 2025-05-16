using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Validation;

namespace WebApplication1.Modules
{
    public class TestClass
    {
        public TestClass()
        {
        }
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        [Display(Name = "关联设备ID")]
        public int DeviceId { get; set; }//与Device相关联的外键
        [ValidateNever]
        public DeviceClass Device { get; set; }

        //[ValidateNever]
        //[Display(Name = "实验ID")]
        //public int TestId { get; set; }//与TestResult相关联的外键
        [ValidateNever]
        public ICollection<TestResultClass> TestResults { get; set;}
        [Display(Name = "实验名称")]
        public string? Name { get; set; }
        [Display(Name = "档位%")] public string DataValue { get; set; }
         public string Unit { get; set; }
        [Display(Name = "档位%")]
        [ValidateNever]
        public string Mode { get; private set; }//档位
        public void CombineDataAndUnit()
        {
            Mode = $"{DataValue}{Unit}";
        }
        // 在加载时分解数据部分和单位部分
        public void DecomposeMode() { 
            if (!string.IsNullOrEmpty(Mode) && Mode.Length > 1)
            {
                DataValue = Mode.Substring(0, Mode.Length - 1); 
                Unit = Mode.Substring(Mode.Length - 1);
            }
        }
        [Display(Name = "百分比%")]
        [Range(0, 100)]
        public double Percentage { get; set; } //百分比(百分位数)
        [Display(Name = "数据下限%")]
        public double DataLow { get; set; }//数据下限(百分位数)
        [Display(Name = "数据上限%")]
        public double DataHigh { get; set; }//数据上限(百分位数)
        [Display(Name = "实测数据%")]
        [ValueBetween("DataLow", "DataHigh")]
        public double DataTest { get; set; }//实测数据(百分位数)
    }
}
