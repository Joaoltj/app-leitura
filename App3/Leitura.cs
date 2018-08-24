using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Googlecode.Tesseract.Android;

namespace App3
{
    [Activity(Label = "Read")]
    public class Leitura : Activity
    {
        private const string TESS_DATA = "/tessdata";
        private TessBaseAPI tessBaseAPI;
        private TextView txtView;
        private Uri outputFileDir;
        private Button btn,btnRead;
        private ImageView img;
        Android.Net.Uri uri;
    
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.read);

            btn = FindViewById<Button>(Resource.Id.btnI);
            btnRead = FindViewById<Button>(Resource.Id.btnRead);
            btnRead.Click += delegate{
                GetText();
            };
            txtView = FindViewById<TextView>(Resource.Id.txtI);
            img = FindViewById<ImageView>(Resource.Id.imgI);
            btn.Click += Btn_Click;
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum]Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            // Bitmap bitmap = (Bitmap)data.Extras.Get("data");
            // img.SetImageBitmap(bitmap);
            //  GetText(bitmap);

            if (requestCode == 1)
            {
                // Bitmap bitmap = (Bitmap)data.Extras.Get("data");
                Bitmap bitmap = BitmapFactory.DecodeFile(data.Data.EncodedPath);
                img.SetImageBitmap(bitmap);
                GetText(bitmap);
                // uri = data.Data;
                // cropImagem();
            }
            else
            {
                if (data != null)
                {
                    Bitmap bitmap = (Bitmap)data.Extras.Get("data");

                 img.SetImageBitmap(bitmap);
                  GetText(bitmap);
                }
                  
                
            }

        }

        public void GetText(Bitmap bitmap = null)
        {
            TessBaseAPI api = new TessBaseAPI();
            bool result = api.Init("/storage/emulated/0/DCIM/Facebook", "por");

            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InSampleSize = 4;
          
            
            Bitmap bit = BitmapFactory.DecodeFile("/storage/emulated/0/DCIM/Facebook/joao.jpg");
            api.SetImage(bit);
            String recognizedText = api.UTF8Text;
            txtView.Text = recognizedText;
            api.End();
        }

        private void Btn_Click(object sender, EventArgs e)
        {

            //abri galeria
            Intent intent = new Intent();
            intent.SetAction(Intent.ActionGetContent);
            intent.SetType("image/*");
            StartActivityForResult(intent, 1);

            /*
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 1);*/
        }

        public void cropImagem( )
        {
           
                Intent intent = new Intent("com.android.camera.action.CROP");

                intent.SetDataAndType(uri, "image/*");
                intent.PutExtra("crop", "true");
                intent.PutExtra("outputX", 180);
                intent.PutExtra("outputY", 180);
                intent.PutExtra("aspectX", 3);
                intent.PutExtra("aspectY", 4);
                intent.PutExtra("scaleUpIfNeeded", true);
                intent.PutExtra("return-data", true);
                StartActivityForResult(intent, 0);
           
          
        }
    }
}