

using MassTransit;
using Municipal.Consumers.Consumers;
using Municipal.Consumers.Contracts;
using Municipal.Consumers.Saga;
using Municipal.DataAccess.Persistence;

namespace Municipal.Api.Extensions;

public static class MassTransitConsumerExt
{
 public static void AddMassTransitConsumerServices(this WebApplicationBuilder builder)
    {

        builder.Host.ConfigureServices((_, services) =>
        {
            services.AddMassTransit(busConfigurator =>
            {

                var mqOptions = new RabbitMqOptions();
                var retryOptions = new RetryOptions();

                builder.Configuration.GetRequiredSection(nameof(RabbitMqOptions)).Bind(mqOptions);
                var section = builder.Configuration.GetRequiredSection(nameof(RetryOptions));

                builder.Services.Configure<RetryOptions>(section);
                busConfigurator.AddSagaStateMachine<ServicesSaga,ServicesSagaData>()
                    .EntityFrameworkRepository(r =>
                {
                    r.ExistingDbContext<RequestsStatesDb>();

                    r.UseSqlServer();
                });

                section.Bind(retryOptions);
                
                busConfigurator.AddConsumer<OrderConsumer>(
                    consumerConfig =>
                    {
                        consumerConfig.UseMessageRetry(opt => opt.Interval(3, new TimeSpan(0, 1, 0)));
                        consumerConfig.UseDelayedRedelivery(cb => cb.Interval(3, new TimeSpan(0, 0, 30)));

                        consumerConfig.UseCircuitBreaker(config =>
                        {
                            config.TripThreshold = 15;
                            config.ActiveThreshold = 5;
                            config.ResetInterval = TimeSpan.FromSeconds(30);
                        });
                    }).Endpoint(endpoint =>
                {
                    endpoint.PrefetchCount = mqOptions.PrefetchCount;
                    endpoint.ConcurrentMessageLimit = mqOptions.ConcurrentMessageLimit;
                    endpoint.Name = nameof(OrderContract);
                    
                }); 


                busConfigurator.AddHealthChecks();


                busConfigurator.SetKebabCaseEndpointNameFormatter();
             


                busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
                {
                    busFactoryConfigurator.Host(mqOptions.Host, mqOptions.VHost, h =>
                    {
                        h.Username(mqOptions.UserName);
                        h.Password(mqOptions.Password);
                    });

                    busFactoryConfigurator.ConfigureEndpoints(context);
                });
                
            });
            });
        }
    }



public class RabbitMqOptions
{
    public string Host { get; set; }
    public string VHost { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string QueueName { get; set; }
    public int PrefetchCount { get; set; }
    public int? ConcurrentMessageLimit { get; set; }
}
