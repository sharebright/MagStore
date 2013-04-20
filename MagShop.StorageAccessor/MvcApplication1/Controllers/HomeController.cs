using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Linq;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string str = "DefaultEndpointsProtocol=http;AccountName=[magshopstrg];AccountKey=[7CqBJtUagWciyMUq+U92WTaP5kJF6ID5pAhgtylgs1w7zA9hFFGUz5ejUBJCAyNmROEfF1GHA3GKa8Y9q+EcTg==]";
            StorageCredentials storageCredentials = new StorageCredentials("magshopstrg", "H3g2iG5XyUzX5BhUqBtw5VRtdSN++0aNhXDhKHpEJe2kDh/oSEOGbrhKDQ0AkdVdM0P+Ons+7mH2FMNzxNyddw==");

            CloudStorageAccount acct = new CloudStorageAccount(storageCredentials, false); //CloudStorageAccount.Parse(str);  

            CloudBlobClient blobClient = acct.CreateCloudBlobClient();

            // Retrieve a reference to a container. 
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");

            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();



            CloudBlobContainer container2 = blobClient.GetContainerReference("resources");
            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container2.GetBlockBlobReference("myblob.png");

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(@"C:\temp\barcode.png"))
            {
                blockBlob.UploadFromStream(fileStream);
            }

            var m = container2.ListBlobs(null, false).Select(x => x.Uri).ToList();
            return View(m);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
