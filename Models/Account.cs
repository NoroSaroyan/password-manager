using PasswordManager.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
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

        public string Id { get; set; }

        public Account()
        {

        }
        public Account(string id, string platform, string login, string password)
        {
            this.Id = id;
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

        public static bool Delete(string Id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Persist Security Info=False;User ID=noriksDB;Password=1234;Initial Catalog=PasswordManager;Server=DESKTOP-P18ASPH\SQLEXPRESS;"))
                {
                    conn.Open();

                    string query = "DELETE FROM[dbo].[accounts] WHERE id = @userIdParameterName";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add("@userIdParameterName", SqlDbType.NVarChar, 50).Value = Id;

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

        public static bool Edit(Account newAcc)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Persist Security Info=False;User ID=noriksDB;Password=1234;Initial Catalog=PasswordManager;Server=DESKTOP-P18ASPH\SQLEXPRESS;"))
                {
                    conn.Open();
                    //UPDATE [dbo].[accounts] SET [platform] = 'Instagram' ,[login] = 'username',[password] = 'pswrd' WHERE[Id] = 'id'
                    string query = "UPDATE [dbo].[accounts] SET [platform] = @platform,[login] = @login,[password] = @password WHERE [Id] = @id";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar, 50).Value = newAcc.Id;
                    cmd.Parameters.Add("@platform", SqlDbType.NVarChar, 50).Value = newAcc.Platform;
                    cmd.Parameters.Add("@login", SqlDbType.NVarChar, 50).Value = newAcc.Login;
                    cmd.Parameters.Add("@password", SqlDbType.NVarChar, 50).Value = newAcc.Password;
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
