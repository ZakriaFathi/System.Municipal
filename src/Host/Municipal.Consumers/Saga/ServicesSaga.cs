using MassTransit;
using Municipal.Application.Legacy.Features.Orders.Commands.CreateOrder;
using Municipal.Consumers.Contracts;
using Municipal.Utils.Enums;

namespace Municipal.Consumers.Saga;

public class ServicesSaga : MassTransitStateMachine<ServicesSagaData>
{
    public State Activation { get;  }
    public State Failed { get; set; }
    
    public Event<ServiceFailed> ServiceFailed { get; set; }
    public Event<ServiceDispatched> ServiceDispatched { get;  }  
    public Event<ServiceActivated> ServiceActivated { get; }

    public ServicesSaga()
    {
        InstanceState(x => x.CurrentState);
        Event(() => ServiceDispatched, e =>
            e.CorrelateById(m => m.Message.RequestId));
        Event(() => ServiceFailed, e =>
            e.CorrelateById(m => m.Message.RequestId));
        Event(() => ServiceActivated, e =>
            e.CorrelateById(m => m.Message.RequestId));

        Initially(
            When(ServiceDispatched)
                .Then(context =>
                {
                    context.Saga.ServiceActivated = false;
                    context.Saga.RequstId = context.Message.RequestId;
                    context.Saga.UserName = context.Message.Customer.UserName;
                    context.Saga.CreatedAt = DateTime.Now;
                    context.Saga.Message = "Operation Started";
                }).TransitionTo(Activation)
                .Publish(context => new OrderContract{
                    RequestId = context.Message.RequestId,
                    CreateOrderRequest = context.Message.Customer,
                    FormType = context.Message.FormType
                }));

        During(Activation,
            When(ServiceFailed).Then(context =>
            {
                context.Saga.RequstId = context.Message.RequestId;
                context.Saga.Message = context.Message.ResponseMessage;
                context.Saga.CreatedAt = DateTime.Now;

            }).TransitionTo(Failed).Finalize());
        When(ServiceActivated)
            .Then(context =>
            {
                context.Saga.ServiceActivated = true;
                context.Saga.RequstId = context.Message.RequestId;
                context.Saga.UserName = context.Message.CreateOrderRequest.UserName;
                context.Saga.CreatedAt = DateTime.Now;
            }).TransitionTo(Activation).Finalize();
    }
}

public class ServiceActivated
{
    public Guid RequestId { get; set; }
    public CreateOrderRequest CreateOrderRequest { get; set; }
    public FormType FormType { get; set; }

}
public class ServiceFailed
{ 
    public Guid RequestId { get; set; }
    public string? ResponseMessage { get; set; }
}

public class ServiceDispatched
{
    public Guid RequestId { get; set; }
    public FormType FormType { get; set; }
    public CreateOrderRequest Customer { get; set; }
}

