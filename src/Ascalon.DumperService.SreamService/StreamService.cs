using Ascalon.DumperService.SreamService.Dtos;
using Ascalon.Kafka;
using System.Collections.Generic;
using System.Threading;

namespace Ascalon.DumperService.SreamService
{
    public class StreamService : IStreamService
    {
        private static Producer _producer;
        private Queue<DumperData> _dataFromDumper = new Queue<DumperData>();

        public StreamService(Producer producer)
        {
            _producer = producer;
            Thread sendData = new Thread(new ThreadStart(SendDataToNeuralNetwork));
            sendData.Start();
        }

        public void SetData(DumperData dumperData)
        {
            _dataFromDumper.Enqueue(dumperData);
        }

        public async void SendDataToNeuralNetwork()
        {
            try
            {
                while (true)
                {
                    List<DumperData> dumperData = new List<DumperData>();

                    while (true)
                    {
                        _dataFromDumper.TryDequeue(out DumperData dumperInfo);

                        if (dumperInfo == null)
                            continue;

                        dumperData.Add(dumperInfo);

                        if (dumperData.Count == 50)
                            break;
                    }

                    await _producer.Produce(null, dumperData, "neuralnetwork_data");
                }
            }
            finally
            {
                Thread.ResetAbort();
            }
        }
    }
}
