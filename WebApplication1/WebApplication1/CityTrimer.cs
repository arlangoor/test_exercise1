using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPatientsClinics
{
	/// <summary>
	/// Вобор данных о городе по формату
	/// </summary>
	public static class CityTrimer
	{
		/// <summary>
		/// Метод находит название города в строке входных параметров
		/// </summary>
		/// <param name="city">"Город" - строка входных параметров</param>
		/// <returns></returns>
		public static string Trim(string city)
		{
			if (!string.IsNullOrEmpty(city) && city.Contains("г."))
			{
				int startIndex = city.IndexOf("г.") + 2;
				int endIndex = city.IndexOf(",", startIndex);
				if (endIndex > 0 && endIndex < city.Length)
					return city.Substring(startIndex, endIndex - startIndex).Replace(" ","");
			}
			throw new Exception("Wrong format: 'City'");
		}
	}
}
