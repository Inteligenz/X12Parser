namespace OopFactory.X12.Sql
{
	using System;

	public static class TypeExtensions
	{
		public static object GetDefaultValue(this Type t)
		{
			if (t.IsValueType)
				return Activator.CreateInstance(t);

			return null;
		}
	}
}