using StreamProcessor.BL.Models;
using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace StreamProcessor.BL.Producers
{
    public class MessagesProducer : IProducer<EventMessage>
    {
        public event Func<EventMessage, Task> DataArrived;
        public event Action<Exception> Error;
        private readonly CancellationToken _cancellationToken;
        private readonly StreamReader _stream;
        private Task _worker;

        public MessagesProducer(StreamReader stream, CancellationToken cancellationToken)
        {
            _stream = stream;
            _cancellationToken = cancellationToken;
        }

        public void Subscribe(Func<EventMessage, Task> action, Action<Exception> errorAction)
        {
            DataArrived += action;
            Error += errorAction;
        }

        public void Start()
        {
            _worker = Task.Factory.StartNew(InnerProcess, _cancellationToken);
        }

        private void InnerProcess()
        {
            while (!_cancellationToken.IsCancellationRequested && !_stream.EndOfStream)
            {
                var input = _stream.ReadLine();

                try
                {
                    var eventMessage = JsonSerializer.Deserialize<EventMessage>(input);
                    OnMessageArrived(eventMessage);
                }
                catch (Exception ex)
                {
                    OnError(new Exception($"Error parsing event message: {ex.Message}"));
                }
            }

            _cancellationToken.ThrowIfCancellationRequested();
        }

        private void OnMessageArrived(EventMessage message) => DataArrived?.Invoke(message);

        private void OnError(Exception ex) => Error?.Invoke(ex);
    }
}
