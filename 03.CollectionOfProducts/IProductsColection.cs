namespace _03.CollectionOfProducts
{
    using System.Collections.Generic;

    public interface IProductsColection
    {
        // Add new product(if the id already exists, the new product replaces the old one)
        void Add(int id, string title, string suplier, decimal price);

        // Remove product by id – returns true or false
        bool Remove(int id);

        // Find products in given price range[x…y] – returns the products sorted by id
        IEnumerable<Product> FindProducts(decimal start, decimal end);

        // Find products by title – returns the products sorted by id
        IEnumerable<Product> FindProducts(string title);

        // Find products by title + price – returns the products sorted by id
        // Find products by supplier + price – returns the products sorted by id
        IEnumerable<Product> FindProducts(string key, decimal price, SearchType searchType);

        // Find products by title + price range – returns the products sorted by id
        // Find products by supplier + price range – returns the products sorted by id
        IEnumerable<Product> FindProducts(string key, decimal start, decimal end, SearchType searchType);
    }
}