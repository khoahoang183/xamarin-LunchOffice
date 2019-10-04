using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Data.SQLite;
using LunchOffice_App.Droid.Code.SQLite;
namespace LunchOffice_App.Droid.Code.SQLite
{
    public class SQLiteLogIn
    {
        public void InsertDB_TestData()
        {
            try
            {
                if (SQLiteDbUtilities.CheckDBExists() == true) // neu ton tai db moi add
                {
                    SQLiteDbUtilities.OpenConnection();
                    string insert_tableNguoiDung = "insert into NguoiDung (TaiKhoan, MatKhau) " +
                                                "values ('test', '123456')";
                    SQLiteDbUtilities.ExecuteNonQuery(insert_tableNguoiDung);
                    SQLiteDbUtilities.CloseConnection();
                    SQLiteDbUtilities.CloseConnection();
                }
            }
            catch
            {
                throw;
            }
        }
        public bool SQLiteLogIn_CheckLogIn(string Id, string Password)
        {
            try
            {
                if (SQLiteDbUtilities.CheckDBExists() == true) // neu ton tai db moi add
                {
                    SQLiteDbUtilities.OpenConnection();
                    string insert_tableNguoiDung = "Select * From NguoiDung " +
                                                   "Where TaiKhoan=" + Id + "And MatKhau = " + Password;
                    SQLiteDbUtilities.ExecuteReader(insert_tableNguoiDung);
                    SQLiteDbUtilities.CloseConnection();
                    return true;
                }
            }
            catch
            {
                throw;                
            }
            return false;
        }
    }
}