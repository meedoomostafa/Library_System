using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Library.Program;
public struct Pair<T1, T2>
{
    public T1 First { get; set; }
    public T2 Second { get; set; }

    public Pair(T1 first, T2 second)
    {
        First = first;
        Second = second;
    }
    public override string ToString() => $"{First} _ {Second}";
}
namespace Library
{
    
    internal class Books
    {
        public event NewPrices NewEventPrice;

        ColorsOfTextBase SelectColor;
        public struct Book
        {
            public string name {  get; set; }   
            public int id {  get; set; }

            public string date {  get; set; }
            public string author {  get; set; }

            public int price {  get; set; }
            public int amount {  get; set; }

            public Book(string name, int id, string date, string author, int price, int amount)
            {
                this.name = name;
                this.id = id;
                this.date = date;
                this.author = author;
                this.price = price;
                this.amount = amount;
            }
            public string BookExist (int amount)
            {
                if (amount > 0)
                    return($"{amount} Left");
                else
                    return("Out Of Stock");
            }
            public string ToString_N_ID() => $"{name}_{id}";
            public string ToString_ALL() => $"Book Name : {name}\tBook_Id : {id}\tBook Price : {price}\nBook Existence : {BookExist(amount)}\tBook Author : {author}\nThe Date is : {date}";
        }

        public List<List<Book>> books;
        private static readonly string[] Departments = { "AI", "IT", "CS", "IS", "DS" };
        static int[] CounterOfBooks = { 0, 0, 0, 0, 0 };
        private int NumberOfBooks;

        
        public List<Book> this[int index]
        {
            get => books[index];
            set => books[index] = value;
        }
        public Books(int columns)
        {
            SelectColor = new ColorsOfTextBase();
            this.NumberOfBooks = columns;
            int rows = 5;
            books = new List<List<Book>>();
            for (int i = 0; i < rows; i++)
            {
                var row = new List<Book>();
                books.Add(row);
                for (int j = 0; j < columns; j++)
                {
                    row.Add(new Book());
                }
            }
        }
        public void ShowDepartments()
        {
            Console.WriteLine("\n____________________________________________________________________________________________________");
            SelectColor.Cyan();
            Console.Write("Departments: ");
            foreach (var Depart in Departments)
            {
               Console.Write($"{Depart,15}" );
            }
            SelectColor.Gray_Baisc();
            Console.WriteLine("\n____________________________________________________________________________________________________");
        }
        public int GetDepartmentIndex(string DepartmentName)
        {
            DepartmentName = DepartmentName.ToUpper();
            for (int i = 0; i < DepartmentName.Length; ++i)
            {

                if (DepartmentName == Departments[i])
                    return i;
            }
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Make sure that you type the RIGHT name of Department");
            Console.BackgroundColor = ConsoleColor.Gray;
            return -1;
        }

        public void Add(int NumberOfDepartment)
        {
            int row = NumberOfDepartment - 1;
            for (int j = 0; j < this.NumberOfBooks; ++j)
            {
                
                Console.Write("Enter Book Name: ");
                string BookName = Console.ReadLine().Trim();

                Console.Write("Enter The Date: ");
                string Date = Console.ReadLine().Trim();

                Console.Write("Enter Author Name: ");
                string Author = Console.ReadLine().Trim();

                Console.Write($"Enter Price Of {BookName}: ");
                int Price;
                while (!int.TryParse(Console.ReadLine(), out Price))
                {
                    Console.WriteLine("Please enter a valid integer for the Price.");
                }

                Console.Write($"Enter Amount Of {BookName}: ");
                int Amount;
                while (!int.TryParse(Console.ReadLine(), out Amount))
                {
                    Console.WriteLine("Please enter a valid integer for the Amount.");
                }

                //Book book = new Book(BookName.ToLower(), j + 1,Date,Author,Price,Amount);
                books[row][j] = new Book(BookName.ToLower(), j + 1, Date, Author, Price, Amount);
            }
            ++CounterOfBooks[row]; 
        }
        public Pair<bool, int> SearchBook_ID(int id, string DepartmentName)
        {
            DepartmentName = DepartmentName.ToUpper();
            int DepartmentIndex = GetDepartmentIndex(DepartmentName);

            foreach (Book book in books[DepartmentIndex])
            {
                if (id == book.id)
                {
                    SelectColor.Green();
                    Console.WriteLine($"The ID ({id}) Exist ");
                    return new(true, id);
                }
            }
            SelectColor.Red();
            Console.WriteLine($"The ID ({id}) Don't Exist ");
            SelectColor.Gray_Baisc();
            return new(false, -1);
        }
        public Pair<bool, string> SearchBook_Name(string BookName, string DepartmentName)
        {

            DepartmentName = DepartmentName.ToUpper();
            int DepartmentIndex = GetDepartmentIndex(DepartmentName);

            foreach (Book book in books[DepartmentIndex])
            {
                if (BookName == book.name)
                {
                    SelectColor.Green();
                    Console.WriteLine($"The ID ({BookName}) Exist ");
                    return new(true,BookName );
                }
            }
            SelectColor.Red();
            Console.WriteLine($"The ID ({BookName}) Don't Exist ");
            SelectColor.Gray_Baisc();
            return new(false, null);
        }

