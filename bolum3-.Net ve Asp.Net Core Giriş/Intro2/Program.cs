using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//HATA YÖNETİMİ   Burası Sadece Hata Olduğunda Çalışıyor (Middleware)
app.UseExceptionHandler(exceptionApp =>
{
    exceptionApp.Run(async context =>
    {
        var feature = context.Features.Get<IExceptionHandlerPathFeature>();//bir hata olduğunda ilgili hatanın bilgisini almaya yarıyo
        var ex = feature?.Error; //feature null değilse ilgili hatayı elde ediyoruz.

        context.Response.StatusCode = 500;//bir hata varsa cevaba 500 kodu atanıyo bu kod da hatayı sunucunun üstlenmesi için konuluyor
        context.Response.ContentType = "text/plain; charset=utf-8";

        await context.Response.WriteAsync("Sunucu Hatası Oluştu!\n");
        System.Console.WriteLine($"[Hata] İstek yolu {feature?.Path} | Mesaj: {ex?.Message}");
    });
});


//Süre ölçme (Middleware)
app.Use(async (context, next) =>
{
    var sw = Stopwatch.StartNew();//kronometre başlatıldı

    context.Response.OnStarting(() =>
    {
       sw.Stop();
       context.Response.Headers["X-Elapsed-Milisecond"] = sw.ElapsedMilliseconds.ToString();
       return Task.CompletedTask; 
    });  

    await next.Invoke(); //zincire devam edecek yani bir sonraki middleware ifadesine devam edecek

    System.Console.WriteLine($"[SÜRE] {context.Request.Method} {context.Request.Path} --> {sw.ElapsedMilliseconds} ms");
});


//ayrıca bu bir endpoint
app.MapGet("/", () => "Hello HASAN!"); //sitenin urlsine / yazınca hello world çıktısı verir

app.MapGet("/hello", () => //MapGet bir mesaj gönderiyo sunucuya, sunucudan dönen mesaj da bize çıktı olarak geliyor
{
    return Results.Text("Merhaba! bu bir middleware odaklı .Net 8.0 deneme uygulamasıdır.");
});

app.MapGet("/boom", () =>
{
   throw new InvalidOperationException("Eğitim Amaçlı Kontrollü Hata");
});

app.Run();
