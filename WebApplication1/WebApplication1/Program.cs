using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPatientsClinics
{
	public class Program
	{
		public static void Main(string[] args)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				//test
				//Clinic clinic1 = new Clinic();
				//clinic1.Name = "Больница 1";
				//db.clinics.Add(clinic1);
				//db.SaveChanges();
				//var test =  db.clinics.ToList();
				CreateHostBuilder(args).Build().Run();
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
