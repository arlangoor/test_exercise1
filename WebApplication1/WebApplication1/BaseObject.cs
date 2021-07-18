using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPatientsClinics
{
	public abstract class BaseObject
	{
		/// <summary>
		/// Идентификатор объекта
		/// </summary>
		public int ID { get; set; }
		/// <summary>
		/// Наименование объекта
		/// </summary>
		public string Name { get; set; }
	}
}
