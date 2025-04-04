using Domain.Port.User;
using Infrastructure.Extensions.JWT;
using Infrastructure.Extensions.Persistence;
using Infrastructure.Extensions.Start;
using Infrastructure.Extensions.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMongoDB(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddJWT(builder.Configuration);
builder.Services.AddSwaggerConfig();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8081);
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "UPCPRO API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseCors("AllowAllOrigins");

using (var scope = app.Services.CreateScope())
{
    var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
    var startMongoDB = new StartMongoDB(userRepository);
    await startMongoDB.CreateAdminStart();
}


app.Run();
