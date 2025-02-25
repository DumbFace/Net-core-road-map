using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Moq;
using NetCoreAPI_Mongodb.SignalRHub;

namespace NetCoreAPI_Test
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
    public class UnitTest1
    {
        //[Theory]
        //[InlineData(null, 0)]
        //public void Test_Integer(int? integerNull, int integerNotNull)
        //{
        //    var expectedIntegerDefaultNull = default(int?);
        //    var expectedIntegerDefaultNotNull = default(int);
        //    var result = new UnitTest1();
        //    Assert.Equal(expectedIntegerDefaultNull, integerNull);
        //    Assert.Equal(expectedIntegerDefaultNotNull, integerNotNull);
        //}

        [Theory]
        [InlineData("", null)]
        public void Test_StringEMpty(string empty, string strNull)
        {
            var expectedStringEmpty = String.Empty;
            Assert.Equal(expectedStringEmpty, empty);
            Assert.NotEqual(expectedStringEmpty, strNull);
        }

        [Fact]
        public async Task Test_SendNotifyAsync()
        {
            var mockClients = new Mock<IHubCallerClients>();
            var mockGroup = new Mock<IClientProxy>();
            mockClients.Setup(clients => clients.Group(It.IsAny<string>())).Returns(mockGroup.Object);
            var mockDependency = new Mock<IHttpContextAccessor>();
            var chatHub = new ChatHub(mockDependency.Object)
            {
                Clients = mockClients.Object,
            };
            await chatHub.SendNotifyToGroup("A", "Hello Wolrd!!");
            await chatHub.SendNotifyToGroup(String.Empty, "Hello Wolrd!!");
            await chatHub.SendNotifyToGroup("\uD83D\uDE04", "Hello Wolrd!!");
            await chatHub.SendNotifyToGroup(null, "Hello Wolrd!!");

            await chatHub.SendNotifyToGroup("A", String.Empty);
            await chatHub.SendNotifyToGroup("B", "\uD83D\uDE04");
            await chatHub.SendNotifyToGroup("A", null);
            await chatHub.SendNotifyToGroup("A", "Hello Wolrd!!");

            await chatHub.SendNotifyToGroup("\t", "\t");

            //mockClients.Verify(clients => clients.Group(groupName), Times.Once);
            //mockGroup.Verify(group => group.SendAsync("ReceiveNotify", message, default), Times.Once);
        }
    }
}