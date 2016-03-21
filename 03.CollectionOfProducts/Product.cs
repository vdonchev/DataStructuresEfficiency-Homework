namespace _03.CollectionOfProducts
{
    using System;

    public class Product : IComparable<Product>
    {
        public Product(int id, string title, string suplier, decimal price)
        {
            this.Id = id;
            this.Title = title;
            this.Suplier = suplier;
            this.Price = price;
        }

        public int Id { get; }

        public string Title { get; }

        public string Suplier { get; }

        public decimal Price { get; }

        public int CompareTo(Product other)
        {
            return this.Id.CompareTo(other.Id);
        }

        public override string ToString()
        {
            return $"[{this.Id:000}] {this.Title} ({this.Suplier}) - {this.Price:C}";
        }
    }
}