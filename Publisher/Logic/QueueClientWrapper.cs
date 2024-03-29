﻿using Azure.Storage.Queues;

namespace Publisher.Logic
{
    internal class QueueClientWrapper : IQueueClientWrapper
    {
        private readonly QueueClient queueClient;

        public QueueClientWrapper(QueueClient queueClient)
        {
            this.queueClient = queueClient;
        }

        public Task SendMessageAsync(string jsonMessage)
        {
            return queueClient.SendMessageAsync(jsonMessage);
        }
    }
}
