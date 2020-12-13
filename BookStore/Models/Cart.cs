using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Cart
    {
        public List<CartLine> lineCollection = new List<CartLine>();
        public void AddItem(Book book, int quantity)
        {
            CartLine line = lineCollection
                .Where(bk => bk.Book.Id == book.Id)
                .FirstOrDefault();
            if(line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Book = book,
                    Quantity = quantity
                });
            } 
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Book book)
        {
            lineCollection.RemoveAll(bk => bk.Book.Id == book.Id);
        }
        public decimal ComputeSum()
        {
            return lineCollection.Sum(bk => bk.Book.Cost * bk.Quantity);
        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public int GetTotalAmount()
        {
            return lineCollection.Count;
        }
    }
    public class CartLine
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}