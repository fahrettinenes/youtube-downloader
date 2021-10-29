using System;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace YoutubeDownloader
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            YoutubeClient youtubeClient = new YoutubeClient();
            Console.Write("Please write youtube url: ", Console.ForegroundColor = ConsoleColor.Cyan);
            var url = Console.ReadLine();
            Console.Write("1) MP4\n2) MP3\n", Console.ForegroundColor = ConsoleColor.Yellow);
            var section = Console.ReadLine();
            if(section == "1")
            {
                var video = await youtubeClient.Videos.GetAsync(url);
                Console.WriteLine("Downloading: " + video.Title);
                var manifest = await youtubeClient.Videos.Streams.GetManifestAsync(video.Id);
                var willDownloadVideo = manifest.GetVideoStreams().GetWithHighestVideoQuality();
                await youtubeClient.Videos.Streams.DownloadAsync(willDownloadVideo, Environment.CurrentDirectory + $@"/{video.Id}.mp4");
            }
            else if(section == "2")
            {
                var video = await youtubeClient.Videos.GetAsync(url);
                Console.WriteLine("Downloading: " + video.Title);
                var manifest = await youtubeClient.Videos.Streams.GetManifestAsync(video.Id);
                var willDownloadSong = manifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                await youtubeClient.Videos.Streams.DownloadAsync(willDownloadSong, Environment.CurrentDirectory + $@"/{video.Id}.mp3");
            }
            Console.ReadLine();
        }
    }
}
