namespace MagStore.Web.Models.ShoppingCart
{
    public class AddToCartPostInputModel
    {   
        public string ProductId { get; set; }

        public string Code { get; set; }

        public string Colour { get; set; }

        public string Size { get; set; }
    }
}