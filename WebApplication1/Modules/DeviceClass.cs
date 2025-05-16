using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Modules
{
    public class DeviceClass
    {
        public int Id { get; set; }
        [Display(Name = "设备编号")]
        public string DeviceId { get; set; } // 字符串类型的Id，用于查询和显示、从1开始计数
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "制造商")]
        public string Manufacturer { get; set; }
        [Display(Name = "生产地")]
        public string Place { get; set; }

        [Display(Name = "描述")]
        public string Description { get; set; }

        [Display(Name = "生产时间")]
        [DisplayFormat(DataFormatString = "{0:yyyyMMdd}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }

        [Display(Name = "联系方式")]
        [Phone]
        public string TelephoneNumber { get; set; }

        public DeviceClass() {
            TestItems = new List<TestClass>();

            DeviceId = (Id+1).ToString();//从1开始记录
        }

        //导航属性
        [ValidateNever]
        public ICollection<TestClass> TestItems { get; set; }

    }
}
