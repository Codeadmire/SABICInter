using System;
using InterviewTask.Signalr.Hubs;
using Microsoft.OpenApi.Models;

namespace InterviewTask.Signalr
{
	public class Startup
	{
		public IConfiguration Configuration;
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddSignalR();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Chat WebApi", Version = "v1" });
			});
		}

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chathub");
            });
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseSwagger();
            //    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json","Chat App"));
            //}
            //else
            //{
            //    app.UseHsts();
            //}

            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            //});

            //app.UseHttpsRedirection();
            //app.UseRouting();
            //app.UseCors("CorsPolicy");

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //    endpoints.MapHub<ChatHub>("chathub");
            //});

            // app.Run(async (context) => await Task.Run(() => context.Response.Redirect("/swagger")));

        }
    }
}

