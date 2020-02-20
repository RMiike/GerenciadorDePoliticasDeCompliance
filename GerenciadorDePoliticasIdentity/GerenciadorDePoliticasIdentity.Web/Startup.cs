using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorDePoliticasIdentity.Core.Data;
using GerenciadorDePoliticasIdentity.Core.Deposito;
using GerenciadorDePoliticasIdentity.Core.Dominio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GerenciadorDePoliticasIdentity.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })

            .AddCookie(IdentityConstants.ApplicationScheme, options =>
            {
                options.LoginPath = "/Usuario/Login";
                options.LogoutPath = "/Usuario/logout";
            })
            .AddCookie(IdentityConstants.ExternalScheme, options =>
            {
                options.Cookie.Name = IdentityConstants.ExternalScheme;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            })
            .AddCookie(IdentityConstants.TwoFactorRememberMeScheme, options =>
            {
                options.Cookie.Name = IdentityConstants.TwoFactorRememberMeScheme;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Home/Login";
                options.LogoutPath = "/home/logout";
            })
            .AddCookie(IdentityConstants.TwoFactorUserIdScheme, options =>
            {
                options.Cookie.Name = "primeira";
                options.Cookie.Name = "segunda";
            });


            services.AddDbContext<Contexto>(options =>
                           options.UseSqlServer(Configuration.GetConnectionString("Contexto"), builder => builder.MigrationsAssembly("GerenciadorDePoliticasIdentity.Web")));

            services.AddIdentityCore<Usuario>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
            }).AddDefaultTokenProviders();

      

            services.Configure<DataProtectionTokenProviderOptions>(options =>
              options.TokenLifespan = TimeSpan.FromHours(3));

            services.AddScoped<ServicoPessoa>();
            services.AddScoped<IUserStore<Usuario>, UsuarioStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