        public int GetBookID_ByName(string BookName , int DepartPosition)
        {
            for (int i = 0; i < books[DepartPosition].Count; ++i)
            {
                if(BookName == books[DepartPosition][i].name)
                    return i+1;
            }
            return -1;
        }

        public void DeleteBook()
        {
            SelectColor.Red();
            
            Console.Write("Enter the ( DepartmentName ) of this Book : ");
            string DepartmentName = Console.ReadLine().Trim();
            Console.Write("Enter the Id of this Book : ");
            int obj;
            while (!int.TryParse(Console.ReadLine(),out obj))
            {
                Console.WriteLine("Please enter a valid integer for the Id.");
            }
            var exist= SearchBook_ID(obj, DepartmentName);  
        
            int DepartPosition = GetDepartmentIndex(DepartmentName);
            //int mid = (CounterOfBooks[DepartPosition] - 1) / 2;
                SelectColor.Green();
                if( exist.First )
                {
                    int index = exist.Second-1;
                    int NumberOfColumns = books[DepartPosition].Count-1;
                    int IdIndexSwap = books[DepartPosition][index].id;
                    int IdLastSwap = books[DepartPosition][NumberOfColumns].id;

                    Book temp = new Book();
                    temp = books[DepartPosition][index];
                    books[DepartPosition][index] = books[DepartPosition][NumberOfColumns];
                    books[DepartPosition][NumberOfColumns] = temp;

                    temp = books[DepartPosition][index];
                    temp.id = IdIndexSwap;
                    books[DepartPosition][index] = temp;

                    temp = books[DepartPosition][NumberOfColumns];
                    temp.id = IdLastSwap;
                    books[DepartPosition][NumberOfColumns] = temp;
                    CounterOfBooks[DepartPosition]--;
                    Console.WriteLine($"{obj} Book Deleted Successfuly");
                }
                SelectColor.Gray_Baisc();
        }
        public void EditBook() 
        {
            bool c = false;
            for (int i = 0;i<CounterOfBooks.Length;++i)
            {
                if (CounterOfBooks[i]>0)
                {
                    c = true;
                    break;
                }
                else
                    Add(i);
            }
            SelectColor.Yellow();
            Console.Write("\nEnter The Name Of The Department You Want To Edit : ");
            string DepartmentName = Console.ReadLine().Trim();

            int DepartmentIndex = GetDepartmentIndex(DepartmentName);

            Console.Write("\nEnter The Name Of The Book You Want To Edit : ");
            string BookName = Console.ReadLine().Trim();
            
            int index = GetBookID_ByName(BookName,DepartmentIndex)-1;
                    
            SelectColor.Red();
            Console.Write("Enter The New Book Name : ");
            string NewBookName = Console.ReadLine().Trim();
            Console.Write("Enter The New Book Date : ");
            string NewBookDate = Console.ReadLine().Trim();
            Console.Write("Enter The New Book Auther : ");
            string NewAuthorName = Console.ReadLine().Trim();
            Console.Write("Enter The New Book Price : ");
            int NewBookPrice;
            while (!int.TryParse(Console.ReadLine(), out NewBookPrice))
            {
                Console.WriteLine("Please enter a valid integer for the New Book Price.");
            }

            var Book = new Book();
            Book = books[DepartmentIndex][index];
            if ( NewEventPrice != null)
            {
                NewEventPrice(Book, NewBookPrice);
            }

            Book book = books[DepartmentIndex][index];
            book.name = NewBookName;
            book.date = NewBookDate;
            book.price = NewBookPrice;
            book.author = NewAuthorName;
            books[DepartmentIndex][index] = book;

            SelectColor.Green();
            Console.WriteLine("\nEdit Completed Successfuly");
            SelectColor.Gray_Baisc();
        }
        public void ShowAllBooks()
        {
            ShowDepartments();
            for (int i = 0; i < books[i].Count; i++) // Assuming all departments have the same number of books
            {
                Console.Write("Book Name _ ID:");
                for (int j = 0; j < books.Count; j++)
                {
                    Console.Write($"{books[j][i].ToString_N_ID(),15}");
                }
                Console.WriteLine();
            }
        }
        public void ShowBookInformation(int Id, int DepartmentIndex)
        {
            SelectColor.Cyan();
            if(DepartmentIndex>-1 && Id >0)
                books[DepartmentIndex][Id-1].ToString_ALL();
            else
                Console.WriteLine("No Book Exist In This Department");
        }
    }
}
