using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LunchOffice_App.Droid.Code.Bean;

namespace LunchOffice_App.Droid.Code.SQLite
{
    public class SQLiteNguoiDung
    {
        List<BeanNguoiDung> listResult = null;
        string query = "";
        public List<BeanNguoiDung> getList()
        {
            try
            {
                query = "SELECT * FROM BeanNguoiDung";
                SQLiteDbUtilities.OpenConnection();
                //listResult=SQLiteDbUtilities.ExecuteReader(query);
                //SQLiteDataAdapter
                SQLiteDbUtilities.CloseConnection();
                return listResult;
            }
            catch
            {
                throw;
            }

        }
    }
}