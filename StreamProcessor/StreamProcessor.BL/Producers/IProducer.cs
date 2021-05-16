using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamProcessor.BL.Producers
{
    public interface IProducer<T>
    {
        event Func<T, Task> DataArrived;
        event Action<Exception> Error;
        void Subscribe(Func<T, Task> action, Action<Exception> errorAction);
        void Start();
    }
}
