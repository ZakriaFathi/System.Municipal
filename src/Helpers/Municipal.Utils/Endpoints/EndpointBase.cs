using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipal.Utils.Endpoints
{
    public abstract class EndpointBase
    {
        public abstract class WithRequestWithResult<TRequest, TResponse> : EndpointBaseAsync.WithRequest<TRequest>.WithResult<TResponse>
        {
            public abstract override Task<TResponse> HandleAsync(
                TRequest request,
                CancellationToken cancellationToken = default);



            protected sealed class FromMultiSourceAttribute : Attribute, IBindingSourceMetadata
            {
                public BindingSource BindingSource { get; } = CompositeBindingSource.Create(new[]
                {
               BindingSource.ModelBinding
            }, nameof(FromMultiSourceAttribute));
            }
        }

        public abstract class WithoutRequestWithResult<TResponse> : EndpointBaseAsync.WithoutRequest.WithResult<TResponse>
        {

            public abstract override Task<TResponse> HandleAsync(
                CancellationToken cancellationToken = default);

            public sealed class FromMultiSourceAttribute : Attribute, IBindingSourceMetadata
            {
                public BindingSource BindingSource { get; } = CompositeBindingSource.Create(new[]
                {
                BindingSource.Query
            }, nameof(FromMultiSourceAttribute));
            }
        }

    }
}
