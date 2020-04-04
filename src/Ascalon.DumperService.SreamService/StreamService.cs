using Ascalon.DumperService.SreamService.Dtos;
using Ascalon.Kafka;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Ascalon.DumperService.SreamService
{
    public class StreamService : IStreamService
    {
        private static Producer _producer;
        private static IMemoryCache _memoryCache;
        private static int _countOfNewIpAddress = 0;
        private ConcurrentDictionary<int, List<DumperData>> _dumpersData = new ConcurrentDictionary<int, List<DumperData>>();
        private Queue<DumperData> _dataFromDumper = new Queue<DumperData>();

        public StreamService(Producer producer, IMemoryCache memoryCache)
        {
            _producer = producer;
            _memoryCache = memoryCache;
        }

        public async void SetData(DumperData dumperData)
        {
            int IdDumper = _memoryCache.GetOrCreate(dumperData.IpAddress, option =>
            {
                return ++_countOfNewIpAddress;
            });

            dumperData.Id = IdDumper;

            _dumpersData.TryGetValue(IdDumper, out List<DumperData> dumperArray);

            if (dumperArray == null)
            {
                _dumpersData.TryAdd(IdDumper, new List<DumperData>() { dumperData });
                return;
            }

            if (dumperArray.Count != 50)
                dumperArray.Add(dumperData);
            else
            {
                _dumpersData.TryUpdate(IdDumper, new List<DumperData>(), dumperArray);
                await _producer.Produce(null, dumperArray, "neuralnetwork_data");
            }
        }
    }
}
