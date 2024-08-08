using MassTransit;

namespace Municipal.Consumers.Saga;

public class ServicesSagaData : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    public Guid RequstId { get; set; }
    public string UserName { get; set; }
    public bool ServiceActivated { get; set; }
    public string? Message { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
}