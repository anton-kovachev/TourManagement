using System;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;


namespace TourManagement.API.Services
{
    public class ConnectionResiliencyExecutionStrategy : ExecutionStrategy
    {
        public ConnectionResiliencyExecutionStrategy(DbContext context) : base(context, ExecutionStrategy.DefaultMaxRetryCount, ExecutionStrategy.DefaultMaxDelay)
        {

        }

        public ConnectionResiliencyExecutionStrategy(ExecutionStrategyDependencies dependencies) : base(dependencies, ExecutionStrategy.DefaultMaxRetryCount, ExecutionStrategy.DefaultMaxDelay)
        {

        }

        public ConnectionResiliencyExecutionStrategy(DbContext context, int maxRetryCount, TimeSpan maxRetryDelay) : 
            base(context, maxRetryCount, maxRetryDelay)
            {

            }

        protected override bool ShouldRetryOn(Exception exception)
        {
            return exception.GetType() == typeof(DbUpdateException) ? true : false;
        }
    }
}