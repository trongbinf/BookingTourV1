
using BookingTour.Business.Service;
using BookingTour.Business.Service.IService;
using BookingTour.Data.Data;
using BookingTour.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using User.Management.Service.Services;

namespace BookingTour
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;
            builder.Services.AddDbContext<BookingTourDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DBContext"));
			});

			// Configure Identity 
			builder.Services.AddIdentity<AppUser, IdentityRole>()
				.AddEntityFrameworkStores<BookingTourDbContext>()
				.AddDefaultTokenProviders();

            //Add Config for required email
            builder.Services.Configure<IdentityOptions>(
                options => options.SignIn.RequireConfirmedEmail = true);
            //Add Email Configs
            var emailConfig = configuration.GetSection("EmailConfiguration")
                                           .Get<EmailConfiguration>();
            builder.Services.AddSingleton(emailConfig);

            builder.Services.AddScoped<IEmailService, EmailService>();

            builder.Services.AddSingleton<IEmailSender>(sp =>
			   new EmailSender(sp.GetRequiredService<IConfiguration>()));

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookingTourAPI", Version = "v1" });

				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please enter token",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					BearerFormat = "JWT",
					Scheme = "bearer"
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
		{
			{
				new OpenApiSecurityScheme
				{
					Reference = new OpenApiReference
					{
						Type=ReferenceType.SecurityScheme,
						Id="Bearer"
					}
				},
				new string[]{}
			}
		});
			}
			);

			builder.Services
				.AddAuthentication(config =>
				{
					config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
					config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(options =>
				{
					options.SaveToken = true;
					options.RequireHttpsMetadata = false;
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(
							Encoding.ASCII.GetBytes(builder.Configuration["JWT:Secret"].ToString())),
						ValidateIssuer = true,
						ValidIssuer = builder.Configuration["JWT:Issuer"],
						ValidateAudience = true,
						ValidAudience = builder.Configuration["JWT:Audience"]
					};
				});
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowAnyOrigin();
            });

            app.UseAuthentication();

            app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
