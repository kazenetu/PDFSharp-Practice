using webapp.Utils;
using Microsoft.AspNetCore.StaticFiles;
using PdfLibrary;

var builder = WebApplication.CreateBuilder(args);

// フォントリゾルバーのグローバル登録
PdfLibrary.Utilites.Utility.SetJapaneseFontResolver();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();

var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".ftl"] = "text/plain";
app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
