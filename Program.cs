using ClinicaASPNet.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ClinicaASPNet.Repositories.RepositoryPaciente>();
builder.Services.AddScoped<ClinicaASPNet.Repositories.RepositoryEspecialidade>();
builder.Services.AddScoped<ClinicaASPNet.Repositories.RepositoryProfissional>();

builder.Services.AddDbContext<ClinicaDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ClinicaConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ClinicaDbContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pacientes}/{action=Index}/{id?}");

app.Run();
