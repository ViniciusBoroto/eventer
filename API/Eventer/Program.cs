using Eventer.Contexts.EventContext.DTOs.Requests;
using Eventer.Contexts.EventContext.Interfaces;
using Eventer.Contexts.EventContext.Repositories;
using Eventer.Contexts.EventContext.UseCases;
using Eventer.Contexts.OrderContext.Interfaces;
using Eventer.Contexts.OrderContext.Repositories;
using Eventer.Contexts.OrderContext.UseCases;
using Eventer.Contexts.TicketContext.Interfaces;
using Eventer.Contexts.TicketContext.Repositories;
using Eventer.Database;
using Microsoft.EntityFrameworkCore;

void AddToContainer(WebApplicationBuilder b)
{
    b.Services.AddOpenApi();
    b.Services.AddControllers();
    b.Services.AddCors(options =>
    {
        options.AddPolicy("Frontend", policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });
    
    b.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite(b.Configuration.GetConnectionString("DefaultConnection")));

    b.Services.AddScoped<IEventRepository, EventRepository>();
    b.Services.AddScoped<EventUpdateCase>();
    b.Services.AddScoped<EventDeleteCase>();
    b.Services.AddScoped<EventCreateCase>();
    b.Services.AddScoped<EventGetAllCase>();
    b.Services.AddScoped<EventGetByIdCase>();

    b.Services.AddScoped<IOrderRepository, OrderRepository>();
    b.Services.AddScoped<ITicketRepository, TicketRepository>();
    b.Services.AddScoped<OrderDeleteCase>();
    b.Services.AddScoped<OrderCreateCase>();
    b.Services.AddScoped<OrderGetAllCase>();
    b.Services.AddScoped<OrderGetByIdCase>();
    b.Services.AddScoped<OrderPayCase>();
    b.Services.AddScoped<OrderCancelCase>();
}

void ConfigureRequestPipeline(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/openapi/v1.json", "API v1"));
    }
    else
    {
        app.UseHttpsRedirection();
    }
    app.UseCors("Frontend");

    app.MapControllers();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
AddToContainer(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
ConfigureRequestPipeline(app);

app.Run();
