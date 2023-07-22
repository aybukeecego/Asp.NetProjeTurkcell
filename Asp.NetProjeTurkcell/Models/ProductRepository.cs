namespace Asp.NetProjeTurkcell.Models
{
    public class ProductRepository

    {
        private static List<Product> _products = new List<Product>();



        public List<Product> GetAll() => _products;

        public void Add(Product newProduct) => _products.Add(newProduct);

    }
}
