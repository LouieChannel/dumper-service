using Ascalon.DumperService.SreamService.Dtos;
using Ascalon.Kafka;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ascalon.DumperService.SreamService
{
    public class StreamService : IStreamService
    {
        private static Producer _producer;
        private static IMemoryCache _memoryCache;
        private static int _countOfNewIpAddress = 0;
        private ConcurrentDictionary<int, DumperData> _dumpersData = new ConcurrentDictionary<int, DumperData>();

        public StreamService(Producer producer, IMemoryCache memoryCache)
        {
            _producer = producer;
            _memoryCache = memoryCache;
        }

        public Task SetData(List<float> array, string ipAddress, string label)
        {
            return Task.Run(async () =>
            {
                int IdDumper = _memoryCache.GetOrCreate(ipAddress, option =>
                {
                    return ++_countOfNewIpAddress;
                });

                _dumpersData.TryGetValue(IdDumper, out DumperData dumperData);

                if (dumperData == null)
                {
                    _dumpersData.TryAdd(IdDumper, new DumperData() 
                    { 
                        Id = IdDumper, 
                        IpAddress = ipAddress,
                        Label = label,
                        Array = new List<List<float>>() 
                        { 
                            array 
                        } 
                    });
                    return;
                }

                if (dumperData.Array.Count != 50)
                    dumperData.Array.Add(array);
                else
                {
                    _dumpersData.TryUpdate(IdDumper, new DumperData() 
                    { 
                        Id = IdDumper, 
                        IpAddress = ipAddress,
                        Label = label,
                    }, dumperData);

                    await _producer.Produce(null, dumperData, "neuralnetwork_data");
                }
            });
        }
    }
}
