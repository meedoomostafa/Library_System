using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class Admin
    {
        public Books BooksConfigration;
        public Customers customers;
        public int BooksNumberOfEachDepartment;
        public Admin(int BooksNumberOfEachDepartment)
        {
            this.BooksNumberOfEachDepartment = BooksNumberOfEachDepartment;
            BooksConfigration = new Books(BooksNumberOfEachDepartment);
            customers = new Customers(BooksConfigration);
        }
        public void AddingBooks()
        {
            Console.Write("Enter Id Of Department To Add Books : ");
            int get;
            while (!int.TryParse(Console.ReadLine(), out get))
            {
                Console.WriteLine("Please enter a valid integer for the Price.");
            }
            BooksConfigration.Add(get);
        }
        public void DeleteBook()
        {
            BooksConfigration.DeleteBook();
        }
        public void EditBook()
        {
            BooksConfigration.EditBook();
        }
    }
}
