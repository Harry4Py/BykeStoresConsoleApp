using BykeStoresConsoleApp.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BykeStoresConsoleApp.Services
{
    internal class BykeStoresDAL
    {
        // All properties required for working with SQL Server DB
        public SqlConnection _Con;
        public SqlCommand _cmd;
        public SqlDataReader rd;
        public string _ConStr;
        public Staff staff;

        public BykeStoresDAL(string str)
        {
            _Con = null;
            _cmd = null;
            rd = null;
            
            _ConStr = str;
            staff = null;
        }

        // All methods for working with Staff table -CRUD
        public void DisplayStaffs()
        {
            List<Staff> staffs = new List<Staff>();
            // Step 1
            _Con = new SqlConnection(_ConStr);
            // Step 2
            _cmd = new SqlCommand("Select * from Sales.Staffs", _Con);

            try
            {
                if (_Con.State == ConnectionState.Closed)
                    // step 3
                    _Con.Open();
                // step 4 - Execute the command
                rd = _cmd.ExecuteReader();
                // Step 5 - traverse through to read all the records
                while (rd.Read())
                {
                    Console.Write(rd["Staff_Id"] + ", " + rd["first_Name"] + ", " + rd["last_Name"] + ", " + rd["email"] + "\n");
                }

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Something went wrong - {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong - {ex.Message}");
            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                    // step 6
                    _Con.Close();
            }
        }
        public List<Staff> GetStaffs()
        {
            List<Staff> staffs = new List<Staff>();
            // Step 1
            _Con = new SqlConnection(_ConStr);
            // Step 2
            _cmd = new SqlCommand("Select * from Sales.Staffs", _Con);

            try
            {
                if (_Con.State == ConnectionState.Closed)
                    // step 3
                    _Con.Open();
                // step 4 - Execute the command
                rd = _cmd.ExecuteReader();
                // Step 5 - traverse through to read all the records
                while (rd.Read())
                {
                    //Console.Write(rd["Staff_Id"] + ", " + rd["first_Name"] + ", " + rd["last_Name"] + ", " + rd["email"] + "\n");
                    staff = new Staff();
                    staff.staff_id = Convert.ToInt32(rd["Staff_Id"]);
                    staff.first_name = rd["first_Name"].ToString();
                    staff.last_name = rd["last_Name"].ToString();
                    staff.phone = rd["Phone"].ToString();
                    staffs.Add(staff);
                }

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Something went wrong - {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Something went wrong - {ex.Message}");
                throw ex;
            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                    // step 6
                    _Con.Close();
            }
            return staffs;
        }

        public void DisplayCustomerAndStaffs()
        {
            List<Staff> staffs = new List<Staff>();
            // Step 1
            _Con = new SqlConnection(_ConStr);
            // Step 2
            _cmd = new SqlCommand("Select * from Sales.Staffs;Select * from Sales.Customers WHERE City='San Jose'", _Con);

            try
            {
                if (_Con.State == ConnectionState.Closed)
                    // step 3
                    _Con.Open();
                // step 4 - Execute the command
                rd = _cmd.ExecuteReader();
                // Step 5 - traverse through to read all the records

                    while (rd.Read())
                    {
                        Console.Write(rd["Staff_Id"] + ", " + rd["first_Name"] + ", " + rd["last_Name"] + ", " + rd["email"] + "\n");
                    }

                if (rd.NextResult())
                {
                    while (rd.Read())
                    {
                        Console.Write(rd["Customer_Id"] + ", " + rd["first_Name"] + ", " + rd["last_Name"] + ", " + rd["email"] + "\n");
                    }
                }

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Something went wrong - {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong - {ex.Message}");
            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                    // step 6
                    _Con.Close();
            }
        }

        public void DisplayCustomerPhone(int custId)
        {
            // Step 1
            _Con = new SqlConnection(_ConStr);
            // Step 2
            _cmd = new SqlCommand("Select phone from Sales.Customers WHERE customer_id =" + custId, _Con);

            try
            {
                if (_Con.State == ConnectionState.Closed)
                    // step 3
                    _Con.Open();
                // step 4 - Execute the command
                string PhoneNum = _cmd.ExecuteScalar().ToString();
                // Step 5 - traverse through to read all the records
                Console.Write($"Phone of Customer: {custId} is {PhoneNum}");
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Something went wrong - {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong - {ex.Message}");
            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                    // step 6
                    _Con.Close();
            }
        }

        public int InsertCustomer()
        {
            // Step 1
            _Con = new SqlConnection(_ConStr);
            Console.WriteLine("Enter First name:");
            string firstname = Console.ReadLine();
            Console.WriteLine("Enter Last name:");
            string lastname = Console.ReadLine();
            Console.WriteLine("Enter Email:");
            string email = Console.ReadLine();
            Console.WriteLine("Enter City:");
            string city = Console.ReadLine();
            Console.WriteLine("Enter 2 char State name:");
            string state = Console.ReadLine().ToUpper();
            int i = 0;
            try
            {
                if (_Con.State == ConnectionState.Closed)
                    // step 3
                    _Con.Open();
                string CmdStr = "INSERT INTO " +
                        "Sales.Customers(first_Name, last_name, email, city,state) " +
                        "VALUES(@fName, @lName, @Email, @City, @State)";
                using (_cmd = new SqlCommand(CmdStr, _Con))
                {
                    _cmd.CommandType = CommandType.Text;
                    _cmd.Parameters.AddWithValue("@fName", firstname);
                    _cmd.Parameters.AddWithValue("@lName", lastname);
                    _cmd.Parameters.AddWithValue("@Email", email);
                    _cmd.Parameters.AddWithValue("@City", city);
                    _cmd.Parameters.AddWithValue("@State", state);

                    i = _cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Something went wrong - {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong - {ex.Message}");
            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                    // step 6
                    _Con.Close();
            }
            return i;
        }

        public int UpdateCustomerPhone()
        {
            // Step 1
            _Con = new SqlConnection(_ConStr);
            Console.WriteLine("Enter Customer Phone (999) 999-9999:");
            string phone = Console.ReadLine();
            Console.WriteLine("Enter Customer Id:");
            int CustId = Convert.ToInt32(Console.ReadLine());

            int i = 0;
            try
            {
                if (_Con.State == ConnectionState.Closed)
                    // step 3
                    _Con.Open();
                string CmdStr = "UpdateCustomerPhone";
                using (_cmd = new SqlCommand(CmdStr, _Con))
                {
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.AddWithValue("@Phone", phone);
                    _cmd.Parameters.AddWithValue("@CustId", CustId);

                    i = _cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Something went wrong - {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong - {ex.Message}");
            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                    // step 6
                    _Con.Close();
            }
            return i;
        }

        public int DeleteCustomerById()
        {
            // Step 1
            _Con = new SqlConnection(_ConStr);

            Console.WriteLine("Enter Customer Id to delete:");
            int CustId = Convert.ToInt32(Console.ReadLine());

            int i = 0;
            try
            {
                if (_Con.State == ConnectionState.Closed)
                    // step 3
                    _Con.Open();
                string CmdStr = "DELETE FROM Sales.Customers Where Cust_Id = @CustId";
                using (_cmd = new SqlCommand(CmdStr, _Con))
                {
                    _cmd.CommandType = CommandType.Text;
                    _cmd.Parameters.AddWithValue("@CustId", CustId);
                    i = _cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Something went wrong - {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong - {ex.Message}");
            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                    // step 6
                    _Con.Close();
            }
            return i;
        }
    }
}
