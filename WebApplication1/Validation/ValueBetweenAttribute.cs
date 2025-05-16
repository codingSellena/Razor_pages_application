using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace WebApplication1.Validation // 请使用你项目的命名空间
{
    public class ValueBetweenAttribute : ValidationAttribute
    {
        private readonly string _minPropertyName;
        private readonly string _maxPropertyName;

        public ValueBetweenAttribute(string minPropertyName, string maxPropertyName)
        {
            _minPropertyName = minPropertyName;
            _maxPropertyName = maxPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var minPropertyInfo = validationContext.ObjectType.GetProperty(_minPropertyName);
            var maxPropertyInfo = validationContext.ObjectType.GetProperty(_maxPropertyName);

            if (minPropertyInfo == null)
                throw new ArgumentException($"Property '{_minPropertyName}' not found.");

            if (maxPropertyInfo == null)
                throw new ArgumentException($"Property '{_maxPropertyName}' not found.");

            var minValue = (double)minPropertyInfo.GetValue(validationContext.ObjectInstance);
            var maxValue = (double)maxPropertyInfo.GetValue(validationContext.ObjectInstance);

            if (value is double doubleValue)
            {
                if (doubleValue >= minValue && doubleValue <= maxValue)
                    return ValidationResult.Success;

                return new ValidationResult($"{validationContext.DisplayName} 必须在 {minValue} 和 {maxValue} 之间。");
            }

            return new ValidationResult($"{validationContext.DisplayName} 的类型无效。");
        }
    }
}
