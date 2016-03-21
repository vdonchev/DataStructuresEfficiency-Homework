namespace _03.CollectionOfProducts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using _00.ExtensionMethods;
    using Wintellect.PowerCollections;

    public class ProductsColection : IProductsColection
    {
        private readonly Dictionary<int, Product> productsById =
            new Dictionary<int, Product>();

        private readonly OrderedDictionary<decimal, SortedSet<Product>> productsByPrice =
            new OrderedDictionary<decimal, SortedSet<Product>>();

        private readonly Dictionary<string, SortedSet<Product>> productsByTitle =
            new Dictionary<string, SortedSet<Product>>();

        private readonly Dictionary<string, OrderedDictionary<decimal, SortedSet<Product>>> productsByTitleAndPrice =
            new Dictionary<string, OrderedDictionary<decimal, SortedSet<Product>>>();

        private readonly Dictionary<string, OrderedDictionary<decimal, SortedSet<Product>>> productsBySuplierAndPrice =
            new Dictionary<string, OrderedDictionary<decimal, SortedSet<Product>>>();

        public void Add(int id, string title, string suplier, decimal price)
        {
            var product = new Product(id, title, suplier, price);
            if (this.productsById.ContainsKey(id))
            {
                this.Remove(id);
            }

            // Add by ID
            this.productsById.AddValue(id, product);

            // Add by Price
            this.productsByPrice.AddValueToKey(price, product);

            // Add by Title
            this.productsByTitle.AddValueToKey(title, product);

            // Add by Title & Price
            this.productsByTitleAndPrice.EnsureKeyExists(title);
            this.productsByTitleAndPrice[title].AddValueToKey(price, product);

            // Add by Suplier & Price
            this.productsBySuplierAndPrice.EnsureKeyExists(suplier);
            this.productsBySuplierAndPrice[suplier].AddValueToKey(price, product);
        }

        public void Add(Product product)
        {
            if (this.productsById.ContainsKey(product.Id))
            {
                this.Remove(product.Id);
            }

            // Add by ID
            this.productsById.AddValue(product.Id, product);

            // Add by Price
            this.productsByPrice.AddValueToKey(product.Price, product);

            // Add by Title
            this.productsByTitle.AddValueToKey(product.Title, product);

            // Add by Title & Price
            this.productsByTitleAndPrice.EnsureKeyExists(product.Title);
            this.productsByTitleAndPrice[product.Title].AddValueToKey(product.Price, product);

            // Add by Suplier & Price
            this.productsBySuplierAndPrice.EnsureKeyExists(product.Suplier);
            this.productsBySuplierAndPrice[product.Suplier].AddValueToKey(product.Price, product);
        }

        public bool Remove(int id)
        {
            Product product;
            if (!this.productsById.TryGetValue(id, out product))
            {
                return false;
            }

            // Remove by ID
            this.productsById.Remove(id);

            // Remove by Price
            this.productsByPrice[product.Price].Remove(product);

            // Remove product by Title
            this.productsByTitle[product.Title].Remove(product);

            // Remove by Title & Price
            this.productsByTitleAndPrice[product.Title][product.Price].Remove(product);

            return true;
        }

        public IEnumerable<Product> FindProducts(decimal start, decimal end)
        {
            var results = new SortedSet<Product>();

            var productsInRange = this.productsByPrice.Range(start, true, end, true);
            foreach (var productResult in productsInRange)
            {
                foreach (var product in productResult.Value)
                {
                    results.Add(product);
                }
            }

            return results;
        }

        public IEnumerable<Product> FindProducts(string title)
        {
            return this.productsByTitle.GetValuesForKey(title);
        }

        public IEnumerable<Product> FindProducts(string key, decimal price, SearchType searchType)
        {
            var collection = this.GetCollection(searchType);
            if (!collection.ContainsKey(key))
            {
                return Enumerable.Empty<Product>();
            }

            return collection[key].GetValuesForKey(price);
        }

        public IEnumerable<Product> FindProducts(
            string key,
            decimal start,
            decimal end,
            SearchType searchType)
        {
            var results = new SortedSet<Product>();

            var collection = this.GetCollection(searchType);
            if (!collection.ContainsKey(key))
            {
                return results;
            }

            var productsInPriceRange = collection[key].Range(start, true, end, true);
            foreach (var productsByPriceList in productsInPriceRange)
            {
                foreach (var product in productsByPriceList.Value)
                {
                    results.Add(product);
                }
            }

            return results;
        }

        private IDictionary<string, OrderedDictionary<decimal, SortedSet<Product>>> 
            GetCollection(SearchType searchType)
        {
            var productsRepo = typeof(ProductsColection)
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(f => f.Name.EndsWith(searchType.ToString()));

            if (productsRepo == null)
            {
                throw new NotImplementedException("Search type not implemented yet");
            }

            var colleciton = productsRepo.GetValue(this)
                    as Dictionary<string, OrderedDictionary<decimal, SortedSet<Product>>>;

            return colleciton;
        }
    }
}
