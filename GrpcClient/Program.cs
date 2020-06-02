using Grpc.Net.Client;
using grpc_netcore_demo;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);//跨语言调用

                //访问项目自身的.net core grpc服务
                var channel = GrpcChannel.ForAddress("http://localhost:5001");
                var client = new Greeter.GreeterClient(channel);
                var reply = await client.SayHelloAsync(
                    new HelloRequest { Name = "World" });
                Console.WriteLine(reply.Message);

                //跨语言访问 golang grpc服务
                var channel_go = GrpcChannel.ForAddress("http://localhost:50051");
                var client_go = new Greeter.GreeterClient(channel_go);
                var reply_go = await client_go.SayHelloAsync(
                    new HelloRequest { Name = "World" });
                Console.WriteLine(reply_go.Message);
                Console.ReadKey();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            
        }
    }
}
