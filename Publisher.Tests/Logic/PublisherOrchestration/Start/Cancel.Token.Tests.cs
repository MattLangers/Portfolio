﻿using Database;
using Database.Models.DTO;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;
using Publisher.Logic;
using Publisher.Logic.Factories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Publisher.Tests.Logic.PublisherOrchestration.Start
{
    [TestFixture]
    public sealed class PublisherOrchestration_Start_Cancel_Token_Tests
    {
        private static readonly Guid guidForProduct1 = new Guid();
        private static readonly Guid guidForProduct2 = new Guid();

        private static readonly ProductDtoForPublishing productDto1 = new ProductDtoForPublishing() { Id = guidForProduct1 };
        private static readonly ProductDtoForPublishing productDto2 = new ProductDtoForPublishing() { Id = guidForProduct2 };

        private readonly List<ProductDtoForPublishing> products = new List<ProductDtoForPublishing>()
        {
            productDto1, productDto2
        };

        private readonly CancellationTokenSource cancelationServiceToken = new CancellationTokenSource();

        private readonly AutoMocker autoMocker = new AutoMocker();

        private readonly Mock<IQueueClientWrapper> mockQueueClientWrapper = new Mock<IQueueClientWrapper>();

        [OneTimeSetUp]
        public async Task Setup()
        {
            autoMocker.GetMock<IProductsDAL>().Setup(m => m.GetUnPublishedProducts()).Returns(Task.Run(() => products));

            autoMocker.GetMock<IQueueFactory>().Setup(m => m.CreateQueueClient()).Returns(Task.Run(() => mockQueueClientWrapper.Object)).Callback(() =>
            {
                cancelationServiceToken.Cancel();
            });

            await autoMocker.CreateInstance<Publisher.Logic.PublisherOrchestration>().Start(cancelationServiceToken.Token);
        }

        [Test]
        public void IProductsDAL_VerifyAll()
        {
            autoMocker.VerifyAll();
        }

        [Test]
        public void MockQueueClientWrapper_SendMessageAsync_Verify_Never_Called()
        {
            mockQueueClientWrapper.Verify(m => m.SendMessageAsync(It.IsAny<string>()), Times.Never);
        }
    }
}
