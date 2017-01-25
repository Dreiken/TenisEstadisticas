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
using Newtonsoft.Json;

namespace TenisEstadisticas
{
    [Activity(Label = "TenisEstadisticas")]
    public class EstadisticasActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.Estadisticas);

            string deserPartido = Intent.GetStringExtra("passPartido");
            Core.Partido juego = JsonConvert.DeserializeObject<Core.Partido>(deserPartido);

            TextView estadisticasHeader = FindViewById<TextView>(Resource.Id.EstadisticasHeaderText);
            TextView jugador1Nombre = FindViewById<TextView>(Resource.Id.Jugador1NombreStats);
            TextView jugador2Nombre = FindViewById<TextView>(Resource.Id.Jugador2NombreStats);
            TextView noForzados1 = FindViewById<TextView>(Resource.Id.NoForzados1Text);
            TextView noForzados2 = FindViewById<TextView>(Resource.Id.NoForzados2Text);
            TextView aces1 = FindViewById<TextView>(Resource.Id.Aces1Text);
            TextView aces2 = FindViewById<TextView>(Resource.Id.Aces2Text);
            TextView winners1 = FindViewById<TextView>(Resource.Id.Winners1Text);
            TextView winners2 = FindViewById<TextView>(Resource.Id.Winners2Text);
            TextView dobleFaltas1 = FindViewById<TextView>(Resource.Id.DobleFaltas1Text);
            TextView dobleFaltas2 = FindViewById<TextView>(Resource.Id.DobleFaltas2Text);
            TextView porcentajePrimerG1 = FindViewById<TextView>(Resource.Id.PorcentajePrimeros1Text);
            TextView porcentajePrimerG2 = FindViewById<TextView>(Resource.Id.PorcentajePrimeros2Text);
            TextView porcentajeSegundoG1 = FindViewById<TextView>(Resource.Id.PorcentajeSegundos1Text);
            TextView porcentajeSegundoG2 = FindViewById<TextView>(Resource.Id.PorcentajeSegundos1Text);
            TextView gamesStats = FindViewById<TextView>(Resource.Id.GamesStatsText);

            Button primerSetButton = FindViewById<Button>(Resource.Id.PrimerSetButton);
            Button segundoSetButton = FindViewById<Button>(Resource.Id.SegundoSetButton);
            Button tercerSetButton = FindViewById<Button>(Resource.Id.TercerSetButton);
            Button partidoCompletoButton = FindViewById<Button>(Resource.Id.PartidoCompletoButton);

            jugador1Nombre.Text = juego.jugador[0];
            jugador2Nombre.Text = juego.jugador[1];
            
            switch (juego.mostrarEstadisticas)
            {
                case 0:

                    estadisticasHeader.Text = "PARTIDO";
                    if (juego.sets[2].jugado[0])
                    {
                        gamesStats.Text = juego.sets[0].games[0].ToString() + "/" + juego.sets[0].games[1].ToString() + " " + juego.sets[1].games[0].ToString() + "/" +
                            juego.sets[1].games[1].ToString() + " " + juego.sets[2].games[0].ToString() + "/" + juego.sets[2].games[1].ToString();
                    }
                    else
                    {
                        gamesStats.Text = juego.sets[0].games[0].ToString() + "/" + juego.sets[0].games[1].ToString() + " " + juego.sets[1].games[0].ToString() + "/" +
                            juego.sets[1].games[1].ToString();
                    }
                    noForzados1.Text = juego.noforzados[0].ToString();
                    noForzados2.Text = juego.noforzados[1].ToString();
                    aces1.Text = juego.aces[0].ToString();
                    aces2.Text = juego.aces[1].ToString();
                    winners1.Text = juego.winners[0].ToString();
                    winners2.Text = juego.winners[1].ToString();
                    dobleFaltas1.Text = juego.doblefaltas[0].ToString();
                    dobleFaltas2.Text = juego.doblefaltas[1].ToString();
                    porcentajePrimerG1.Text = Core.Estadisticas.SacarPorcentajeGanados(juego.primeros[0], juego.primerosGanados[0]);
                    porcentajePrimerG2.Text = Core.Estadisticas.SacarPorcentajeGanados(juego.primeros[1], juego.primerosGanados[1]);
                    porcentajeSegundoG1.Text = Core.Estadisticas.SacarPorcentajeGanados(juego.segundos[0], juego.segundosGanados[0]);
                    porcentajeSegundoG2.Text = Core.Estadisticas.SacarPorcentajeGanados(juego.segundos[1], juego.segundosGanados[1]);
                    primerSetButton.Enabled = true;
                    segundoSetButton.Enabled = true;
                    if (!juego.sets[2].jugado[0])
                    {
                        tercerSetButton.Enabled = false;
                    }
                    else tercerSetButton.Enabled = true;
                    partidoCompletoButton.Enabled = false;
                    break;
                case 1:
                    estadisticasHeader.Text = "1ER SET";
                    gamesStats.Text = juego.sets[0].games[0].ToString() + "/" + juego.sets[0].games[1].ToString();
                    noForzados1.Text = juego.sets[0].noforzados[0].ToString();
                    noForzados2.Text = juego.sets[0].noforzados[1].ToString();
                    aces1.Text = juego.sets[0].aces[0].ToString();
                    aces2.Text = juego.sets[0].aces[1].ToString();
                    winners1.Text = juego.sets[0].winners[0].ToString();
                    winners2.Text = juego.sets[0].winners[1].ToString();
                    dobleFaltas1.Text = juego.sets[0].doblefaltas[0].ToString();
                    dobleFaltas2.Text = juego.sets[0].doblefaltas[1].ToString();
                    porcentajePrimerG1.Text = Core.Estadisticas.SacarPorcentajeGanados(juego.sets[0].primeros[0], juego.sets[0].primerosGanados[0]);
                    porcentajePrimerG2.Text = Core.Estadisticas.SacarPorcentajeGanados(juego.sets[0].primeros[1], juego.sets[0].primerosGanados[1]);
                    porcentajeSegundoG1.Text = Core.Estadisticas.SacarPorcentajeGanados(juego.sets[0].segundos[0], juego.sets[0].segundosGanados[0]);
                    porcentajeSegundoG2.Text = Core.Estadisticas.SacarPorcentajeGanados(juego.sets[0].segundos[1], juego.sets[0].segundosGanados[1]);
                    primerSetButton.Enabled = false;
                    segundoSetButton.Enabled = true;
                    if (!juego.sets[2].jugado[0])
                    {
                        tercerSetButton.Enabled = false;
                    }
                    else tercerSetButton.Enabled = true;
                    partidoCompletoButton.Enabled = true;
                    break;
                case 2:
                    estadisticasHeader.Text = "2DO SET";
                    gamesStats.Text = juego.sets[1].games[0].ToString() + "/" + juego.sets[1].games[1].ToString();
                    noForzados1.Text = juego.sets[1].noforzados[0].ToString();
                    noForzados2.Text = juego.sets[1].noforzados[1].ToString();
                    aces1.Text = juego.sets[1].aces[0].ToString();
                    aces2.Text = juego.sets[1].aces[1].ToString();
                    winners1.Text = juego.sets[1].winners[0].ToString();
                    winners2.Text = juego.sets[1].winners[1].ToString();
                    dobleFaltas1.Text = juego.sets[1].doblefaltas[0].ToString();
                    dobleFaltas2.Text = juego.sets[1].doblefaltas[1].ToString();
                    porcentajePrimerG1.Text = Core.Estadisticas.SacarPorcentajeGanados(juego.sets[1].primeros[0], juego.sets[1].primerosGanados[0]);
                    porcentajePrimerG2.Text = Core.Estadisticas.SacarPorcentajeGanados(juego.sets[1].primeros[1], juego.sets[1].primerosGanados[1]);
                    porcentajeSegundoG1.Text = Core.Estadisticas.SacarPorcentajeGanados(juego.sets[1].segundos[0], juego.sets[1].segundosGanados[0]);
                    porcentajeSegundoG2.Text = Core.Estadisticas.SacarPorcentajeGanados(juego.sets[1].segundos[1], juego.sets[1].segundosGanados[1]);
                    primerSetButton.Enabled = true;
                    segundoSetButton.Enabled = false;
                    if (!juego.sets[2].jugado[0])
                    {
                        tercerSetButton.Enabled = false;
                    }
                    else tercerSetButton.Enabled = true;
                    partidoCompletoButton.Enabled = true;
                    break;
                case 3:
                    estadisticasHeader.Text = "3ER SET";
                    gamesStats.Text = juego.sets[2].games[0].ToString() + "/" + juego.sets[2].games[1].ToString();
                    noForzados1.Text = juego.sets[2].noforzados[0].ToString();
                    noForzados2.Text = juego.sets[2].noforzados[1].ToString();
                    aces1.Text = juego.sets[2].aces[0].ToString();
                    aces2.Text = juego.sets[2].aces[1].ToString();
                    winners1.Text = juego.sets[2].winners[0].ToString();
                    winners2.Text = juego.sets[2].winners[1].ToString();
                    dobleFaltas1.Text = juego.sets[2].doblefaltas[0].ToString();
                    dobleFaltas2.Text = juego.sets[2].doblefaltas[1].ToString();
                    porcentajePrimerG1.Text = Core.Estadisticas.SacarPorcentajeGanados(juego.sets[2].primeros[0], juego.sets[2].primerosGanados[0]);
                    porcentajePrimerG2.Text = Core.Estadisticas.SacarPorcentajeGanados(juego.sets[2].primeros[1], juego.sets[2].primerosGanados[1]);
                    porcentajeSegundoG1.Text = Core.Estadisticas.SacarPorcentajeGanados(juego.sets[2].segundos[0], juego.sets[2].segundosGanados[0]);
                    porcentajeSegundoG2.Text = Core.Estadisticas.SacarPorcentajeGanados(juego.sets[2].segundos[1], juego.sets[2].segundosGanados[1]);
                    primerSetButton.Enabled = true;
                    segundoSetButton.Enabled = true;
                    tercerSetButton.Enabled = false;
                    partidoCompletoButton.Enabled = true;
                    break;
            }

            primerSetButton.Click += delegate
            {
                juego.mostrarEstadisticas = 1;
                var serPartido = JsonConvert.SerializeObject(juego);
                var EstadisticasActivity = new Intent(this, typeof(EstadisticasActivity));
                EstadisticasActivity.PutExtra("passPartido", serPartido);
                StartActivity(EstadisticasActivity);
            };

            segundoSetButton.Click += delegate
            {
                juego.mostrarEstadisticas = 2;
                var serPartido = JsonConvert.SerializeObject(juego);
                var EstadisticasActivity = new Intent(this, typeof(EstadisticasActivity));
                EstadisticasActivity.PutExtra("passPartido", serPartido);
                StartActivity(EstadisticasActivity);
            };

            tercerSetButton.Click += delegate
            {
                juego.mostrarEstadisticas = 3;
                var serPartido = JsonConvert.SerializeObject(juego);
                var EstadisticasActivity = new Intent(this, typeof(EstadisticasActivity));
                EstadisticasActivity.PutExtra("passPartido", serPartido);
                StartActivity(EstadisticasActivity);
            };

            partidoCompletoButton.Click += delegate
            {
                juego.mostrarEstadisticas = 0;
                var serPartido = JsonConvert.SerializeObject(juego);
                var EstadisticasActivity = new Intent(this, typeof(EstadisticasActivity));
                EstadisticasActivity.PutExtra("passPartido", serPartido);
                StartActivity(EstadisticasActivity);
            };
        }
    }
}