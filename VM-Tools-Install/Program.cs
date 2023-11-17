using System;
using System.Diagnostics;
using System.Net;

namespace VM_Tools_Install
{
    internal class Program
    {
        private const string VMwareUrl = "https://download3.vmware.com/software/WKST-PLAYER-1750/VMware-player-full-17.5.0-22583795.exe";
        private const string VirtualBoxUrl = "https://download.virtualbox.org/virtualbox/7.0.12/VirtualBox-7.0.12-159484-Win.exe";
        private const string MediaCreationToolUrl = "https://download.microsoft.com/download/9/e/a/9eac306f-d134-4609-9c58-35d1638c2363/MediaCreationTool22H2.exe";

        private const string VMwareFileName = "TEMP-VMware.exe";
        private const string VirtualBoxFileName = "TEMP-VirtualBox.exe";
        private const string MediaCreationToolFileName = "TEMP-MediaCreationTool.exe";

        static void Main(string[] args)
        {
            Console.Title = "VM Tool Install";
            Console.WriteLine("1. VMWare");
            Console.WriteLine("2. Virtual Box");
            Console.WriteLine("3. Windows Iso/Media Creation Tool");
            Console.WriteLine("Options: ");
            string option = Console.ReadLine();

            if (option == "1")
            {
                DownloadAndRunTool(VMwareUrl, VMwareFileName, "VMware");
            }
            if (option == "2")
            {
                DownloadAndRunTool(VirtualBoxUrl, VirtualBoxFileName, "Virtual Box");
            }
            if (option == "3")
            {
                DownloadAndRunTool(MediaCreationToolUrl, MediaCreationToolFileName, "Media Creation Tool");
            }
            else
            {
                Console.WriteLine("Invalid option. Please choose a valid option.");
            }
        }

        private static void DownloadAndRunTool(string url, string fileName, string toolName)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadProgressChanged += DownloadProgressCallback;

                    Console.WriteLine($"Downloading {toolName}...");

                    client.DownloadFileAsync(new Uri(url), fileName);

                    while (client.IsBusy)
                    {
                        System.Threading.Thread.Sleep(100);
                    }

                    Console.WriteLine("\nDownload complete!");
                    Console.WriteLine($"Opening {toolName}");
                    Process.Start(fileName);
                    Console.ReadKey();
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine($"Error downloading {toolName}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.Write($"\rProgress: {e.ProgressPercentage}%");
        }
    }
}
