﻿using AppChatMVC.Entities;
using AppChatMVC.Hubs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//database tạo biến  
builder.Services.AddDbContext<AppChatDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddSession(); //có lệnh này mới sử dụng đc mvc

builder.Services.AddAuthentication("Cookies")
    .AddCookie(opt =>
    {
        opt.LoginPath = "/Account/Login";
        opt.ExpireTimeSpan = TimeSpan.FromMinutes(90);
        opt.Cookie.HttpOnly = true;
    });

builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();//có lệnh này mới sử dụng đc mvc

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); //lệnh này phải nằm trước lệnh Author; đăng nhập cookies
app.UseAuthorization();//phân quyền


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/chat");//Hubs
app.Run();
