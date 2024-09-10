
namespace Library
{
    internal class Program
    {
        const string AdminPassword = "Admin123";
        public class Base
        {
            public Admin Admin;

            public Base(int BooksNumberPerDepartment)
            {
                Admin = new Admin(BooksNumberPerDepartment);
            }
        }
        
        public delegate void NewPrices(Books.Book books , int NewBookPrice) ;
        static void Main(string[] args)
        {
            WaitForUserInput();
            Console.Clear();

            Console.Write($"{"Which Number Of Books Per Department : ",10}");
            int NumberOfBooksPerDepartment;
            while (!int.TryParse(Console.ReadLine(), out NumberOfBooksPerDepartment))
            {
                Console.WriteLine("Please enter a valid number.");
            }

            Base @base = new Base(NumberOfBooksPerDepartment);
        A:
            Console.WriteLine($"{"+++++++++++++++++++++++++++++++++++++",20}");
            Console.WriteLine($"{"|              Main                 |",20}");
            Console.WriteLine($"{"| [1] Admin                         |",20}");
            Console.WriteLine($"{"| [2] Customer                      |",20}");        
            Console.WriteLine($"{"| [3] Exist The Library             |",20}");
            Console.WriteLine($"{"+++++++++++++++++++++++++++++++++++++",20}");

            Console.Write("\nEnter Your Choice *(please enter number)*: ");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 1 || choice > 3))
            {
                Console.WriteLine("Please enter a valid number (1-3).");
            }

            switch (choice)
            {
                case 1:
                    HandleAdmin(ref @base);
                    Console.Clear();
                    break;
                case 2:
                    HandleCustomer(ref @base);
                    Console.Clear();
                    break;
                case 3:
                    return;
            }
            goto A;
        }

        private static void HandleAdmin(ref Base @base)
        {
            Console.Write("\nEnter The Password: ");
            string pass = Console.ReadLine().Trim();
            while (AdminPassword != pass)
            {
                Console.WriteLine("Incorrect Password! Try again.");
                Console.Write("Enter The Password: ");
                pass = Console.ReadLine().Trim();
            }

            Console.WriteLine("\n{0,20}","+++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("{0,10}", "| [1] Adding Books                  |");
            Console.WriteLine("{0,10}", "| [2] Delete Book                   |");
            Console.WriteLine("{0,20}", "| [3] Edit Book                     |");
            Console.WriteLine("{0,20}", "| [4] Back To Main                  |");
            Console.WriteLine("{0,20}","+++++++++++++++++++++++++++++++++++++");

            Console.Write("\n","Enter Your Choice *(please enter number)*: ");
            int ch;
            while (!int.TryParse(Console.ReadLine(), out ch) || (ch < 1 || ch > 4))
            {
                Console.WriteLine("Please enter a valid number (1-4).");
            }

            Console.Clear();

            switch (ch)
            {
                case 1:
                    Console.Clear();
                    @base.Admin.AddingBooks();
                    Console.Clear();
                    break;
                case 2:
                    Console.Clear();
                    @base.Admin.DeleteBook();
                    Console.Clear();
                    break;
                case 3:
                    Console.Clear();
                    @base.Admin.EditBook();
                    Console.Clear();
                    @base.Admin.BooksConfigration.NewEventPrice += Books_NewEventPrice;
                    break;
                case 4:
                    break;
            }
            
        }

        private static void HandleCustomer(ref Base @base)
        {
            V:
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("| [1] Sign Up                       |");
            Console.WriteLine("| [2] Login                         |");
            Console.WriteLine("| [3] Back To Main                  |");
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++");

            Console.Write("\nEnter Your Choice *(please enter number)*: ");
            int c;
            while (!int.TryParse(Console.ReadLine(), out c) || (c < 1 || c > 3))
            {
                Console.WriteLine("Please enter a valid number (1-3).");
            }

            Console.Clear();

            switch (c)
            {
                case 1:
                    Console.Clear();
                    @base.Admin.customers.AddAccount();
                    WaitForUserInput();
                    Console.Clear();
                    break;
                case 2:
                    Console.Clear();
                    @base.Admin.customers.Login();
                    WaitForUserInput();
                    Console.Clear();
                    break;
                case 3:
                    return;
            }
            if (c == 1)
            {
                goto V;
            }
            else
            {
                CustomerTransactions(ref @base);
                return;
            }
        }

        private static void CustomerTransactions(ref Base @base)
        {
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("| [1] Borrow Book                   |");
            Console.WriteLine("| [2] Buy Book                      |");
            Console.WriteLine("| [3] Back To The Main              |");
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++");

            Console.Write("\nEnter Your Choice *(please enter number)*: ");
            int c;
            while (!int.TryParse(Console.ReadLine(), out c) || (c < 1 || c > 3))
            {
                Console.WriteLine("Please enter a valid number (1-3).");
            }
            switch (c)
            {
                case 1:
                    Console.Clear();
                    @base.Admin.customers.transactions.BookBorrow();
                    WaitForUserInput();
                    Console.Clear();
                    break;
                case 2:
                    Console.Clear();
                    @base.Admin.customers.transactions.Buying();
                    WaitForUserInput();
                    Console.Clear();
                    break;
                case 3:
                    return;
            }
            
        }

        private static void Books_NewEventPrice(Books.Book books, int NewBookPrice)
        {
            if (books.price > NewBookPrice)
                Console.WriteLine($"The {books.name} Price Decreased From {books.price}$ To {NewBookPrice}$!!");
            else if (books.price < NewBookPrice)
                Console.WriteLine($"The {books.name} Price Increased From {books.price}$ To {NewBookPrice}$!!");
            else
                Console.WriteLine($"The {books.name} Was Edited But Price Not Changed!!");
        }

        private static void WaitForUserInput()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}
