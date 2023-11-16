using Microsoft.EntityFrameworkCore;
using Shopping;

var builder = WebApplication.CreateBuilder(args);

//connect DB
builder.Services.AddDbContext<StoreContext>(dbOptions =>
{
  dbOptions.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();


var app = builder.Build();


// DB Migration
using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  try
  {
    var db = scope.ServiceProvider.GetRequiredService<StoreContext>();
    db.Database.Migrate();

  }
  catch (Exception error)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(error, "DB Migration Error");
  }
}


// swagger
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}



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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



