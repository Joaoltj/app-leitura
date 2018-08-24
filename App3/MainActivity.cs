using Android.App;
using Android.Widget;
using Android.OS;
using Com.Googlecode.Tesseract.Android;
using Android.Support.V7.App;
using Android.Views;
using Android.Gms.Vision;
using Android.Gms.Vision.Texts;
using Android.Util;
using Android.Graphics;
using Android.Runtime;
using Android.Support.V4.App;
using Android;
using Android.Content.PM;
using static Android.Gms.Vision.Detector;
using Java.Lang;
using Android.Support.V4.Content;
using System;
using Android.Content;
using Android.Provider;

using Tesseract;
using Tesseract.Droid;

namespace App3
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        
      
        private TextView txtView;
        private Android.Net.Uri uri; 
        private const int RequestCameraPermission = 13;
        private Button btn, btnCamera,btnGo;
        private TextView txt;
        private ImageView imgView;
        Bitmap bitmap = null;
        TextRecognizer textRecognizer;
        

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestCameraPermission:

                    if (grantResults[0] == Permission.Granted)
                    {

                    }
                    break;

            }

        }

       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
         


            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

          


            imgView = FindViewById<ImageView>(Resource.Id.imgView);
            txt = FindViewById<TextView>(Resource.Id.txtView);
            btn = FindViewById<Button>(Resource.Id.btn);
            btnCamera = FindViewById<Button>(Resource.Id.btnFoto);
            btnCamera.Click += Btn_Click;
                
  
            textRecognizer = new TextRecognizer.Builder(ApplicationContext).Build();
            btn.Click += Ler;
            btnGo = FindViewById<Button>(Resource.Id.btnGo);
            btnGo.Click += delegate
            {
                var intent = new Intent(this,typeof(Leitura));
                StartActivity(intent);
            };

      



        }


        private void Ler(object sender, EventArgs e)
        {
            
            if (bitmap != null)
            {
                if (!textRecognizer.IsOperational)
                {

                }
                else
                {

                    Frame frame = new Frame.Builder().SetBitmap(bitmap).Build();
                    var items = textRecognizer.Detect(frame);
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < items.Size(); i++)
                    {
                        var textBlock = items.ValueAt(i) as TextBlock;
                        sb.Append(textBlock.Value);
                        sb.Append("\n");

                    }
                    txt.Text = sb.ToString();
                }
            }
        }
           

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Android.App.Result resultCode, Intent data)
        {

            if (requestCode == 1)
            {
                bitmap = (Bitmap)data.Extras.Get("data");
                //uri = data.Data;
               imgView.SetImageBitmap(bitmap);
                //cropImagem();
            }
            else
            {
                if (data != null)
                {
                    bitmap = (Bitmap)data.Extras.Get("data");
                    imgView.SetImageBitmap(bitmap);
                   
                }

            }
            base.OnActivityResult(requestCode, resultCode, data);
          
        }

        private void Btn_Click(object sender, EventArgs e)
        {

            //abri galeria
            /*Intent intent = new Intent();
           intent.SetAction(Intent.ActionGetContent);
           intent.SetType("image/*");
           StartActivityForResult(intent, 1);*/

            Intent intent = new Intent(MediaStore.ActionImageCapture);
           StartActivityForResult(intent, 0); 
        }

        public void cropImagem()
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

