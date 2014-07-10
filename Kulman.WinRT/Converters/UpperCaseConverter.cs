using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kulman.WinRT.Converters.Abstract;

namespace Kulman.WinRT.Converters
{
    /// <summary>
    /// Converts give string to upper case
    /// </summary>
    public class UpperCaseConverter : BaseConverter<string, string>
    {
        public override string Convert(string value)
        {
            if (String.IsNullOrEmpty(value)) return null;

            return value.ToUpper();
        }
    }
}
