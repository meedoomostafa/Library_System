using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class BookTransactions
    {
        Books BooksTransactions;
        public Customers customers;
        ColorsOfTextBase SelectColor;
        public BookTransactions(Books AdminBooks, Customers Customers) 
        {
            BooksTransactions = AdminBooks;
            customers = Customers;
            SelectColor = new ColorsOfTextBase();
        }
        public void BookBorrow()
        {
            BooksTransactions.ShowAllBooks();
            Console.WriteLine("\nEnter Name Of Department : ");
            string DepartmentName = Console.ReadLine().Trim();
            Console.WriteLine("Enter The Id Of Book : ");
            int Id;
            while (!int.TryParse(Console.ReadLine(), out Id))
            {
                Console.WriteLine("Please Enter Intger Number");
            }
            int DepartmentIndex = BooksTransactions.GetDepartmentIndex(DepartmentName);
            BooksTransactions.ShowBookInformation(Id, DepartmentIndex);
            SelectColor.Red();
            Console.WriteLine("Do You Really Reserved This Book ? (1-yes 2-no)");
            dynamic Choose = Console.Read();

            switch (Choose)
            {
                case int i:
                    {
                        if (i == 1 && BooksTransactions.books[DepartmentIndex][Id - 1].amount > 0) 
                        { 
                            SelectColor.Green();
                            Console.WriteLine("Compeleted Successfully");
                            Books.Book book = new Books.Book();
                            book = BooksTransactions.books[DepartmentIndex][Id-1];
                            book.amount -= 1;
                            BooksTransactions.books[DepartmentIndex][Id - 1] = book;
                        }
                        else
                        {
                            SelectColor.Red();
                            Console.WriteLine("Reservation Canceled");
                        }
                        break;
                    }
                    case string s:
                    {
                        if((s == "Yes" ||s == Choose.ToUpper() || s == Choose.ToLower()) && BooksTransactions.books[DepartmentIndex][Id - 1].amount > 0)
                        {
                            SelectColor.Green();
                            Console.WriteLine("Compeleted Successfully");
                            Books.Book book = new Books.Book();
                            book = BooksTransactions.books[DepartmentIndex][Id - 1];
                            book.amount -= 1;
                            BooksTransactions.books[DepartmentIndex][Id - 1] = book;
                        }
                        else
                        {
                            SelectColor.Red();
                            Console.WriteLine("Reservation Canceled");
                        }
                        break;
                    }
            }
        }
        public void Buying()
        {
        V:
            var pair = this.customers.LoginCase;
            if (pair.First)
            {
                BooksTransactions.ShowAllBooks();
                Console.WriteLine("\nEnter Name Of Department : ");
                string DepartmentName = Console.ReadLine().Trim();
                Console.WriteLine("Enter The Id Of Book : ");
                int Id;
                while(!int.TryParse(Console.ReadLine(), out Id))
                {
                    Console.WriteLine("Please Enter Intger Number");
                }
                int DepartmentIndex = BooksTransactions.GetDepartmentIndex(DepartmentName);
                BooksTransactions.ShowBookInformation(Id, DepartmentIndex);
                SelectColor.Red();
                Console.WriteLine("Will You Really Buy This Book ? (1-yes 2-no)");
                dynamic Choose = Console.Read();

                switch (Choose)
                {
                    case int i:
                        {
                            if (i == 1 && BooksTransactions.books[DepartmentIndex][Id - 1].amount > 0 && BooksTransactions.books[DepartmentIndex][Id - 1].price < customers[pair.Second].VisaAmount )
                            {
                                SelectColor.Green();
                                Console.WriteLine("Compeleted Successfully");
                                Books.Book book = new Books.Book();
                                book = BooksTransactions.books[DepartmentIndex][Id - 1];
                                book.amount -= 1;
                                BooksTransactions.books[DepartmentIndex][Id - 1] = book;
                                var Customer = customers[pair.Second];
                                Customer.VisaAmount -= book.price;
                                customers[pair.Second] = Customer;
                            }
                            else
                            {
                                SelectColor.Red();
                                Console.WriteLine("Reservation Canceled");
                            }
                            break;
                        }
                    case string s:
                        {
                            if ((s == "Yes" || s == Choose.ToUpper() || s == Choose.ToLower()) && BooksTransactions.books[DepartmentIndex][Id - 1].amount > 0 && BooksTransactions.books[DepartmentIndex][Id - 1].price < customers[pair.Second].VisaAmount)
                            {
                                SelectColor.Green();
                                Console.WriteLine("Compeleted Successfully");
                                Books.Book book = new Books.Book();
                                book = BooksTransactions.books[DepartmentIndex][Id - 1];
                                book.amount -= 1;
                                BooksTransactions.books[DepartmentIndex][Id - 1] = book;
                                var Customer = customers[pair.Second];
                                Customer.VisaAmount -= book.price;
                                customers[pair.Second] = Customer;
                            }
                            else
                            {
                                SelectColor.Red();
                                Console.WriteLine("Reservation Canceled");
                            }
                            break;
                        }
                }
            }
            else
            {
                var access = customers.Login();
                if (access.First)
                    goto V;
            }
        }
    }
}
