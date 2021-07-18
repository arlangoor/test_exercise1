using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPatientsClinics
{
	/// <summary>
	/// Пациент
	/// </summary>
	public class Patient : BaseObject
	{
		/// <summary>
		/// Город
		/// </summary>
		public string City { get; set; }
		/// <summary>
		/// Клиника пациента
		/// </summary>
		public Clinic clinic { get; set; }
	}
}
