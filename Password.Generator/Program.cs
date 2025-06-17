using Password.Generator.Infrastructure.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.AddBuiltInServices();
builder.AddCustomServices();


var app = builder.Build();
app.UseRouting().UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapControllers();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.Run();
