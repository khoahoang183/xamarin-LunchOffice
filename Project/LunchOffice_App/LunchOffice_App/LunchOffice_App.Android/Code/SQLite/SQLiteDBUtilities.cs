using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LunchOffice_App.Droid.Code.SQLite
{
    public static class SQLiteDbUtilities
    {
        public static string _dbName = "LunchOffice.db";
        public static SQLiteConnection _dbConnection;
        public static string sql_table = "";

        public static void OpenConnection()
        {
            SQLiteConnection.CreateFile(_dbName);
            _dbConnection = new SQLiteConnection("Data Source=" + _dbName + ";Version=3;");
            _dbConnection.Open();
        }

        /// <summary>
        /// result tra ve la so dong duoc select
        /// </summary>
        /// <param name="query"></param>
        public static int ExecuteReader(string query)
        {
            SQLiteCommand command = new SQLiteCommand(query, _dbConnection);
            return command.ExecuteNonQuery();
        }
        /// <summary>
        /// result tra ve la so dong anh huong (insert, update)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string query)
        {           
            SQLiteCommand command = new SQLiteCommand(query, _dbConnection);
            return command.ExecuteNonQuery();
        }
        /// <summary>
        /// result tra ve la so dong anh huong (insert, update)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static void CloseConnection()
        {
            _dbConnection.Close();
        }

        /// <summary>
        /// true la ton tai
        /// </summary>
        /// <returns></returns>
        public static bool CheckDBExists()
        {
            if (File.Exists(_dbName))
            {
                return true;
            }
            return false;
        }
        public static void createDB()
        {
            try
            {
                if (CheckDBExists() == true) // neu ton tai db moi add
                {
                    OpenConnection();
                    string sql_tableNguoiDung = "create table NguoiDung " +
                        "(" +
                        "MaNguoiDung uniqueidentifier," +
                        "TaiKhoan varchar(100)," +
                        "MatKhau varchar(100)," +
                        "HoTen nvarchar(500)," +
                        "GioiTinh bit," +
                        "NgaySinh dateTime," +
                        "HinhAnh varchar(500)," +
                        "DiaChiMacDinh uniqueidentifier," +
                        "Email varchar(150)," +
                        "LoaiNguoiDung int," +
                        "KhoaNguoiDung int" +
                        "MaOTP int," +
                        "Created datetime," +
                        "Modified datetime," +
                        "CreatedBy uniqueidentifier," +
                        "ModifiedBy uniqueidentifier," +
                        ")";
                    //test

                    ExecuteNonQuery(sql_tableNguoiDung);
                    CloseConnection();

                    //test
                }             
            }
            catch
            {
                throw;
            }
        }


    }
}