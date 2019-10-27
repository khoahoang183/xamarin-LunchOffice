using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LunchOffice_App.Droid.Code.Utilities
{
    public static class Utilities_LoadImage
    {
        public static void LoadImageToImageView(string filename, ImageView imgView )
        {
            string _filename = Path.GetFileName(filename);
            string strongPath = Android.OS.Environment.ExternalStorageDirectory.Path;
            string filePath = System.IO.Path.Combine(strongPath, _filename);
            if (File.Exists(filePath))
            {
                imgView.SetImageDrawable(Drawable.CreateFromPath(filePath));
            }
        }
    }
}