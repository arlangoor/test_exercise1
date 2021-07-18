using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1
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

		public ApplicationContext()
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
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=RU01-2830\\MSSQLSERVERDEV;Database=ProjectClinics;Trusted_Connection=True"); 
			//optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ProjectClinics;Trusted_Connection=True");
		}
	}
}
