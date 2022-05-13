using System;

namespace ST10085288_POE_Part1
{
    internal class Program
    {
        //global variables to be accessed by all methods
        public static bool bContinue = true;
        public static double monthlyIncome = 0;
        public static double monthlyHomePayment = 0;
        public static double purchasePrice = 0;
        public static double totalDeposit = 0;
        public static double interestRate = 0;
        public static double numMonths = 0;
        public static double monthlyRemainingMoney = 0;
        public static double totalexpenses = 0;
        //array to store the categories of expenses
        // (Troelsen and Japikse,2021:111,112,113,114,117,118)
        public static string[] category  =new string[6] {"tax deducted",
                                                         "groceries expenses",
                                                         "water and light expenses",                                                       
                                                         "travel cost expenses(includes petrol)",
                                                         "cellphone/telephone expenses",
                                                         "other expenses" };
        //array to store the users input of above categories and more
        // (Troelsen and Japikse,2021:111,112,113,114,117,118)
        public static double[] expenses = new double[7];
        static void Main(string[] args)
        {
            
            while (bContinue == true)// (Troelsen and Japikse,2021:96,97,98,99,100,101)
            {
                Console.WriteLine("=================================="
                            + "\nPERSONAL BUDGET PLANNER"
                            + "\n==================================");
                Entervalues();//calls method allowing the user to enter information
                CalculateMonthlyLivingExpenses();//calls method to calculate the monthly living expenses
                Display();//calls method to display a message for the user with the total monthly income and expenses
                          //and the the available/remaining money per month

                Console.WriteLine("Do you want to continue using this budget planner? " +
                              "\n(Y/y for yes or other key for no)");
                string sContinue = Console.ReadLine().ToUpper(); ;
                
                if (sContinue.Equals("Y"))// (Troelsen and Japikse,2021:97,98,99,100,101,102)
                {
                    bContinue = true;
                }
                else
                {
                    bContinue= false;
                    Environment.Exit(0);
                }
            }           
        }


        public static void Entervalues()//asks the user to enter all information
        {
            Console.Write("Enter gross monthly income(before deductions): R");
            monthlyIncome = double.Parse(Console.ReadLine());

            int i= 0;

            while (i < category.Length)// (Troelsen and Japikse,2021:96,97,98,99,100,101)
            {
                try// (Troelsen and Japikse,2021:279)
                {
                    // (Troelsen and Japikse,2021:111,112,113,114,117,118)
                    Console.Write("Enter monthly {0} : R", category[i]);
                    expenses[i] = double.Parse(Console.ReadLine());
                    i++;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("PLEASE ENTER A NUMBER ");
                }
            }
           
            Console.Write("Are you buying or renting property? \nEnter R/r for renting and any other key for buying: ");

            string sAccomadationOption = Console.ReadLine().ToUpper();
            if (sAccomadationOption.Equals("R"))// (Troelsen and Japikse,2021:97,98,99,100,101,102)
            {
                Renting();
            }
            else
            {
                Buying();
            }          
        }

        public static void Renting()//asks user to enter monthly payment for renting
        {
            Renting message = new Renting();//Displays the heading of the Renting section by calling
            message.HeadingMessage();         //the abstract method Heading message from the Renting class
           

            int i = 0;
            while (i < 1)// (Troelsen and Japikse,2021:96,97,98,99,100,101)
                try// (Troelsen and Japikse,2021:279)
                {
                    Console.Write("Enter monthly rental amount: R");
                    monthlyHomePayment = double.Parse(Console.ReadLine());
                    // (Troelsen and Japikse,2021:111,112,113,114,117,118)
                    expenses[6] = monthlyHomePayment;
                    i++;
                }
                catch(FormatException e)
                {
                    Console.WriteLine("PLEASE ENTER A NUMBER ");
                }      
        }

        public static void Buying()//asks user to enter payment information for a home loan
        {
            HomeLoan message = new HomeLoan();//Displays the heading of the Home Buying section by calling
            message.HeadingMessage();         //the abstract method Heading message from the HomeLoan class
            
            bool b = false;
            int i = 0;
            while (i < 1)// (Troelsen and Japikse,2021:96,97,98,99,100,101)
                try// (Troelsen and Japikse,2021:279)
                {
                    Console.Write("Enter purchase price of the property: R");
                    purchasePrice = double.Parse(Console.ReadLine());

                    Console.Write("Enter total deposit: R");
                    totalDeposit = double.Parse(Console.ReadLine());

                    Console.Write("Enter interest rate in percentage: ");
                    interestRate = (double.Parse(Console.ReadLine()) / 100);
                    i++;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("PLEASE ENTER A NUMBER ");
                }

            while (b == false)// (Troelsen and Japikse,2021:96,97,98,99,100,101)
            {
                 Console.Write("Enter number of months to repay(between 240 and 360 ): ");
                 numMonths = double.Parse(Console.ReadLine());

                if (numMonths < 240 || numMonths > 360)// (Troelsen and Japikse,2021:97,98,99,100,101,102)
                {
                    Console.WriteLine("Number of months must be between 240 and 360");
                    b = false;
                 }
                else
                 { 
                    b = true;
                 }
                }
         CalculateMonthlyHomeLoanRepayment();//calls the methid to calculate the monthly home loan repayment                     
        }


        public static void CalculateMonthlyHomeLoanRepayment()//calculates the monthly payment for a home loan by calling the
                                                              //abstract method from Homeloan class and displays a appropriate
                                                              //message if it is approved or not
        {
            HomeLoan homeLoan = new HomeLoan();

            double totalAfterDeposit = purchasePrice - totalDeposit;
            
            monthlyHomePayment = homeLoan.Calculate( totalAfterDeposit, interestRate,  numMonths);
            //(Troelsen and Japikse,2021:111,112,113,114,117,118)
            expenses[6] = monthlyHomePayment;
            Console.Write("-----------------------------------------" +
                          "\nMonthly home payment per month: R" + monthlyHomePayment);
            
            if ((monthlyIncome / 3) < monthlyHomePayment)// (Troelsen and Japikse,2021:97,98,99,100,101,102)
            {
                homeLoan.UnapprovedMessage();
            }
            else
            {
                homeLoan.ApprovedMessage();
            }
        }

        public static void CalculateMonthlyLivingExpenses()//calculates the available/remaining money per month and total expenses
        {
            totalexpenses = 0;
            for (int i = 0; i < expenses.Length; i++)// (Troelsen and Japikse,2021:94)
            {
                // (Troelsen and Japikse,2021:111,112,113,114,117,118)
                totalexpenses += expenses[i];
            }

            monthlyRemainingMoney = monthlyIncome - totalexpenses;
        }

        public static void Display()//display a message for the user with the total monthly income and expenses
                             //and the the available/remaining money per month
        {
            string text = "-----------------------------------------" +
                          "\nBUDGET PLANNING INFROMATION" +
                          "\n-----------------------------------------";
            text += "\nMonthly income: \tR" + Math.Round(monthlyIncome,2) +
                    "\n" + "Monthly expenses: \tR" + Math.Round(totalexpenses,2) +
                    "\n" + "Available money: \tR" + Math.Round(monthlyRemainingMoney,2) +
                    "\n" + "-----------------------------------------";
            Console.WriteLine(text);
        }
    }
}

//Reference list
//Troelsen, A and Japikse P. 2021. Pro C# 9 with.NET5 foundational principles and practices in programming. 10th ed. 
//New York. Apress Media
