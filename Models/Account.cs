using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace PasswordManager.Models
{
    public class Account
    {
        public string Platform { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

       

        public Account()
        {

        }
        public Account(string platform, string login, string password)
        {
            this.Platform = platform;
            this.Login = login;
            this.Password = password;
        }

        public static bool CreateAccount(Account acc, string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Persist Security Info=False;User ID=noriksDB;Password=1234;Initial Catalog=PasswordManager;Server=DESKTOP-P18ASPH\SQLEXPRESS;"))
                {
                    conn.Open();

                    string query = "INSERT INTO [dbo].[accounts]([id],[platform],[login],[password],[username])" +
                                          "VALUES(@id, @platform, @login, @password, @username) ";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar, 50).Value = Guid.NewGuid().ToString();
                    cmd.Parameters.Add("@platform", SqlDbType.NVarChar, 50).Value = acc.Platform;
                    cmd.Parameters.Add("@login", SqlDbType.NVarChar, 50).Value = acc.Login;
                    cmd.Parameters.Add("@password", SqlDbType.NVarChar, 50).Value = acc.Password;
                    cmd.Parameters.Add("@username", SqlDbType.NVarChar, 50).Value = username;

                    int affected = cmd.ExecuteNonQuery();
                    if (affected == 0)
                    {
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
                //return false;
            }
        }
    }
}
