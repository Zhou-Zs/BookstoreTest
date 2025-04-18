
using Bookstore.Application.Mapping;
using Bookstore.Application.Validators;
using Bookstore.API.Extensions;
using Bookstore.Domain.IRepositories;
using Domain.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Bookstore.Infrastructure;
using Bookstore.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Bookstore.Application.DTO;

namespace Bookstore.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ʹ���ڴ����ݿⷽ�����
            builder.Services.AddDbContext<BookstoreDdContext>(options => options.UseInMemoryDatabase("BookstoreDB"));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<BookstoreDdContext>().AddDefaultTokenProviders();

            // ע��ִ��ͷ���
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            builder.Services.AddScoped<ShoppingCartService>();

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(CustomProfile));

            // FluentValidation
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddScoped<IValidator<CreateBookDto>, CreateBookValidator>();

            builder.Services.AddJwtAuthentication(builder.Configuration);
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers();
           
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
