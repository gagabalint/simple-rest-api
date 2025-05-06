using KEZDOCSAPATXI.Services;
namespace KEZDOCSAPATXI


{
    public class Program
    {
        public static void Main(string[] args)
        {
           

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IRatingGeneratorService, RatingGeneratorService>();
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            
            builder.Services.AddCors(opt => opt.AddDefaultPolicy(test => {
                test.AllowAnyHeader();
                test.AllowAnyMethod();
                test.AllowAnyOrigin();
            }));
            var app = builder.Build();

         

            app.UseHttpsRedirection();

            app.UseCors();


            app.MapControllers();

            app.Run();
        }
    }
}
