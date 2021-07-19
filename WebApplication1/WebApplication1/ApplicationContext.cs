using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectPatientsClinics
{
	public class ApplicationContext: DbContext
	{
		/// <summary>
		/// Пациенты
		/// </summary>
		public DbSet<Patient> patients { get; set; }
		/// <summary>
		/// Клиники
		/// </summary>
		public DbSet<Clinic> clinics { get; set; }
		private void CheckDataBase()
		{
			bool created = Database.EnsureCreated();
			if (created)
			{
				try
				{
					Database.ExecuteSqlRaw("ALTER TABLE clinics ADD CONSTRAINT UNIQUEName UNIQUE (Name)");
				}
				catch
				{
					throw new Exception("Нарушение структуры БД: ");
				}
			}
		}
		public ApplicationContext()
		{
			CheckDataBase();
		}
		public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options)
		{
			CheckDataBase();
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//optionsBuilder.UseSqlServer("Server=RU01-2830\\MSSQLSERVERDEV;Database=ProjectClinics;Trusted_Connection=True");
			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ProjectClinics;Trusted_Connection=True");
		}
	}
}
