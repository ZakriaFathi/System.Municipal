using MassTransit;
using Microsoft.Extensions.Options;
using Municipal.Application.Legacy.Abstracts;

namespace Municipal.Consumers.Consumers;

public class RetryOptions
{
    public int MaxAttempts { get; set; }
    public int InitialDelayMilliseconds { get; set; }
    public double JitterFactor { get; set; }
}



public abstract class BaseConsumer<T> : IConsumer<T> where T : class
{
    private readonly RetryOptions _retryOptions;
    protected readonly IOrdersRepository OrdersRepository;


    protected BaseConsumer(IOptions<RetryOptions> retryOptions, IOrdersRepository ordersRepository)
    {
        OrdersRepository = ordersRepository;
        _retryOptions = retryOptions.Value;
    }

    protected abstract Task ConsumeMessage(ConsumeContext<T> context);

    public async Task Consume(ConsumeContext<T> context)
    {
        try
        {
            await ConsumeMessage(context);
        }
        catch (Exception e )
        {
            await RedeliverMessage(context);
        }
    }

    protected async Task RedeliverMessage(ConsumeContext<T> context)
    {
        var retryAttempt = context.GetRedeliveryCount();

        if (retryAttempt < _retryOptions.MaxAttempts) await context.Redeliver(CalculateExponentialBackoffDelay(retryAttempt));
    }

    private TimeSpan CalculateExponentialBackoffDelay(int retryAttempt)
    {
        var delayMilliseconds = _retryOptions.InitialDelayMilliseconds * Math.Pow(2, retryAttempt - 1);
        var jitteredDelay = JitterDelay(delayMilliseconds, _retryOptions.JitterFactor);
        return TimeSpan.FromMilliseconds(jitteredDelay);
    }

    private static double JitterDelay(double delay, double jitterFactor)
    {
        var minJitter = delay - (delay * jitterFactor);
        var maxJitter = delay + (delay * jitterFactor);

        return (Random.Shared.NextDouble() * (maxJitter - minJitter)) + minJitter;
    }
}