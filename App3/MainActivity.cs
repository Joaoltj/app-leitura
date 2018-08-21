using Android.App;
using Android.Widget;
using Android.OS;
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
using System.Threading.Tasks;

namespace App3
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {


        private TextView txtView;

        private const int RequestCameraPermission = 13;
        private Button btn, btnCamera;
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

      



        }


        private void Ler(object sender, EventArgs e)
        {
            OrcEngine
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
           

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            bitmap = (Bitmap)data.Extras.Get("data");
            imgView.SetImageBitmap(bitmap);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }

    }
}

