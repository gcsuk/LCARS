using System;
using System.ComponentModel;
using System.Reflection;

namespace LCARS
{
	public static class ExtensionMethods
	{
		public static string GetDescription(this Enum currentEnum)
		{
			FieldInfo fi = currentEnum.GetType().GetField(currentEnum.ToString());

			DescriptionAttribute da = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));

			return da != null ? da.Description : currentEnum.ToString();
		}
	}
}