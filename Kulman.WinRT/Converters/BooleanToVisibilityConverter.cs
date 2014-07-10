using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kulman.WinRT.Converters.Abstract;

namespace Kulman.WinRT.Converters
{
    public class BooleanToVisibilityConverter : BaseVisibilityConverter<bool>
    {
        public bool IsInverted { get; set; }

        protected override bool? ConvertToVisibility(bool value)
        {
            return IsInverted ? !value : value;
        }
    }
}
