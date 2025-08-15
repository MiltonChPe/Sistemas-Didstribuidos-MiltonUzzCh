namespace FileDownload;

class Program
{
    public static async Task Main(string[] args)
    {
        var cancellationToken = new CancellationTokenSource().Token;
        var peer = new Peer();

        var task = peer.StartAsync(cancellationToken);

        if (args.Length > 0 && args[0] == "download")
        {   //1- ip servidor 2-puerto 3-nombre del archvio 4-ruta de guardado
            await peer.DownloadFileAsync(args[1], int.Parse(args[2]), args[3], args[4], cancellationToken);
        }
        else
        {
            Console.WriteLine("Waiting for other peers to connect...");
        }
        await task;
    }
}