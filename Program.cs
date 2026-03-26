// See https://aka.ms/new-console-template for more information
using BykeStoresConsoleApp.Services;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using BykeStoresConsoleApp.Models;

Console.WriteLine("Hello, World!");
// To read from an xml file app.Config
string str = System.Configuration.ConfigurationManager.ConnectionStrings["BykeCon"].ConnectionString;
// To read from a json file appsettings.json
IConfiguration config = new ConfigurationBuilder()
    //.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();
string Constr = config.GetConnectionString("BykeCon");

BykeStoresDAL bykeDal = new BykeStoresDAL(Constr);
//bykeDal.DisplayCustomerAndStaffs();
//Console.WriteLine("Enter Customer Id:");
//int custid = Convert.ToInt32(Console.ReadLine());
//bykeDal.DisplayCustomerPhone(custid);
int result = bykeDal.InsertCustomer();
if (result > 0)
    Console.WriteLine("Record inserted successfully");
else
    Console.WriteLine("Record not inserted, pls check");


 static void DisplayStaffs(BykeStoresDAL bd)
{
    List<Staff> AllStaffs = bd.GetStaffs();
    try
    {
        if (AllStaffs != null)
        {
            foreach (Staff stf in AllStaffs)
                Console.Write(stf.staff_id + ", " + stf.first_name + ", " + stf.last_name);
        }
    }
    catch (Exception ex)
    {
    }
}
