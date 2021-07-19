using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPatientsClinics
{
	/// <summary>
	/// Реализация постраничного получения данных
	/// </summary>
	public static class Paging
	{
		/// <summary>
		/// Получение страницы
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="en">Коллекция</param>
		/// <param name="pageSize">Число записей на странице</param>
		/// <param name="page">Номер страницы</param>
		/// <returns></returns>
		public static IEnumerable<T> Page<T>(this IEnumerable<T> en, int pageSize, int page)
		{
			return en.Skip(page * pageSize).Take(pageSize);
		}
		/// <summary>
		/// Получение страницы
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="en">Коллекция</param>
		/// <param name="pageSize">Число записей на странице</param>
		/// <param name="page">Номер страницы</param>
		/// <returns></returns>
		public static IQueryable<T> Page<T>(this IQueryable<T> en, int pageSize, int page)
		{
			return en.Skip(page * pageSize).Take(pageSize);
		}
	}
}
