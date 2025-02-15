﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using WebApi.ModelView;

namespace WebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {
                    Version = "v1",
                    Title = "My API",
                    Description = "My First ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "Talking Dotnet", Email = "contact@talkingdotnet.com", Url = "www.talkingdotnet.com" }
                });
            });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AdversoViewModel, Adverso>();
                cfg.CreateMap<Adverso, AdversoViewModel>();

                cfg.CreateMap<AgendamentoViewModel, Agendamento>();
                cfg.CreateMap<Agendamento, AgendamentoViewModel>();


                cfg.CreateMap<ClienteViewModel, Cliente>();
                cfg.CreateMap<Cliente, ClienteViewModel>();

                cfg.CreateMap<ContatoViewModel, Contato>();
                cfg.CreateMap<Contato, ContatoViewModel>();

                cfg.CreateMap<ProcessoViewModel, Processo>();
                cfg.CreateMap<Processo, ProcessoViewModel>();

                cfg.CreateMap<RamoViewModel, Ramo>();
                cfg.CreateMap<Ramo, RamoViewModel>();

                
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseSwagger();

        }
    }
}
