using System;
using System.Text;
using System.Threading.Tasks;
using ApiTaCerto.Models.Usuario;
using ApiTaCerto.Repositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ApiTaCerto
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MainDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IPessoaRepository, PessoaRepository>();
            services.AddTransient<IAtividadeRepository, AtividadeRepository>();
            services.AddTransient<IMidiaRepository, MidiaRepository>();

            services.AddCors(options => 
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            services.AddMvc();

            services.AddControllers();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo{ Title = "Api Tá Certo", Version = "v1"});
            });

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "sesi",
                        ValidAudience = "usuário",
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))
                    };
                    options.RequireHttpsMetadata = true;

                    options.Events = new JwtBearerEvents{
                        OnAuthenticationFailed = context => {
                            Console.WriteLine("Token inválido..." + context.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context => {
                            Console.WriteLine("Token válido..." + context.SecurityToken);
                            return Task.CompletedTask;
                        }
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true; 
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Tá Certo V1");
            });

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
