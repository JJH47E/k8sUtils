using K8sUtils.Factories;
using K8sUtils.ProcessHosts;
using K8sUtils.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace K8sUtils;

public static class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        var runner = host.Services.GetRequiredService<ConsoleRunner>();
        runner.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices(c =>
            {
                c.AddSingleton<IKubectlService, KubectlService>();
                // Use Stub implementation for testing
                c.AddSingleton<IKubectlHost, StubKubectlHost>();
                c.AddSingleton<IPodActionFrameFactory, PodActionFrameFactory>();
                c.AddSingleton<ConsoleRunner>();
                c.AddSingleton<MainWindow>();
            });
    }
}