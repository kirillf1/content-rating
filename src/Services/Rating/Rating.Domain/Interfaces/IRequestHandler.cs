﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rating.Domain.Interfaces
{
    public interface IRequestHandler<TRequest, TResult>
    {
        Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken);
    }
}
