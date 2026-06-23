using ContactApp.Services;

var builder = WebApplication.CreateBuilder(args);//uygulama oluþturuluyor

// Add services to the container.
builder.Services.AddControllersWithViews();//Servis kaydý yapýlýyo. Controller yapýlarýný bu projede kullanýcam demek


//Dependency Injection - Register || kullanýcýlarý kaydetmek gibi biþey
builder.Services.AddSingleton<IContactRepository, InMemoryContactRepository>();
//IContactRepository ne zaman enjekte edilirse InMemoryContactRepository'i Newlenecek bu saydece uygulama bize bir referans vericek
//EMÝN DEÐÝLÝM ARAÞTIRILMALI


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

app.MapControllerRoute(//uygulama tarayýcýda default olarak direkt bu sayfada açýlýr
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); //https://localhost:5176/Home/Index anlamýna geliyor

app.Run();
