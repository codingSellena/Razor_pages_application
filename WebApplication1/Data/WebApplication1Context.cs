using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Modules;

namespace WebApplication1.Data
{
    public class WebApplication1Context : DbContext
    {
        public WebApplication1Context (DbContextOptions<WebApplication1Context> options)
            : base(options)
        {
        }

        public DbSet<WebApplication1.Modules.TestClass> TestClasses { get; set; } = default!;
        public DbSet<WebApplication1.Modules.DeviceClass> DeviceClasses { get; set; } = default!;
        public DbSet<WebApplication1.Modules.TestResultClass> TestResultClasses { get; set; } = default!;

        public DbSet<WebApplication1.Modules.TestResultClass> TestReportClasses { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeviceClass>()
                .HasMany(d => d.TestItems)// 一个设备有多个测试项
                .WithOne(t => t.Device)// 每个测试项属于一个设备
                .HasForeignKey(t => t.DeviceId) // 外键
                .OnDelete(DeleteBehavior.Cascade); 

            // 配置 DeviceClass 的 Id 为自动递增
            modelBuilder.Entity<DeviceClass>()
                .Property(d => d.Id)
                .ValueGeneratedOnAdd();

            // 配置 TestReportClass 的 Id 为自动递增
            modelBuilder.Entity<TestReportClass>()
                .Property(trc => trc.Id)
                .ValueGeneratedOnAdd();


            modelBuilder.Entity<TestClass>()
                .HasMany(tr => tr.TestResults)
                .WithOne(ti => ti.testItem)
                .HasForeignKey(tid => tid.TestId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<TestReportClass>()
               .HasMany(trs => trs.TestResults)
               .WithOne(ti => ti.TestReport)
               .HasForeignKey(tid => tid.TestReportId)
               .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<WebApplication1.Modules.TestReportClass> TestReportClass { get; set; } = default!;
    }
}
