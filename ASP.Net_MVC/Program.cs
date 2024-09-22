using ASP.Net_MVC.DataBases;
using ASP.Net_MVC.Models;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

using(MainDbContext db = new MainDbContext())
{
    if(db.Workers.Count() == 0)
    {
        Worker.FillEventsAndWorkers(db);
    }
    if(db.Marks.Count() == 0)
    {
        Calendar.FillMarks(db);
    }
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=EventList}/{action=Index}");

app.MapFallbackToController(//¬ызываетс€ этот адрес, если адрес оказалс€ не правильным или недействительным
    action: "Index",
    controller: "EventList");

app.Run();