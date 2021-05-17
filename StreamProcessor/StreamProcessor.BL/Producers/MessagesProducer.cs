using StreamProcessor.BL.Consumers;
using StreamProcessor.BL.Models;
using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace StreamProcessor.BL.Producers
{
    public class MessagesProducer : Producer<EventMessage>
    {
        private readonly CancellationToken _cancellationToken;
        private readonly StreamReader _stream;
        private Task _worker;

        public MessagesProducer(StreamReader stream, CancellationToken cancellationToken)
        {
            _stream = stream;
            _cancellationToken = cancellationToken;
        }

        public override void Start()
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
                    OnDataArrived(eventMessage);
                }
                catch (Exception ex)
                {
                    OnError(new Exception($"Error parsing event message: {ex.Message}"));
                }
            }

            _cancellationToken.ThrowIfCancellationRequested();
        }
    }
}
