using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
		/// Внешний ключ
		/// </summary>
		public int clinicID { get; set; }
		/// <summary>
		/// Клиника пациента
		/// </summary>
		[ForeignKey("clinicID")]
		public Clinic Clinic { get; set; }
	}
}
