var builder = WebApplication.CreateBuilder(args);

// ????? ????? ??? Controllers
builder.Services.AddControllers();

// ????? Swagger ?????? ??? API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ????? Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();