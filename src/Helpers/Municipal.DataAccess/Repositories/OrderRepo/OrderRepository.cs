using FluentResults;
using Microsoft.EntityFrameworkCore;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Application.Legacy.Features.Orders;
using Municipal.Application.Legacy.Features.Orders.Commands.CreateOrder;
using Municipal.Application.Legacy.Features.Orders.Commands.DeleteOrder;
using Municipal.Application.Legacy.Features.Orders.Commands.UpdateOrder;
using Municipal.Application.Legacy.Features.Orders.Queries.GetAllOrders;
using Municipal.Application.Legacy.Features.Orders.Queries.GetOrderById;
using Municipal.Application.Legacy.Features.Orders.Queries.GetOrderByUserId;
using Municipal.Application.Legacy.Features.Orders.Queries.GetOrderByUserName;
using Municipal.DataAccess.Persistence;
using Municipal.Domin.Entities;
using Municipal.Utils.Enums;

namespace Municipal.DataAccess.Repositories.OrderRepo;

public class OrderRepository : IOrdersRepository
{
    private readonly OrderDbContext _dbContext;


    public OrderRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<GetOrderResponse>>> GetAll(GetAllOrdersRequest request, CancellationToken cancellationToken)
    {
        var orders = await _dbContext.Orders
            .Select(x => new GetOrderResponse
            {
                Id = x.Id,
                OrderState = x.OrderState,
                UserName = x.UserName,
            }).ToListAsync();
        
        return orders;
    }

    public async Task<Result<GetOrderResponse>> GetById(GetOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == request.OrderId);
        if (order is null)
            return Result.Fail("هذا الطلب غير موجود" );

        var result = new GetOrderResponse
        {
            Id = order.Id,
            OrderState = order.OrderState,
            UserName = order.UserName,
        };
        
        return result;
    }

    public async Task<Result<GetOrderResponse>> GetOrderByUserId(GetOrderByUserIdRequest request, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == request.UserId);
        if (order is null)
            return Result.Fail("هذا الطلب غير موجود");
        
        var result = new GetOrderResponse
        {
            Id = order.Id,
            UserName = order.UserName,
            OrderState = order.OrderState,
        };
        
        return result;
    }

    public async Task<Result<string>> CreateOrder(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        //var validator = new CreateOrderRequestValidator();
        //var result1 = validator.Validate(request);
        //if (result1.IsValid == false)
        //    return Result<string>.UnValid(new List<string>() { result1.Errors[0].ErrorMessage });
        
        var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.UserName == request.UserName);
        if(order != null)
            return Result.Fail("هذا الطلب موجود");
        
        var result = new Order
        {
            Id = Guid.NewGuid(),
            OrderState = OrderState.Pending,
            FirstName = request.FirstName,
            FatherName = request.FatherName,
            LastName = request.LastName,
            Country = request.Country,
            Address = request.Address,
            EmailAddress = request.EmailAddress,
            UserName = request.UserName,
        };

        await _dbContext.Orders.AddAsync(result); 
        await _dbContext.SaveChangesAsync();
        
        return result.Id.ToString();

    }

    public async Task<Result<string>> UpdateOrder(UpdateOrderRequest request, CancellationToken cancellationToken)
    {
        //var validator = new UpdateOrderRequestValidator();
        //var result1 = validator.Validate(request);
        //if (result1.IsValid == false)
        //    return Result<string>.UnValid(new List<string>() { result1.Errors[0].ErrorMessage });
        
        var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.UserName == request.UserName);
        if(order == null)
            return Result.Fail("هذا الطلب غير موجود");

        order.Id = Guid.NewGuid();
        order.FirstName = request.FirstName;
        order.FatherName = request.FatherName;
        order.LastName = request.LastName;
        order.Country = request.Country;
        order.Address = request.Address;
        order.EmailAddress = request.EmailAddress;
        
         await _dbContext.SaveChangesAsync();
        
        return "تم التعديل";
    }

    public async Task<Result<string>> DeleteOrder(DeleteOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == request.Id);
        if(order == null)
            return Result.Fail("هذا الطلب غير موجود");
        
        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();
        
        return "تم الحذف";
    }

    public async Task<Result<List<Order>>> GetOrdersByUserName(GetOrderByUserNameRequest request, CancellationToken cancellationToken)
    {
        var orderList = await _dbContext.Orders
            .Where(o => o.UserName == request.UserName)
            .ToListAsync();
        return orderList ;
    }
}