global using TalkBackAccessControl.Data.Services;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using System.Text.Json.Serialization;
global using TalkBackAccessControl.Data.Context;
global using TalkBackAccessControl.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


builder.Services.AddDbContext<TalkBackDbContext>(options =>
options.UseSqlServer(
builder.Configuration.GetConnectionString("TalkBackConnectionString")));

builder.Services.AddCors(setup =>
{
    setup.AddPolicy("default", options =>
    {
        options.AllowAnyMethod().AllowAnyHeader()
        .AllowAnyOrigin().WithOrigins(builder.Configuration["Angular:LocalHost"]);
    });
});

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IContactApi, ContactApi>();


builder.Services.AddControllers().AddJsonOptions(options =>
options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<TalkBackDbContext>();
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
