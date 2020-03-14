using Ascalon.Kafka.Dtos;

namespace Ascalon.DumperService.Kafka
{
    /// <summary>
    /// Информация о конфигурации Consumer в Kafka.
    /// </summary>
    public class PostDumperConsumerServiceOptions
    {
        public KafkaConsumerConfig Config { get; set; }
        public string TopicForProducePostDumper { get; set; }
        public string TopicForProduceErrors { get; set; }
    }
}
