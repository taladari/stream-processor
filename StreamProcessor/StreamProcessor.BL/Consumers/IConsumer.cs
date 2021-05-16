using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamProcessor.BL.Consumers
{
    public interface IConsumer<T>
    {
        Task OnDataArrived(T data);
        void OnError(Exception ex);
    }
}
