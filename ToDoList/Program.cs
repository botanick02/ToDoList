using AutoMapper;
using MicrosoftSqlDB.Models;
using XMLStorage;
using ToDoList;
using ToDoList.GraphQL;
using GraphQL.Server;
using GraphQL.SystemTextJson;
using GraphQL;
using ToDoList.sourceChanger.Enums;
using ToDoList.sourceChanger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();



var config = AutoMapperConfig.Configure();
IMapper mapper = config.CreateMapper();

builder.Services.AddSingleton<IMapper>(mapper);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<DoToListSchema>();
builder.Services.AddGraphQL()
                .AddSystemTextJson()
                .AddGraphTypes(typeof(DoToListSchema));

builder.Services.AddProviderService();

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

app.UseGraphQL<DoToListSchema>();
app.UseGraphQLAltair();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ToDoList}/{action=Index}/{id?}");

app.Run();
