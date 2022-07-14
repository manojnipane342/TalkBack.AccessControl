global using Microsoft.EntityFrameworkCore;
global using System.Text.Json.Serialization;
global using TalkBack.Contacts.Data.Context;
global using TalkBack.Contacts.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TalkBackContactsDbContext>(options =>
options.UseSqlServer(
builder.Configuration.GetConnectionString("TalkBackContactsConnectionString")));

builder.Services.AddCors(setup =>
{
    setup.AddPolicy("default", options =>
    {
        options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().WithOrigins(builder.Configuration["Angular:LocalHost"]);
    });
});


builder.Services.AddScoped<IContactsRepository, ContactsRepository>();



builder.Services.AddControllers().AddJsonOptions(options =>
options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<TalkBackContactsDbContext>();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("default");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
