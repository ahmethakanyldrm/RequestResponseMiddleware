using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RequestResponseMiddleware.Library;
using RequestResponseMiddleware.Library.Models;

namespace MiddlewareTestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {

           
            
            var builder = WebApplication.CreateBuilder(args);
            

            // Add services to the container.
            builder.Services.AddLogging(conf =>
            {
                conf.AddConsole();
                //  conf.AddDebug();
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.AddRequestResponseMiddleware(option =>
            {
                //option.UseHandler(async context =>
                //{
                //    await Console.Out.WriteLineAsync($"Handler - RequestBody: {context.RequestBody}");
                //    await Console.Out.WriteLineAsync($"Handler - ResponseBody: {context.ResponseBody}");
                //    await Console.Out.WriteLineAsync($"Handler - Timing: {context.FormattedCreationTime}");
                //    await Console.Out.WriteLineAsync($"Handler - Url: {context.Url}");
                //});

                option.UseLogger(app.Services.GetRequiredService<ILoggerFactory>(), opt =>
                {
                    opt.LogLevel = LogLevel.Error;
                    opt.LoggerCategoryName = "My Custom Category Name";

                    opt.LoggingFields.Add(LogFields.Request);
                    opt.LoggingFields.Add(LogFields.Response);
                    opt.LoggingFields.Add(LogFields.ResponseTiming);
                    opt.LoggingFields.Add(LogFields.Path);
                    opt.LoggingFields.Add(LogFields.QueryString);
                });
            });

            app.MapControllers();

            app.Run();
        }
    }
}
