using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
	/// <summary>
	/// Клиника
	/// </summary>
	public class Clinic: BaseObject
	{
		/// <summary>
		/// Название клиники
		/// </summary>
		[StringLength(450)]
		[Required]
		new public string Name { get; set; }
	}
}
