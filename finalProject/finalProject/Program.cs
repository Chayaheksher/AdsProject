using finalProject;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("_myAllowSpecificOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddScoped<IAdsDAL, AdsDAL>();
builder.Services.AddScoped<IAdsBL, AdsBL>();
builder.Services.AddScoped<IConnectionDAL, ConnectionDAL>();
builder.Services.AddScoped<IConnectionBL, ConnectionBL>();
builder.Services.AddScoped<IGraphsDAL, GraphsDAL>();
builder.Services.AddScoped<IGraphsBL, GraphsBL>();
builder.Services.AddScoped<IUserDetailsDAL, UserDetailsDAL>();
builder.Services.AddScoped<IUserDetailsBL, UserDetailsBL>();


var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    });

    app.UseCors("_myAllowSpecificOrigins");

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}


