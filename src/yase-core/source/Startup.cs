using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Web;
using OpenTracing;
using OpenTracing.Util;
using Jaeger.Samplers;
using Jaeger;
using Prometheus.Client.MetricServer;
using Prometheus.Client.AspNetCore;
using Prometheus.Client.Collectors;
using Prometheus;

using yase_core.Logic;

namespace yase_core
{
    public class Startup
    {
        const string HASHING_SECTION = "Hashing";
        const string CORS_POLICY  = "CorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
                {
                    options.AddPolicy(CORS_POLICY,
                        builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials() );
                });

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services
                .AddOpenTracing();
            
            services.AddSingleton<ITracer>(serviceProvider =>
            {
                string serviceName = serviceProvider.GetRequiredService<IHostingEnvironment>().ApplicationName;

                var tracer = new Tracer.Builder(serviceName)
                    .WithSampler(new ConstSampler(true))
                    .Build();
                    
                GlobalTracer.Register(tracer);

                return tracer;
            });
               
            var hashingSection = Configuration.GetSection(HASHING_SECTION);
            var settings = hashingSection.Get<Settings>();
            services
                .AddSingleton<ISettings>(settings)
                .AddSingleton<IHash, Hash>()
                .AddSingleton<IHashing, Hashing>()
                .AddSingleton<IWebRequestFactory, WebRequestFactory>()
                .AddSingleton<IHttpRequests, HttpRequests>()
                .AddSingleton<IStorageServiceWrapper, StorageServiceWrapper>()
                .AddSingleton<IUrlHandler, UrlHandler>()
                .AddSingleton<IValidator, Validator>()
                .AddSingleton<ITimeToLive, TimeToLive>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            var counter = Metrics.CreateCounter("PathCounter", "Counts requests to endpoints", new CounterConfiguration
            {
                LabelNames = new[] { "method", "endpoint" }
            });
            app.Use((context, next) =>
            {
                counter.WithLabels(context.Request.Method, context.Request.Path).Inc();
                return next();
            });
            
            // app.UsePrometheusServer(q => { q.UseDefaultCollectors = false; });
            // app.UsePrometheusServer(q =>
            // {
            //     q.CollectorRegistryInstance = new CollectorRegistry();
            //     q.MapPath = "/default-metrics";
            // });

            app.UsePrometheusServer();
            app.UseCors(CORS_POLICY);
            app.UseMvc();
        }
    }
}

