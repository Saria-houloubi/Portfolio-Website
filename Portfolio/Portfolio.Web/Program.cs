using Portfolio.Core.Abstractions;
using Portfolio.Web.Middlewares;
using Portfolio.Web.Services;
using Portfolio.Web.Setup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
        options.RootDirectory = "/Views/Pages";
});
builder.Services.AddServerSideBlazor();

//Custome services
builder.Services.AddScoped<ICurrentUserState, DefaultCurrentUserState>();
builder.Services.AddSingleton<IDateTimeProvider, UtcDateTimeProvider>();

builder.Services.RegisterSolutionServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

//Custome middle wares
app.UseCheckLanguage();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
