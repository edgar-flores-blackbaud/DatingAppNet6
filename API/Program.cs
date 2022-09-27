using API.Data;
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
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DataContext>();
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
app.UseAuthorization();

app.MapControllers();

app.Run();
