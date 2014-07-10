using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Kulman.WinRT.Converters.Abstract
{
    public abstract class BaseConverter<TFrom, TTo> : IValueConverter
    {
        public abstract TTo Convert(TFrom value);

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null && !typeof(TFrom).GetTypeInfo().IsValueType)
            {
                bool nullable;

                if (typeof(TFrom).GetTypeInfo().IsValueType)
                {
                    nullable = Nullable.GetUnderlyingType(typeof(TFrom)) != null;
                }
                else
                {
                    nullable = true;
                }

                if (nullable)
                {
                    return Convert(default(TFrom));
                }
            }

            if (value is TFrom)
            {
                return Convert((TFrom)value);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
