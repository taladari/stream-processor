using StreamProcessor.BL.Consumers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamProcessor.BL.Producers
{
    public abstract class Producer<T>
    {
        protected event Func<T, Task> DataArrived;
        protected event Action<Exception> Error;

        public void Subscribe(IConsumer<T> consumer)
        {
            DataArrived += consumer.OnDataArrived;
            Error += consumer.OnError;
        }

        public abstract void Start();

        protected virtual void OnDataArrived(T arg) => DataArrived?.Invoke(arg);
        protected virtual void OnError(Exception ex) => Error?.Invoke(ex);
    }
}
