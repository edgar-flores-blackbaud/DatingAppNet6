using API.Data;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

string specificAllowedOrigins = "_specificAllowedOrigins";
var builder = WebApplication.CreateBuilder(args);

// CORS policy.
builder.Services.AddCors(options => {
    options.AddPolicy(
        name: specificAllowedOrigins, 
        policy => {
            policy
            .WithOrigins("https://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});
//builder.Services.AddAuthentication(options => {
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//    })
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(
//                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
//            ValidateIssuer = false,
//            ValidateAudience = false
//        };
//    });
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddScoped<ITokenService, TokenService>();
//builder.Services.AddDbContext<DataContext>();
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(specificAllowedOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
