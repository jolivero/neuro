using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

public static class UniversalHelpers
{
	public static T ConfigureSection<T>(this IConfiguration config, string? section = null) where T : new() => config.GetSection(section ?? typeof(T).Name).BindJsonProps<T>();

	public static T BindJsonProps<T>(this IConfigurationSection section) where T : new()
	{
		var obj = new T();
		var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

		foreach (var prop in props)
		{
			var name = prop.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? prop.Name;

			if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string))
			{
				var sub = section.GetSection(name);
				if (sub.Exists())
				{
					var nested = typeof(UniversalHelpers)
						.GetMethod(nameof(BindJsonProps))!
						.MakeGenericMethod(prop.PropertyType)
						.Invoke(null, new object[] { sub });

					prop.SetValue(obj, nested);
				}
			}
			else if (section[name] is string val)
			{
				prop.SetValue(obj, Convert.ChangeType(val, prop.PropertyType));
			}
		}

		return obj;
	}
}
