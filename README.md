# grpc-netcore-demo
一个基于.net core 3.0的gRpc客户端和服务端，并支持跨语言调用Go gRpc服务端

## 项目架构
### GrpcClient

gRpc客户端，访问了项目本身的gRpc服务，并测试访问了Go语言开发的gRpc服务，实现跨语言通信   
Golang开发的gRpc服务源码[https://github.com/flyingkoala/grpc-go-demo](https://github.com/flyingkoala/grpc-go-demo "点击这里")

### GrpcService

gRpc服务端   

RPC是基于http/2，是同时支持https和http协议的，走http协议，需如下配置Program.cs   

    public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                    {
                        // Setup a HTTP/2 endpoint without TLS.
                        options.ListenLocalhost(5001, o => o.Protocols =
                            HttpProtocols.Http2);
                    });
                    webBuilder.UseStartup<Startup>();
                });
   同时，客户端请求时需设置   

    AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);