
using System.Reflection.PortableExecutable;
using InterviewTask.Signalr.Hubs;
using Microsoft.AspNetCore.Hosting;

namespace InterviewTask.Signalr;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        
        builder.Services.AddRazorPages();

        // Gerald: Enable SignalR functionality
        builder.Services.AddSignalR();

        var app = builder.Build();

        

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();

            // Gerald: Make sure to disable this for development!
            app.UseHttpsRedirection();
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.MapHub<ChatHub>("/chathub");

        app.Run();

        //CreateHostBuilder(args).Build().Run();
    }
}
