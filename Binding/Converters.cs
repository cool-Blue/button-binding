using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace Binding
{
	namespace converters
	{
		public class ResourceDictionaryMembersToString : IValueConverter
		{
			public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
			{
				return value.ToString();
			}

			public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
			{
				throw new NotImplementedException();
			}
		}
		public class NoOpConverter : IValueConverter
		{
			public object Convert (object value, Type targetType, object parameter,
				CultureInfo culture)
			{
				// obtain the conveter for the target type
				TypeConverter converter = TypeDescriptor.GetConverter(targetType);

				try
				{
					// determine if the supplied value is of a suitable type
					return converter.ConvertFrom(converter.CanConvertFrom(value.GetType()) ? value : value.ToString());
				}
				catch (Exception)
				{
					return value;
				}
			}

			public object ConvertBack (object value, Type targetType, object parameter,
				CultureInfo culture)
			{
				return value;
			}
		}
	}
}
