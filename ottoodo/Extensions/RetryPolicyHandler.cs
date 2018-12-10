using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace OttooDo.Extensions
{
    public class RetryPolicyHandler<T> where T : Exception
    {
        private readonly Func<T, bool> exceptionFilter;
        public TimeSpan[] _waittimes { get; set; } = new TimeSpan[] {
                    TimeSpan.FromMilliseconds(100),
                    TimeSpan.FromMilliseconds(200),
                    TimeSpan.FromMilliseconds(400),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(4),
                    TimeSpan.FromSeconds(6) };

        public RetryPolicyHandler(Func<T, bool> exceptionFilter, TimeSpan[] waittimes = null)
        {
            this.exceptionFilter = exceptionFilter;
            if (waittimes != null)
                _waittimes = waittimes;

        }

        public virtual async Task DoWithRetry(Func<Task> action)
        {
            await Policy
                .Handle(exceptionFilter)
                .Or<SocketException>()
                .WaitAndRetryAsync(_waittimes)
                .ExecuteAsync(action);
        }

        public virtual A DoWithRetry<A>(Func<A> function)
        {
            return Policy
                .Handle(exceptionFilter)
                .Or<SocketException>()
                .WaitAndRetry(_waittimes)
                .Execute(function);
        }
    }
}
