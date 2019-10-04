using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Graphics.Drawables;
using Java.IO;
using Java.Net;

namespace LunchOffice_App.Droid.Code.Utilities
{
            [Obsolete]
    public class Utilities_DownloadImageFromURL : AsyncTask<string, string, string>
    {
        [Obsolete]
        private ProgressDialog pDialog;
        private ImageView imgView;
        private Context context;
        public Utilities_DownloadImageFromURL(Context context, ImageView imgView)
        {
            this.context = context;
            this.imgView = imgView;
        }

        [Obsolete]
        protected override void OnPreExecute()
        {
            pDialog = new ProgressDialog(context);
            pDialog.SetMessage("Downloading file. Please wait...");
            pDialog.Indeterminate = false;
            pDialog.Max = 100;
            pDialog.SetProgressStyle(ProgressDialogStyle.Horizontal);
            pDialog.SetCancelable(true);
            pDialog.Show();
            base.OnPreExecute();
        }
        protected override string RunInBackground(params string[] @params)
        {
            string strongPath = Android.OS.Environment.ExternalStorageDirectory.Path;
            string filePath = System.IO.Path.Combine(strongPath, "download.jpg");
            int count;
            try
            {
                URL url = new URL(@params[0]);
                URLConnection connection = url.OpenConnection();
                connection.Connect();
                int LengthOfFile = connection.ContentLength;
                InputStream input = new BufferedInputStream(url.OpenStream(), LengthOfFile);
                OutputStream output = new FileOutputStream(filePath);
                byte[] data = new byte[1024];
                long total = 0;
                while ((count = input.Read(data)) != -1)
                {
                    total += count;
                    PublishProgress("" + (int)((total / 100) / LengthOfFile));
                    output.Write(data, 0, count);
                }
                output.Flush();
                output.Close();
                input.Close();
            }
            catch (Exception e)
            {
                throw;
            }
            return null;
        }

        protected override void OnPostExecute(string result)
        {
            string strongPath = Android.OS.Environment.ExternalStorageDirectory.Path;
            string filePath = System.IO.Path.Combine(strongPath, "download.jpg");
            pDialog.Dismiss();
            imgView.SetImageDrawable(Drawable.CreateFromPath(filePath));
        }
        protected override void OnProgressUpdate(params string[] values)
        {
            base.OnProgressUpdate(values);
            pDialog.SetProgressNumberFormat(values[0]);
            pDialog.Progress = int.Parse(values[0]);
        }
    }
}