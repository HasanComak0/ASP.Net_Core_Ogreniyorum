var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!"); // bu satýr bir endpoint. Sunucuya bir istek gönderilir ve oradan gelen cevap 
//kullanýcýya çýktý olarak gösterilir

app.Run();
