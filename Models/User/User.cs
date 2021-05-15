using PasswordManager.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Models.User
{
    public class User
    {
        /// <summary>
        /// User's SignIn Username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User's SignIn password 
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// List of accounts owned by User 
        /// </summary>
        public ICollection<Account> Accounts { get; set; }

        public bool IsLogin { get; private set; }

        Guid LoginID { get; set; }

        public User()
        {
            this.Accounts = new Collection<Account>();
        }

        public User(string name, string password)
        {
            this.UserName = name;
            this.Password = password;
            this.Accounts = new Collection<Account>();
        }
        /// <summary>
        /// Signing in user from User's database 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Login()
        {
            if (this.IsLogin)
            {
                return true;
            }

            try
            {
                //"Persist Security Info=False;User ID=noriksDB;Password=1234;Initial Catalog=AdventureWorks;Server=DESKTOP-P18ASPH\SQLEXPRESS;"
                using (SqlConnection conn = new SqlConnection(@"Persist Security Info=False;User ID=noriksDB;Password=1234;Initial Catalog=PasswordManager;Server=DESKTOP-P18ASPH\SQLEXPRESS;"))
                {
                    conn.Open();

                    string query = "Select * from [PasswordManager].[dbo].[users] where  password=@Password and name=@UserName ";
                    //string query = string.Format("Select * from [PasswordManager].[dbo].[users] where name='{0}' and  password='{1}' ", user.UserName , user.Password);

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50).Value = this.Password;
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 50).Value = this.UserName;


                    //cmd.Parameters.AddWithValue("@Password", user.Password);
                    //cmd.Parameters.AddWithValue("@UserName", user.UserName);


                    using (SqlDataReader dr = cmd.ExecuteReader())
                        if (dr.Read())
                        {
                            object o = dr["name"];
                            Console.WriteLine(o);
                            this.IsLogin = true;
                            return true;
                        }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        public void AddAccount(Account account)
        {
            if (this.IsLogin)
            {
                this.Accounts.Add(account);
            }
            else
                this.Login();
        }

        public ICollection<Account> GetAccounts()
        {
            if (this.IsLogin == false)
            {
                return this.Accounts;
            }
            this.Accounts.Clear();
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Persist Security Info=False;User ID=noriksDB;Password=1234;Initial Catalog=PasswordManager;Server=DESKTOP-P18ASPH\SQLEXPRESS;"))
                {
                    conn.Open();
                    // TODO  
                    string query = "Select * from [PasswordManager].[dbo].[accounts] where username=@UserName";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 50).Value = this.UserName;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            object id = dr["id"];
                            object pl = dr["platform"];
                            object l = dr["login"];
                            object psw = dr["password"];
                            object uname = dr["username"];

                            Account account = new Account(id.ToString() , pl.ToString(), l.ToString(), psw.ToString());
                            this.Accounts.Add(account);
                        }
                    }
                    return this.Accounts;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }
    }
}
