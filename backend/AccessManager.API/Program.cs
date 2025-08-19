var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDatabase(builder.Configuration)
    .AddCustomCors()
    .AddHandlers()
    .AddRepositories()
    .AddCustomServices()
    .AddJwtAuthentication(builder.Configuration)
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddControllers(); 

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("Frontend")
   .UseDatabaseConnectionCheck(app.Environment);

if (app.Environment.IsDevelopment())
{
    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
