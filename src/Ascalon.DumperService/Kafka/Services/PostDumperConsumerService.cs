﻿using Ascalon.DumperService.Features.Dumpers.PostDumper;
using Ascalon.Kafka;
using Ascalon.Kafka.Dtos;
using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ascalon.DumperService.Kafka.Services
{
    public class PostDumperConsumerService : Consumer<List<PostDumperCommand>>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly PostDumperConsumerServiceOptions _options;
        private readonly ILogger<PostDumperConsumerService> _logger;

        public PostDumperConsumerService(
            IServiceProvider serviceProvider,
            IOptions<PostDumperConsumerServiceOptions> options,
            ILogger<PostDumperConsumerService> logger) : base(logger)
        {
            _serviceProvider = serviceProvider;
            _options = options.Value;
            _logger = logger;
        }

        public override async Task ProcessMessage(string key, List<PostDumperCommand> postDumperCommands, TopicPartitionOffset offset)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();

                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                foreach (var postDumerCommand in postDumperCommands)
                    await mediator.Send(postDumerCommand);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in method: {nameof(ProcessMessage)}", ex);
            }
        }

        protected override KafkaConsumerConfig BuildConfiguration() => _options.Config;
    }
}
