using System;
using System.Net.Http;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;
using Ascalon.DumperService.SreamService.Dtos;

namespace Ascalon.DumperService.SreamService
{
    public class StreamService : IStreamService
    {
        private static HttpClient _httpClient;
        private Queue<DumperData> _dataFromDumper = new Queue<DumperData>();

        public StreamService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5000/");
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
                    List<DumperData> dumperDatas = new List<DumperData>();

                    while (true)
                    {
                        _dataFromDumper.TryDequeue(out DumperData dumperData);

                        if (dumperData == null)
                            continue;

                        dumperDatas.Add(dumperData);

                        if (dumperDatas.Count == 50)
                            break;
                    }

                    var result = await _httpClient.PostAsync($"predict", new StringContent(JsonConvert.SerializeObject(dumperDatas, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }), System.Text.Encoding.UTF8, "application/json"));

                    var responseObject = await result.Content.ReadAsStringAsync();
                }
            }
            finally
            {
                Thread.ResetAbort();
            }
        }
    }
}
