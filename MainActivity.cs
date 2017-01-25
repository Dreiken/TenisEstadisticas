using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Newtonsoft.Json;

namespace TenisEstadisticas
{
    [Activity(Label = "TenisEstadisticas", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Main);

            Button empezarButton = FindViewById<Button>(Resource.Id.EmpezarButton);
            EditText jugador1Text = FindViewById<EditText>(Resource.Id.Jugador1Text);
            EditText jugador2Text = FindViewById<EditText>(Resource.Id.Jugador2Text);
            EditText sacadorText = FindViewById<EditText>(Resource.Id.SacadorText);

            Core.Set[] sets = new Core.Set[3];
            sets[0] = new Core.Set(new string[2] { "0", "0" }, new bool[2] { false, false }, new int[2] { 0, 0 }, new int[2] { 0, 0 },
                new int[2] { 0, 0 }, new int[2] { 0, 0 }, new int[2] { 0, 0 }, new int[2] { 0, 0 }, new int[2] { 0, 0 }, new int[2] { 0, 0 }, new int[2] { 0, 0 });
            sets[1] = new Core.Set(new string[2] { "0", "0" }, new bool[2] { false, false }, new int[2] { 0, 0 }, new int[2] { 0, 0 },
                new int[2] { 0, 0 }, new int[2] { 0, 0 }, new int[2] { 0, 0 }, new int[2] { 0, 0 }, new int[2] { 0, 0 }, new int[2] { 0, 0 }, new int[2] { 0, 0 });
            sets[2] = new Core.Set(new string[2] { "0", "0" }, new bool[2] { false, false }, new int[2] { 0, 0 }, new int[2] { 0, 0 },
                new int[2] { 0, 0 }, new int[2] { 0, 0 }, new int[2] { 0, 0 }, new int[2] { 0, 0 }, new int[2] { 0, 0 }, new int[2] { 0, 0 }, new int[2] { 0, 0 });

            Core.Partido partido = new Core.Partido(new string[2] { "0", "0" }, sets, new int[2] { 0, 0 }, new int[2] { 0, 0 }, new int[2] { 0, 0 }, new int[2] { 0, 0 },
                new int[2] { 0, 0 }, new int[2] { 0, 0 }, new int[2] { 0, 0 }, new int[2] { 0, 0 }, new bool[2] { false, false }, 0);

            empezarButton.Enabled = false;

            jugador1Text.TextChanged += (sender, e) =>
            {
                if ((jugador1Text.Text.Length > 1) && (jugador2Text.Text.Length > 1) && ((sacadorText.Text == jugador1Text.Text) || (sacadorText.Text == jugador2Text.Text))) empezarButton.Enabled = true;
                else empezarButton.Enabled = false;
                partido.jugador[0] = jugador1Text.Text;
                partido.sets[0].jugador[0] = jugador1Text.Text;
                partido.sets[1].jugador[0] = jugador1Text.Text;
                partido.sets[2].jugador[0] = jugador1Text.Text;
            };

            jugador2Text.TextChanged += (sender, e) =>
            {
                if ((jugador1Text.Text.Length > 1) && (jugador2Text.Text.Length > 1) && ((sacadorText.Text == jugador1Text.Text) || (sacadorText.Text == jugador2Text.Text))) empezarButton.Enabled = true;
                else empezarButton.Enabled = false;
                partido.jugador[1] = jugador2Text.Text;
                partido.sets[0].jugador[1] = jugador2Text.Text;
                partido.sets[1].jugador[1] = jugador2Text.Text;
                partido.sets[2].jugador[1] = jugador2Text.Text;
            };

            sacadorText.TextChanged += (sender, e) =>
            {
                if ((jugador1Text.Text.Length > 1) && (jugador2Text.Text.Length > 1) && ((sacadorText.Text == jugador1Text.Text) || (sacadorText.Text == jugador2Text.Text))) empezarButton.Enabled = true;
                else empezarButton.Enabled = false;
                if (sacadorText.Text == jugador1Text.Text)
                {
                    partido.sacador[0] = true; partido.sacador[1] = false;
                }
                if (sacadorText.Text == jugador2Text.Text)
                {
                    partido.sacador[0] = false; partido.sacador[1] = true;
                }
            };
            

            empezarButton.Click += delegate 
            {
                var serPartido = JsonConvert.SerializeObject(partido);
                var PartidoActivity = new Intent(this, typeof(PartidoActivity));
                PartidoActivity.PutExtra("passPartido", serPartido);
                StartActivity(PartidoActivity);
            };
            

        }
    }
}

