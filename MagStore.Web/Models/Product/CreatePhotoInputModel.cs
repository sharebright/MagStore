using System.Web;

namespace MagStore.Web.Models.Product
{
    public class CreatePhotoInputModel
    {
        public string[] PhotoType { get; set; }
        public HttpPostedFileBase[] File { get; set; }
    }
}