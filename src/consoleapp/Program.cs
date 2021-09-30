using System;
using System.Threading.Tasks;

namespace consoleapp
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            BlobManager.UploadBlob();
        }
    }
}
