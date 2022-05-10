using AutoMapper;
using Business.Models;
using MicrosoftSqlDB.Models;
using XMLStorage;
using ToDoList;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddTransient<ToDoTaskDBRepository>();
builder.Services.AddTransient<CategoryDBRepository>();
builder.Services.AddTransient<ToDoTaskXMLRepository>();
builder.Services.AddTransient<CategoryXMLRepository>();

builder.Services.AddTransient<CategoryRepositoryResolver>(CategoryRepositoryProvider => key =>
{
    switch (key)
    {
        case "D":
            return CategoryRepositoryProvider.GetService<CategoryDBRepository>();
        case "X":
            return CategoryRepositoryProvider.GetService<CategoryXMLRepository>();
        default:
            throw new KeyNotFoundException();
    }
});
builder.Services.AddTransient<ToDoTaskRepositoryResolver>(ToDoTaskRepositoryProvider => key =>
{
    switch (key)
    {
        case "D":
            return ToDoTaskRepositoryProvider.GetService<ToDoTaskDBRepository>();
        case "X":
            return ToDoTaskRepositoryProvider.GetService<ToDoTaskXMLRepository>();
        default:
            throw new KeyNotFoundException();
    }
});

var config = AutoMapperConfig.Configure();
IMapper mapper = config.CreateMapper();

builder.Services.AddSingleton<IMapper>(mapper);

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ToDoList}/{action=Index}/{id?}");

app.Run();
