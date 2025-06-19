using LudotecaApi.Services;

namespace LudotecaApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Registrazione dei servizi
            builder.Services.AddControllers();
            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<GiocoService>();

            // Aggiungi la configurazione CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            

            // Abilita CORS prima di altri middleware (ad esempio, prima di HTTPS Redirection e UseAuthorization)
            app.UseCors("AllowAll");

            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapGet("/", () => Results.Redirect("/swagger"));

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run(); // Unica chiamata a app.Run()
        }
    }
}
