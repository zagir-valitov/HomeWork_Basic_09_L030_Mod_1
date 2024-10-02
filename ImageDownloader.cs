using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_Basic_09_L030_Mod_1;

internal class ImageDownloader
{
    //public delegate void DownloadHandler(string message);
    //public event DownloadHandler? ImageStartedNotify;
    //public event DownloadHandler? ImageCompletedNotify;
    public event Action<string>? ImageStartedNotify;
    public event Action<string>? ImageCompletedNotify;
    public event Action<string>? FileAvailableNotify;
    public event Action<string>? FileNotAvailableNotify;
    public static int Count = 1;
    public List<string> DownloadList = [];
    public List<string> UrlList = [];
    public bool UrlListIsOpen;
    
    public async Task DownloadAsync(string uri, string fileName)
    {
        fileName = $"{fileName}_{Count++}.jpg";
        DownloadList.Add(fileName);
        ImageStartedNotify?.Invoke($"File {fileName}\tdownloaded started!");
        using (var myWebClient = new WebClient())
        {
            await myWebClient.DownloadFileTaskAsync(uri, fileName);
        }
        ImageCompletedNotify?.Invoke($"File {fileName}\tdownload completed!");        
    }

    public List<string> GetUrlListFromTxtFile(string puth)
    {
        //UrlList = new List<string>();

        if (File.Exists(puth))
        {
            UrlListIsOpen = true;
            FileAvailableNotify?.Invoke("URL list file available");
            using (StreamReader reader = new StreamReader(puth))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    UrlList.Add(line);
                }
            }
        }
        else
        {
            UrlListIsOpen = false;
            FileNotAvailableNotify?.Invoke("URL list file unavailable!");
        }
        return UrlList;
    }
}
