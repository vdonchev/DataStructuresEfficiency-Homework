namespace _03.CollectionOfProductsDemo
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using CollectionOfProducts;
    using Newtonsoft.Json;

    public static class CollectionOfProductsDemo
    {
        // NOTE: This demo uses a mock_data.json file stored in the project root folder.
        public static void Main()
        {
            var productsDb = new ProductsColection();

            var file = File.ReadAllLines(@"../../mock_data.json");
            foreach (var line in file)
            {
                productsDb.Add(JsonConvert.DeserializeObject<Product>(line));
            }

            var productsInPriceRange = productsDb.FindProducts(3, 20);
            PrintProducts(productsInPriceRange, "Products in price range [3..20]");

            var productsByTitle = productsDb.FindProducts("Aspirin");
            PrintProducts(productsByTitle, "Products by title \"Aspirin\"");

            var productsByTitleAndPrice =
                productsDb.FindProducts("Aspirin", 82.68M, SearchType.ByTitleAndPrice);
            PrintProducts(productsByTitleAndPrice, "Products by title \"Aspirin\" and Price: 82.68");

            var productsBySuplierAndPrice =
                productsDb.FindProducts("Sandoz Inc", 57.83M, SearchType.BySuplierAndPrice);
            PrintProducts(productsBySuplierAndPrice, "Products by suplier \"Sandoz Inc\" and Price: 57,83");

            var productsByTitleAndPriceRange =
                productsDb.FindProducts("Aspirin", 15, 40, SearchType.ByTitleAndPrice);
            PrintProducts(productsByTitleAndPriceRange, "Products by title \"Aspirin\" and in price range [15..40]");

            var productsBySuplierAndPriceRange =
                productsDb.FindProducts("Sandoz Inc", 1, 100, SearchType.BySuplierAndPrice);
            PrintProducts(productsBySuplierAndPriceRange, "Products by suplier \"Sandoz Inc\" and in price range [1..50]");
        }

        private static void PrintProducts(IEnumerable<Product> products, string title)
        {
            Console.WriteLine('+' + new string('-', 40) + '+');
            Console.WriteLine(title);
            Console.WriteLine('+' + new string('-', 40) + '+');
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }

            Console.WriteLine('+' + new string('-', 40) + '+');
            Console.WriteLine();
        }
    }
}
