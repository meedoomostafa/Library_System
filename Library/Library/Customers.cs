using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.Customers;

namespace Library
{
    internal class Customers
    {
        List<CustomerInformation> customers;
        ColorsOfTextBase SelectColors;
        Books BooksCustomers;
        public BookTransactions transactions;

        public bool Activate;
        public Pair<bool,int> LoginCase;
        public struct CustomerInformation 
        {
            string VisaCardId { get; set; }
            string VisaCardName { get; set; }
            string CustomerName { get; set; }

            public string CustomerPhone { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }

            public int VisaAmount { get; set; }
            public int AmountaGenerationCounter { get; set; }

            public int Id { get; set; }
            
            public CustomerInformation(string Visa_id, string Visa_card_Name, string Customer_Name,int Visa_Amount,string Customer_Phone,string Email,string Password)
            {
                this.Id += 1;
                this.AmountaGenerationCounter = 0;
                this.VisaCardName = Visa_card_Name;
                this.VisaCardId = Visa_id;
                this.CustomerName = Customer_Name;
                this.VisaAmount = Visa_Amount;
                this.CustomerPhone = Customer_Phone;
                this.Email = Email; 
                this.Password = Password;
            }
        }
        public Customers(Books Books)
        {
            SelectColors = new ColorsOfTextBase();
            customers = new List<CustomerInformation>();
            for (int i = 0;i<customers.Count;i++)
                customers[i]=new CustomerInformation();
            BooksCustomers = Books;
            transactions = new BookTransactions(Books,this);
        }
        public CustomerInformation this[int index]
        {
            get => customers[index];
            set => customers[index] = value;
        }
        public void AddAccount()
        {
            SelectColors.Green();
            Console.Write("Enter Your Name : ");
            string CustomerName = Console.ReadLine().Trim();
            Console.Write("Enter Your Email : ");
            string Email = Console.ReadLine().Trim();
            Console.Write("Enter Your Password : ");
            string Password = Console.ReadLine().Trim();
            Console.Write("Enter Your Visa Card Name : ");
            string VisaCardName = Console.ReadLine().Trim();
            Console.Write("Enter Your Visa Card Number : ");
            string VisaCardNumber = Console.ReadLine().Trim();
            Console.Write("Enter Your Phone Number : ");
            string PhoneNumber = Console.ReadLine().Trim();

            SelectColors.Gray_Baisc();

            CustomerInformation customer = new CustomerInformation();
            customer = new CustomerInformation(VisaCardNumber,VisaCardName.ToUpper(),CustomerName.ToLower(),AmountGeneration(ref customer),PhoneNumber,Email,Password);
        }
        public void EditAccountInformation(ref CustomerInformation information)
        {
            SelectColors.Green();
            Console.Write("Enter Your New Name : ");
            string NewCustomerName = Console.ReadLine().Trim();
            Console.Write("Enter Your New Email : ");
            string NewEmail = Console.ReadLine().Trim();
            Console.Write("Enter Your New Password : ");
            string NewPassword = Console.ReadLine().Trim();
            Console.Write("Enter Your New Visa Card Name : ");
            string NewVisaCardName = Console.ReadLine().Trim();
            Console.Write("Enter Your New Visa Card Number : ");
            string NewVisaCardNumber = Console.ReadLine().Trim();
            Console.Write("Enter Your New Phone Number : ");
            string NewPhoneNumber = Console.ReadLine().Trim();

            SelectColors.Gray_Baisc();

            CustomerInformation customer = new CustomerInformation(NewCustomerName, NewVisaCardName.ToUpper(), NewCustomerName.ToLower(), AmountGeneration(ref information), NewPhoneNumber,NewEmail,NewPassword);
            information = customer;
        }

        public int AmountGeneration(ref CustomerInformation customer)
        {
            if(customer.AmountaGenerationCounter >= 1)
            {
                return customer.VisaAmount;
            }
            Random random = new Random();
            int lowerBound = 10;
            int upperBound = 101; 
            int randomIntInRange = random.Next(lowerBound, upperBound);
            customer.AmountaGenerationCounter++;
            return randomIntInRange;
        }

        public Pair<bool,int> SearchCustomer(string Email,string Password)
        {
            for(int i = 0;i<customers.Count;i++)
            {
                if (customers[i].Email == Email && customers[i].Password == Password)
                    return new(true, customers[i].Id);
            }
            return new(false,0);
        }
        public Pair<bool,int> Login()
        {
            Console.Write("Enter Your Email : ");
            string Email = Console.ReadLine().Trim();
            Console.Write("Enter Your Password : ");
            string Password = Console.ReadLine().Trim();

            this.LoginCase= SearchCustomer(Email, Password);

            this.Activate = true;

            return LoginCase;
        }
    }
}
