using HomeWork_Basic_09_L030_Mod_1;
using System.IO;
using System.Text;

Console.WriteLine(" ---- Home work 9 Mod 1 ----\n");

var imageDownloader = new ImageDownloader();
imageDownloader.ImageStartedNotify += DisplayStartedMessage;
imageDownloader.ImageCompletedNotify += DisplayCompletedMessage;
imageDownloader.FileAvailableNotify += DisplayFileAvailable;
imageDownloader.FileNotAvailableNotify += DisplayFileNotAvailable;

CancellationTokenSource cts = new CancellationTokenSource();
CancellationToken token = cts.Token;

var fileName = "picture";
var urlList = imageDownloader.GetUrlListFromTxtFile("UrlList.txt");
var taskList = new List<Task>();

foreach(var url in urlList)
{
    taskList.Add(imageDownloader.DownloadAsync(url, fileName));
}


while (imageDownloader.UrlListIsOpen)
{
    Console.WriteLine("\nPress \"A\" to exit or any other key to check the download status\n");
    
    var command = Console.ReadLine();
    if (command == "A")
    {
        cts.Cancel();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n Operation interrupted");
        Console.WriteLine("\n ------ !Exit! ------");
        Console.ResetColor();
        break;
    }

    if (Task.WhenAll(taskList).IsCompleted)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("All files downloaded!!!");
        Console.ResetColor();
    }
    else
    {
        for (int i = 0; i < taskList.Count; i++)
        {
            Console.WriteLine(
                $"File: {imageDownloader.DownloadList[i]}\t\t" +
                $"Download is completed: {taskList[i].IsCompleted}\t\t" +
                $"Task ID: {taskList[i].Id}");
        }
    }
}

Console.ReadLine();



/////////////////////////////////////////////////////////////////////////////////////////////////////////

void DisplayStartedMessage(string message)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"{message}");
    Console.ResetColor();
}
void DisplayCompletedMessage(string message)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"{message}");
    Console.ResetColor();
}
void DisplayFileAvailable(string message)
{
    Console.WriteLine("-------------------------------------------");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"{message}");
    Console.ResetColor();
    Console.WriteLine("-------------------------------------------");
}
void DisplayFileNotAvailable(string message)
{
    Console.WriteLine("-------------------------------------------");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"{message}");
    Console.ResetColor();
    Console.WriteLine("-------------------------------------------");
}

/*
List<string> GetUrlListFromTxtFile(string puth)
{
    var urlList = new List<string>();
    Console.WriteLine("--------------------------------------------");
    Console.WriteLine($"File: {puth}");

    if (File.Exists(puth))
    {
        Console.WriteLine("File available");
        using (StreamReader reader = new StreamReader(puth))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                urlList.Add(line);
            }
        }
    }
    else
    {
        Console.WriteLine("File not available!");
    }
    Console.WriteLine("--------------------------------------------");

    return urlList; 
}
*/