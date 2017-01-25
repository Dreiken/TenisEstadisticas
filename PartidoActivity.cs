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
    public class PartidoActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.Partido);

            string deserPartido = Intent.GetStringExtra("passPartido");
            Core.Partido juego = JsonConvert.DeserializeObject<Core.Partido>(deserPartido);
            
            
            bool[] sacador = juego.sacador;
            bool[] sacadorComienzo = sacador;
            bool ganogame = false;
            bool ganoset = false;
            bool ganoconsaque = false;
            bool[] cambioSet = new bool[2] { false, false };


            TextView jugador1Text = FindViewById<TextView>(Resource.Id.Jugador1Nombre);
            TextView jugador2Text = FindViewById<TextView>(Resource.Id.Jugador2Nombre);

            jugador1Text.Text = juego.jugador[0];
            jugador2Text.Text = juego.jugador[1];

            int[] score = new int[2] { 0, 0 };
            int[] games = new int[2] { 0, 0 };
            int[] sets = new int[2] { 0, 0 };
            int[] noforzados = new int[2] { 0, 0 };
            int[] aces = new int[2] { 0, 0 };
            int[] winners = new int[2] { 0, 0 };
            int[] primeros = new int[2] { 0, 0 };
            int[] primerosGanados = new int[2] { 0, 0 };
            int[] segundos = new int[2] { 0, 0 };
            int[] segundosGanados = new int[2] { 0, 0 };
            int[] doblesfaltas = new int[2] { 0, 0 };
            bool tiebreak = false;

            TextView Games1 = FindViewById<TextView>(Resource.Id.CantidadGames1);
            TextView Games2 = FindViewById<TextView>(Resource.Id.CantidadGames2);
            TextView Score1 = FindViewById<TextView>(Resource.Id.CantidadScore1);
            TextView Score2 = FindViewById<TextView>(Resource.Id.CantidadScore2);
            TextView Sets1 = FindViewById<TextView>(Resource.Id.CantidadSets1);
            TextView Sets2 = FindViewById<TextView>(Resource.Id.CantidadSets2);
            Button puntoButton1 = FindViewById<Button>(Resource.Id.PuntoButton1);
            Button puntoButton2 = FindViewById<Button>(Resource.Id.PuntoButton2);
            Button priSaqueButton1 = FindViewById<Button>(Resource.Id.PrimerSaqueButton1);
            Button priSaqueButton2 = FindViewById<Button>(Resource.Id.PrimerSaqueButton2);
            Button noForzadoButton1 = FindViewById<Button>(Resource.Id.NoForzadoButton1);
            Button noForzadoButton2 = FindViewById<Button>(Resource.Id.NoForzadoButton2);
            Button segSaqueButton1 = FindViewById<Button>(Resource.Id.SegundoSaqueButton1);
            Button segSaqueButton2 = FindViewById<Button>(Resource.Id.SegundoSaqueButton2);
            Button winnerButton1 = FindViewById<Button>(Resource.Id.WinnerButton1);
            Button winnerButton2 = FindViewById<Button>(Resource.Id.WinnerButton2);
            Button aceButton1 = FindViewById<Button>(Resource.Id.AceButton1);
            Button aceButton2 = FindViewById<Button>(Resource.Id.AceButton2);

            segSaqueButton1.Enabled = false;
            segSaqueButton2.Enabled = false;

            if (sacador[0] == false)
            {
                priSaqueButton1.Enabled = false;
                aceButton1.Enabled = false;
            }
            if (sacador[1] == false)
            {
                priSaqueButton2.Enabled = false;
                aceButton2.Enabled = false;
            }

            #region Botones


            puntoButton1.Click += (sender, e) =>
            {
                score[0]++;
                if (sacador[0] == true) ganoconsaque = true;
                Core.ScoreChecking.SumarSaques(ref sacador, ref primeros, ref ganoconsaque, ref primerosGanados, ref segundos, ref segundosGanados, ref priSaqueButton1, ref priSaqueButton2);
                if (tiebreak == false)
                {
                    Core.ScoreChecking.CheckVentaja(ref score);
                    Core.ScoreChecking.CheckScore(ref score, ref games, ref sets, ref tiebreak, ref ganogame, ref juego);
                    if (ganogame)
                    {
                        if (sacador[0] == true)
                        {
                            sacador[0] = false; sacador[1] = true;
                            ganogame = false;
                        }
                        else
                        {
                            sacador[0] = true; sacador[1] = false;
                            ganogame = false;
                        }
                    }
                    Core.ScoreChecking.ScoreDrawer(ref score, ref Score1, ref Score2);
                }
                else
                {
                    Core.ScoreChecking.CheckTiebreakScore(ref score, ref games, ref sets, ref tiebreak, ref ganoset, ref juego);
                    Score1.Text = score[0].ToString();
                    Score2.Text = score[1].ToString();
                    if (ganoset)
                    {
                        if (sacadorComienzo[0] == true)
                        {
                            sacadorComienzo[0] = false; sacadorComienzo[1] = true;
                            sacador[0] = false; sacador[1] = true;
                        }
                        else
                        {
                            sacadorComienzo[0] = true; sacadorComienzo[1] = false;
                            sacador[0] = true; sacador[1] = false;
                        }
                    }
                    else Core.ScoreChecking.SacadorTiebreakSwap(ref sacador, ref score);
                }
                Sets1.Text = sets[0].ToString();
                Sets2.Text = sets[1].ToString();
                Games1.Text = games[0].ToString();
                Games2.Text = games[1].ToString();

                if (sacador[0])
                {
                    segSaqueButton1.Enabled = false;
                    segSaqueButton2.Enabled = false;
                    priSaqueButton1.Enabled = true;
                    priSaqueButton2.Enabled = false;
                    aceButton1.Enabled = true;
                    aceButton2.Enabled = false;
                }
                else
                {
                    segSaqueButton1.Enabled = false;
                    segSaqueButton2.Enabled = false;
                    priSaqueButton1.Enabled = false;
                    priSaqueButton2.Enabled = true;
                    aceButton1.Enabled = false;
                    aceButton2.Enabled = true;
                }

            };
            puntoButton2.Click += (sender, e) =>
            {
                score[1]++;
                if (sacador[1] == true) ganoconsaque = true;
                Core.ScoreChecking.SumarSaques(ref sacador, ref primeros, ref ganoconsaque, ref primerosGanados, ref segundos, ref segundosGanados, ref priSaqueButton1, ref priSaqueButton2);
                if (tiebreak == false)
                {
                    Core.ScoreChecking.CheckVentaja(ref score);
                    Core.ScoreChecking.CheckScore(ref score, ref games, ref sets, ref tiebreak, ref ganogame, ref juego);
                    if (ganogame)
                    {
                        if (sacador[0] == true)
                        {
                            sacador[0] = false; sacador[1] = true;
                            ganogame = false;
                        }
                        else
                        {
                            sacador[0] = true; sacador[1] = false;
                            ganogame = false;
                        }
                    }
                    Core.ScoreChecking.ScoreDrawer(ref score, ref Score1, ref Score2);
                }
                else
                {
                    Core.ScoreChecking.CheckTiebreakScore(ref score, ref games, ref sets, ref tiebreak, ref ganoset, ref juego);
                    Score1.Text = score[0].ToString();
                    Score2.Text = score[1].ToString();
                    if (ganoset)
                    {
                        if(sacadorComienzo[0] == true)
                        {
                            sacadorComienzo[0] = false; sacadorComienzo[1] = true;
                            sacador[0] = false; sacador[1] = true;
                        }
                        else
                        {
                            sacadorComienzo[0] = true; sacadorComienzo[1] = false;
                            sacador[0] = true; sacador[1] = false;
                        }
                    }
                    else Core.ScoreChecking.SacadorTiebreakSwap(ref sacador, ref score);
                }
                Sets1.Text = sets[0].ToString();
                Sets2.Text = sets[1].ToString();
                Games1.Text = games[0].ToString();
                Games2.Text = games[1].ToString();

                if (sacador[0])
                {
                    segSaqueButton1.Enabled = false;
                    segSaqueButton2.Enabled = false;
                    priSaqueButton1.Enabled = true;
                    priSaqueButton2.Enabled = false;
                    aceButton1.Enabled = true;
                    aceButton2.Enabled = false;
                }
                else
                {
                    segSaqueButton1.Enabled = false;
                    segSaqueButton2.Enabled = false;
                    priSaqueButton1.Enabled = false;
                    priSaqueButton2.Enabled = true;
                    aceButton1.Enabled = false;
                    aceButton2.Enabled = true;
                }
            };
            priSaqueButton1.Click += (sender, e) =>
            {
                segSaqueButton1.Enabled = true;
                priSaqueButton1.Enabled = false;
                segSaqueButton2.Enabled = false;
                priSaqueButton2.Enabled = false;
                aceButton2.Enabled = false;
                aceButton1.Enabled = true;
            };
            priSaqueButton2.Click += (sender, e) =>
            {
                segSaqueButton1.Enabled = false;
                priSaqueButton1.Enabled = false;
                segSaqueButton2.Enabled = true;
                priSaqueButton2.Enabled = false;
                aceButton1.Enabled = false;
                aceButton2.Enabled = true;
            };
            segSaqueButton1.Click += (sender, e) =>
            {
                doblesfaltas[0]++;
                score[1]++;
                if (tiebreak == false)
                {
                    Core.ScoreChecking.CheckVentaja(ref score);
                    Core.ScoreChecking.CheckScore(ref score, ref games, ref sets, ref tiebreak, ref ganogame, ref juego);
                    if (ganogame)
                    {
                        if (sacador[0] == true)
                        {
                            sacador[0] = false; sacador[1] = true;
                            ganogame = false;
                        }
                        else
                        {
                            sacador[0] = true; sacador[1] = false;
                            ganogame = false;
                        }
                    }
                    Core.ScoreChecking.ScoreDrawer(ref score, ref Score1, ref Score2);
                }
                else
                {
                    Core.ScoreChecking.CheckTiebreakScore(ref score, ref games, ref sets, ref tiebreak, ref ganoset, ref juego);
                    Score1.Text = score[0].ToString();
                    Score2.Text = score[1].ToString();
                    if (ganoset)
                    {
                        if (sacadorComienzo[0] == true)
                        {
                            sacadorComienzo[0] = false; sacadorComienzo[1] = true;
                            sacador[0] = false; sacador[1] = true;
                        }
                        else
                        {
                            sacadorComienzo[0] = true; sacadorComienzo[1] = false;
                            sacador[0] = true; sacador[1] = false;
                        }
                    }
                    else Core.ScoreChecking.SacadorTiebreakSwap(ref sacador, ref score);
                }
                Sets1.Text = sets[0].ToString();
                Sets2.Text = sets[1].ToString();
                Games1.Text = games[0].ToString();
                Games2.Text = games[1].ToString();

                if (sacador[0])
                {
                    segSaqueButton1.Enabled = false;
                    segSaqueButton2.Enabled = false;
                    priSaqueButton1.Enabled = true;
                    priSaqueButton2.Enabled = false;
                    aceButton1.Enabled = true;
                    aceButton2.Enabled = false;
                }
                else
                {
                    segSaqueButton1.Enabled = false;
                    segSaqueButton2.Enabled = false;
                    priSaqueButton1.Enabled = false;
                    priSaqueButton2.Enabled = true;
                    aceButton1.Enabled = false;
                    aceButton2.Enabled = true;
                }
            };
            segSaqueButton2.Click += (sender, e) =>
            {
                doblesfaltas[1]++;
                score[0]++;
                if (tiebreak == false)
                {
                    Core.ScoreChecking.CheckVentaja(ref score);
                    Core.ScoreChecking.CheckScore(ref score, ref games, ref sets, ref tiebreak, ref ganogame, ref juego);
                    Core.ScoreChecking.ScoreDrawer(ref score, ref Score1, ref Score2);
                    if (ganogame)
                    {
                        if (sacador[0] == true)
                        {
                            sacador[0] = false; sacador[1] = true;
                            ganogame = false;
                        }
                        else
                        {
                            sacador[0] = true; sacador[1] = false;
                            ganogame = false;
                        }
                    }
                }
                else
                {
                    Core.ScoreChecking.CheckTiebreakScore(ref score, ref games, ref sets, ref tiebreak, ref ganoset, ref juego);
                    Score1.Text = score[0].ToString();
                    Score2.Text = score[1].ToString();
                    if (ganoset)
                    {
                        if (sacadorComienzo[0] == true)
                        {
                            sacadorComienzo[0] = false; sacadorComienzo[1] = true;
                            sacador[0] = false; sacador[1] = true;
                        }
                        else
                        {
                            sacadorComienzo[0] = true; sacadorComienzo[1] = false;
                            sacador[0] = true; sacador[1] = false;
                        }
                    }
                    else Core.ScoreChecking.SacadorTiebreakSwap(ref sacador, ref score);
                }
                Sets1.Text = sets[0].ToString();
                Sets2.Text = sets[1].ToString();
                Games1.Text = games[0].ToString();
                Games2.Text = games[1].ToString();

                if (sacador[0])
                {
                    segSaqueButton1.Enabled = false;
                    segSaqueButton2.Enabled = false;
                    priSaqueButton1.Enabled = true;
                    priSaqueButton2.Enabled = false;
                    aceButton1.Enabled = true;
                    aceButton2.Enabled = false;
                }
                else
                {
                    segSaqueButton1.Enabled = false;
                    segSaqueButton2.Enabled = false;
                    priSaqueButton1.Enabled = false;
                    priSaqueButton2.Enabled = true;
                    aceButton1.Enabled = false;
                    aceButton2.Enabled = true;
                }
            };
            noForzadoButton1.Click += (sender, e) =>
            {
                noforzados[0]++;
                score[1]++;
                if (sacador[1] == true) ganoconsaque = true;
                Core.ScoreChecking.SumarSaques(ref sacador, ref primeros, ref ganoconsaque, ref primerosGanados, ref segundos, ref segundosGanados, ref priSaqueButton1, ref priSaqueButton2);
                if (tiebreak == false)
                {
                    Core.ScoreChecking.CheckVentaja(ref score);
                    Core.ScoreChecking.CheckScore(ref score, ref games, ref sets, ref tiebreak, ref ganogame, ref juego);
                    Core.ScoreChecking.ScoreDrawer(ref score, ref Score1, ref Score2);
                    if (ganogame)
                    {
                        if (sacador[0] == true)
                        {
                            sacador[0] = false; sacador[1] = true;
                            ganogame = false;
                        }
                        else
                        {
                            sacador[0] = true; sacador[1] = false;
                            ganogame = false;
                        }
                    }
                }
                else
                {
                    Core.ScoreChecking.CheckTiebreakScore(ref score, ref games, ref sets, ref tiebreak, ref ganoset, ref juego);
                    Score1.Text = score[0].ToString();
                    Score2.Text = score[1].ToString();
                    if (ganoset)
                    {
                        if (sacadorComienzo[0] == true)
                        {
                            sacadorComienzo[0] = false; sacadorComienzo[1] = true;
                            sacador[0] = false; sacador[1] = true;
                        }
                        else
                        {
                            sacadorComienzo[0] = true; sacadorComienzo[1] = false;
                            sacador[0] = true; sacador[1] = false;
                        }
                    }
                    else Core.ScoreChecking.SacadorTiebreakSwap(ref sacador, ref score);
                }
                Sets1.Text = sets[0].ToString();
                Sets2.Text = sets[1].ToString();
                Games1.Text = games[0].ToString();
                Games2.Text = games[1].ToString();

                if (sacador[0])
                {
                    segSaqueButton1.Enabled = false;
                    segSaqueButton2.Enabled = false;
                    priSaqueButton1.Enabled = true;
                    priSaqueButton2.Enabled = false;
                    aceButton1.Enabled = true;
                    aceButton2.Enabled = false;
                }
                else
                {
                    segSaqueButton1.Enabled = false;
                    segSaqueButton2.Enabled = false;
                    priSaqueButton1.Enabled = false;
                    priSaqueButton2.Enabled = true;
                    aceButton1.Enabled = false;
                    aceButton2.Enabled = true;
                }
            };
            noForzadoButton2.Click += (sender, e) =>
            {
                noforzados[1]++;
                score[0]++;
                if (sacador[0] == true) ganoconsaque = true;
                Core.ScoreChecking.SumarSaques(ref sacador, ref primeros, ref ganoconsaque, ref primerosGanados, ref segundos, ref segundosGanados, ref priSaqueButton1, ref priSaqueButton2);
                if (tiebreak == false)
                {
                    Core.ScoreChecking.CheckVentaja(ref score);
                    Core.ScoreChecking.CheckScore(ref score, ref games, ref sets, ref tiebreak, ref ganogame, ref juego);
                    if (ganogame)
                    {
                        if (sacador[0] == true)
                        {
                            sacador[0] = false; sacador[1] = true;
                            ganogame = false;
                        }
                        else
                        {
                            sacador[0] = true; sacador[1] = false;
                            ganogame = false;
                        }
                    }
                    Core.ScoreChecking.ScoreDrawer(ref score, ref Score1, ref Score2);
                }
                else
                {
                    Core.ScoreChecking.CheckTiebreakScore(ref score, ref games, ref sets, ref tiebreak, ref ganoset, ref juego);
                    Score1.Text = score[0].ToString();
                    Score2.Text = score[1].ToString();
                    if (ganoset)
                    {
                        if (sacadorComienzo[0] == true)
                        {
                            sacadorComienzo[0] = false; sacadorComienzo[1] = true;
                            sacador[0] = false; sacador[1] = true;
                        }
                        else
                        {
                            sacadorComienzo[0] = true; sacadorComienzo[1] = false;
                            sacador[0] = true; sacador[1] = false;
                        }
                    }
                    else Core.ScoreChecking.SacadorTiebreakSwap(ref sacador, ref score);
                }
                Sets1.Text = sets[0].ToString();
                Sets2.Text = sets[1].ToString();
                Games1.Text = games[0].ToString();
                Games2.Text = games[1].ToString();

                if (sacador[0])
                {
                    segSaqueButton1.Enabled = false;
                    segSaqueButton2.Enabled = false;
                    priSaqueButton1.Enabled = true;
                    priSaqueButton2.Enabled = false;
                    aceButton1.Enabled = true;
                    aceButton2.Enabled = false;
                }
                else
                {
                    segSaqueButton1.Enabled = false;
                    segSaqueButton2.Enabled = false;
                    priSaqueButton1.Enabled = false;
                    priSaqueButton2.Enabled = true;
                    aceButton1.Enabled = false;
                    aceButton2.Enabled = true;
                }
            };
            winnerButton1.Click += (sender, e) =>
            {
                winners[0]++;
                score[0]++;
                if (sacador[0] == true) ganoconsaque = true;
                Core.ScoreChecking.SumarSaques(ref sacador, ref primeros, ref ganoconsaque, ref primerosGanados, ref segundos, ref segundosGanados, ref priSaqueButton1, ref priSaqueButton2);
                if (tiebreak == false)
                {
                    Core.ScoreChecking.CheckVentaja(ref score);
                    Core.ScoreChecking.CheckScore(ref score, ref games, ref sets, ref tiebreak, ref ganogame, ref juego);
                    if (ganogame)
                    {
                        if (sacador[0] == true)
                        {
                            sacador[0] = false; sacador[1] = true;
                            ganogame = false;
                        }
                        else
                        {
                            sacador[0] = true; sacador[1] = false;
                            ganogame = false;
                        }
                    }
                    Core.ScoreChecking.ScoreDrawer(ref score, ref Score1, ref Score2);
                }
                else
                {
                    Core.ScoreChecking.CheckTiebreakScore(ref score, ref games, ref sets, ref tiebreak, ref ganoset, ref juego);
                    Score1.Text = score[0].ToString();
                    Score2.Text = score[1].ToString();
                    if (ganoset)
                    {
                        if (sacadorComienzo[0] == true)
                        {
                            sacadorComienzo[0] = false; sacadorComienzo[1] = true;
                            sacador[0] = false; sacador[1] = true;
                        }
                        else
                        {
                            sacadorComienzo[0] = true; sacadorComienzo[1] = false;
                            sacador[0] = true; sacador[1] = false;
                        }
                    }
                    else Core.ScoreChecking.SacadorTiebreakSwap(ref sacador, ref score);
                }
                Sets1.Text = sets[0].ToString();
                Sets2.Text = sets[1].ToString();
                Games1.Text = games[0].ToString();
                Games2.Text = games[1].ToString();

                if (sacador[0])
                {
                    segSaqueButton1.Enabled = false;
                    segSaqueButton2.Enabled = false;
                    priSaqueButton1.Enabled = true;
                    priSaqueButton2.Enabled = false;
                    aceButton1.Enabled = true;
                    aceButton2.Enabled = false;
                }
                else
                {
                    segSaqueButton1.Enabled = false;
                    segSaqueButton2.Enabled = false;
                    priSaqueButton1.Enabled = false;
                    priSaqueButton2.Enabled = true;
                    aceButton1.Enabled = false;
                    aceButton2.Enabled = true;
                }
            };
            winnerButton2.Click += (sender, e) =>
            {
                winners[1]++;
                score[1]++;
                if (sacador[1] == true) ganoconsaque = true;
                Core.ScoreChecking.SumarSaques(ref sacador, ref primeros, ref ganoconsaque, ref primerosGanados, ref segundos, ref segundosGanados, ref priSaqueButton1, ref priSaqueButton2);
                if (tiebreak == false)
                {
                    Core.ScoreChecking.CheckVentaja(ref score);
                    Core.ScoreChecking.CheckScore(ref score, ref games, ref sets, ref tiebreak, ref ganogame, ref juego);
                    if (ganogame)
                    {
                        if (sacador[0] == true)
                        {
                            sacador[0] = false; sacador[1] = true;
                            ganogame = false;
                        }
                        else
                        {
                            sacador[0] = true; sacador[1] = false;
                            ganogame = false;
                        }
                    }
                    Core.ScoreChecking.ScoreDrawer(ref score, ref Score1, ref Score2);
                }
                else
                {
                    Core.ScoreChecking.CheckTiebreakScore(ref score, ref games, ref sets, ref tiebreak, ref ganoset, ref juego);
                    Score1.Text = score[0].ToString();
                    Score2.Text = score[1].ToString();
                    if (ganoset)
                    {
                        if (sacadorComienzo[0] == true)
                        {
                            sacadorComienzo[0] = false; sacadorComienzo[1] = true;
                            sacador[0] = false; sacador[1] = true;
                        }
                        else
                        {
                            sacadorComienzo[0] = true; sacadorComienzo[1] = false;
                            sacador[0] = true; sacador[1] = false;
                        }
                    }
                    else Core.ScoreChecking.SacadorTiebreakSwap(ref sacador, ref score);
                }
                Sets1.Text = sets[0].ToString();
                Sets2.Text = sets[1].ToString();
                Games1.Text = games[0].ToString();
                Games2.Text = games[1].ToString();

                if (sacador[0])
                {
                    segSaqueButton1.Enabled = false;
                    segSaqueButton2.Enabled = false;
                    priSaqueButton1.Enabled = true;
                    priSaqueButton2.Enabled = false;
                    aceButton1.Enabled = true;
                    aceButton2.Enabled = false;
                }
                else
                {
                    segSaqueButton1.Enabled = false;
                    segSaqueButton2.Enabled = false;
                    priSaqueButton1.Enabled = false;
                    priSaqueButton2.Enabled = true;
                    aceButton1.Enabled = false;
                    aceButton2.Enabled = true;
                }
            };
            aceButton1.Click += (sender, e) =>
            {
                aces[0]++;
                score[0]++;
                ganoconsaque = true;
                Core.ScoreChecking.SumarSaques(ref sacador, ref primeros, ref ganoconsaque, ref primerosGanados, ref segundos, ref segundosGanados, ref priSaqueButton1, ref priSaqueButton2);
                if (tiebreak == false)
                {
                    Core.ScoreChecking.CheckVentaja(ref score);
                    Core.ScoreChecking.CheckScore(ref score, ref games, ref sets, ref tiebreak, ref ganogame, ref juego);
                    if (ganogame)
                    {
                        if (sacador[0] == true)
                        {
                            sacador[0] = false; sacador[1] = true;
                            ganogame = false;
                        }
                        else
                        {
                            sacador[0] = true; sacador[1] = false;
                            ganogame = false;
                        }
                    }
                    Core.ScoreChecking.ScoreDrawer(ref score, ref Score1, ref Score2);
                }
                else
                {
                    Core.ScoreChecking.CheckTiebreakScore(ref score, ref games, ref sets, ref tiebreak, ref ganoset, ref juego);
                    Score1.Text = score[0].ToString();
                    Score2.Text = score[1].ToString();
                    if (ganoset)
                    {
                        if (sacadorComienzo[0] == true)
                        {
                            sacadorComienzo[0] = false; sacadorComienzo[1] = true;
                            sacador[0] = false; sacador[1] = true;
                        }
                        else
                        {
                            sacadorComienzo[0] = true; sacadorComienzo[1] = false;
                            sacador[0] = true; sacador[1] = false;
                        }
                    }
                    else Core.ScoreChecking.SacadorTiebreakSwap(ref sacador, ref score);
                }
                Sets1.Text = sets[0].ToString();
                Sets2.Text = sets[1].ToString();
                Games1.Text = games[0].ToString();
                Games2.Text = games[1].ToString();

                if (sacador[0])
                {
                    segSaqueButton1.Enabled = false;
                    segSaqueButton2.Enabled = false;
                    priSaqueButton1.Enabled = true;
                    priSaqueButton2.Enabled = false;
                    aceButton1.Enabled = true;
                    aceButton2.Enabled = false;
                }
                else
                {
                    segSaqueButton1.Enabled = false;
                    segSaqueButton2.Enabled = false;
                    priSaqueButton1.Enabled = false;
                    priSaqueButton2.Enabled = true;
                    aceButton1.Enabled = false;
                    aceButton2.Enabled = true;
                }
            };
            aceButton2.Click += (sender, e) =>
            {
                aces[1]++;
                score[1]++;
                ganoconsaque = true;
                Core.ScoreChecking.SumarSaques(ref sacador, ref primeros, ref ganoconsaque, ref primerosGanados, ref segundos, ref segundosGanados, ref priSaqueButton1, ref priSaqueButton2);
                if (tiebreak == false)
                {
                    Core.ScoreChecking.CheckVentaja(ref score);
                    Core.ScoreChecking.CheckScore(ref score, ref games, ref sets, ref tiebreak, ref ganogame, ref juego);
                    if (ganogame)
                    {
                        if (sacador[0] == true)
                        {
                            sacador[0] = false; sacador[1] = true;
                            ganogame = false;
                        }
                        else
                        {
                            sacador[0] = true; sacador[1] = false;
                            ganogame = false;
                        }
                    }
                    Core.ScoreChecking.ScoreDrawer(ref score, ref Score1, ref Score2);
                }
                else
                {
                    Core.ScoreChecking.CheckTiebreakScore(ref score, ref games, ref sets, ref tiebreak, ref ganoset, ref juego);
                    Score1.Text = score[0].ToString();
                    Score2.Text = score[1].ToString();
                    if (ganoset)
                    {
                        if (sacadorComienzo[0] == true)
                        {
                            sacadorComienzo[0] = false; sacadorComienzo[1] = true;
                            sacador[0] = false; sacador[1] = true;
                        }
                        else
                        {
                            sacadorComienzo[0] = true; sacadorComienzo[1] = false;
                            sacador[0] = true; sacador[1] = false;
                        }
                    }
                    else Core.ScoreChecking.SacadorTiebreakSwap(ref sacador, ref score);

                }
                Sets1.Text = sets[0].ToString();
                Sets2.Text = sets[1].ToString();
                Games1.Text = games[0].ToString();
                Games2.Text = games[1].ToString();

                if (sacador[0])
                {
                    segSaqueButton1.Enabled = false;
                    segSaqueButton2.Enabled = false;
                    priSaqueButton1.Enabled = true;
                    priSaqueButton2.Enabled = false;
                    aceButton1.Enabled = true;
                    aceButton2.Enabled = false;
                }
                else
                {
                    segSaqueButton1.Enabled = false;
                    segSaqueButton2.Enabled = false;
                    priSaqueButton1.Enabled = false;
                    priSaqueButton2.Enabled = true;
                    aceButton1.Enabled = false;
                    aceButton2.Enabled = true;
                }
            };

            #endregion

            Sets1.TextChanged += (sender, e) =>
            {
                #region Set1
                if ((Sets1.Text == "1") && (cambioSet[0] == false))
                {
                    cambioSet[0] = true;

                    juego.noforzados[0] = noforzados[0];
                    juego.aces[0] = aces[0];
                    juego.doblefaltas[0] = doblesfaltas[0];
                    juego.winners[0] = winners[0];
                    juego.primeros[0] = primeros[0];
                    juego.primerosGanados[0] = primerosGanados[0];
                    juego.segundos[0] = segundos[0];
                    juego.segundosGanados[0] = segundosGanados[0];

                    juego.noforzados[1] = noforzados[1];
                    juego.aces[1] = aces[1];
                    juego.doblefaltas[1] = doblesfaltas[1];
                    juego.winners[1] = winners[1];
                    juego.primeros[1] = primeros[1];
                    juego.primerosGanados[1] = primerosGanados[1];
                    juego.segundos[1] = segundos[1];
                    juego.segundosGanados[1] = segundosGanados[1];

                    if ((juego.sets[0].games[0] == 0) && (juego.sets[0].games[0] == 0))
                    {
                        juego.sets[0].noforzados[0] = noforzados[0];
                        juego.sets[0].aces[0] = aces[0];
                        juego.sets[0].doblefaltas[0] = doblesfaltas[0];
                        juego.sets[0].winners[0] = winners[0];
                        juego.sets[0].primeros[0] = primeros[0];
                        juego.sets[0].primerosGanados[0] = primerosGanados[0];
                        juego.sets[0].segundos[0] = segundos[0];
                        juego.sets[0].segundosGanados[0] = segundosGanados[0];

                        juego.sets[0].noforzados[1] = noforzados[1];
                        juego.sets[0].aces[1] = aces[1];
                        juego.sets[0].doblefaltas[1] = doblesfaltas[1];
                        juego.sets[0].winners[1] = winners[1];
                        juego.sets[0].primeros[1] = primeros[1];
                        juego.sets[0].primerosGanados[1] = primerosGanados[1];
                        juego.sets[0].segundos[1] = segundos[1];
                        juego.sets[0].segundosGanados[1] = segundosGanados[1];
                    }
                    else
                    {
                        juego.sets[1].noforzados[0] = (noforzados[0] - juego.sets[0].noforzados[0]);
                        juego.sets[1].aces[0] = (aces[0] - juego.sets[0].aces[0]);
                        juego.sets[1].doblefaltas[0] = (doblesfaltas[0] - juego.sets[0].doblefaltas[0]);
                        juego.sets[1].winners[0] = (winners[0] - juego.sets[0].winners[0]);
                        juego.sets[1].primeros[0] = (primeros[0] - juego.sets[0].primeros[0]);
                        juego.sets[1].primerosGanados[0] = (primerosGanados[0] - juego.sets[0].primerosGanados[0]);
                        juego.sets[1].segundos[0] = (segundos[0] - juego.sets[0].segundos[0]);
                        juego.sets[1].segundosGanados[0] = (segundosGanados[0] - juego.sets[0].segundosGanados[0]);

                        juego.sets[1].noforzados[1] = (noforzados[1] - juego.sets[0].noforzados[1]);
                        juego.sets[1].aces[1] = (aces[1] - juego.sets[0].aces[1]);
                        juego.sets[1].doblefaltas[1] = (doblesfaltas[1] - juego.sets[0].doblefaltas[1]);
                        juego.sets[1].winners[1] = (winners[1] - juego.sets[0].winners[1]);
                        juego.sets[1].primeros[1] = (primeros[1] - juego.sets[0].primeros[1]);
                        juego.sets[1].primerosGanados[1] = (primerosGanados[1] - juego.sets[0].primerosGanados[1]);
                        juego.sets[1].segundos[1] = (segundos[1] - juego.sets[0].segundos[1]);
                        juego.sets[1].segundosGanados[1] = (segundosGanados[1] - juego.sets[0].segundosGanados[1]);
                    }
                }
                    #endregion

                if (Sets1.Text == "2")
                {
                    juego.noforzados[0] = noforzados[0];
                    juego.aces[0] = aces[0];
                    juego.doblefaltas[0] = doblesfaltas[0];
                    juego.winners[0] = winners[0];
                    juego.primeros[0] = primeros[0];
                    juego.primerosGanados[0] = primerosGanados[0];
                    juego.segundos[0] = segundos[0];
                    juego.segundosGanados[0] = segundosGanados[0];

                    juego.noforzados[1] = noforzados[1];
                    juego.aces[1] = aces[1];
                    juego.doblefaltas[1] = doblesfaltas[1];
                    juego.winners[1] = winners[1];
                    juego.primeros[1] = primeros[1];
                    juego.primerosGanados[1] = primerosGanados[1];
                    juego.segundos[1] = segundos[1];
                    juego.segundosGanados[1] = segundosGanados[1];

                    if ((juego.sets[1].games[0] == 0) && (juego.sets[1].games[1] == 0))
                    {
                        juego.sets[1].noforzados[0] = (noforzados[0] - juego.sets[0].noforzados[0]);
                        juego.sets[1].aces[0] = (aces[0] - juego.sets[0].aces[0]);
                        juego.sets[1].doblefaltas[0] = (doblesfaltas[0] - juego.sets[0].doblefaltas[0]);
                        juego.sets[1].winners[0] = (winners[0] - juego.sets[0].winners[0]);
                        juego.sets[1].primeros[0] = (primeros[0] - juego.sets[0].primeros[0]);
                        juego.sets[1].primerosGanados[0] = (primerosGanados[0] - juego.sets[0].primerosGanados[0]);
                        juego.sets[1].segundos[0] = (segundos[0] - juego.sets[0].segundos[0]);
                        juego.sets[1].segundosGanados[0] = (segundosGanados[0] - juego.sets[0].segundosGanados[0]);

                        juego.sets[1].noforzados[1] = (noforzados[1] - juego.sets[0].noforzados[1]);
                        juego.sets[1].aces[1] = (aces[1] - juego.sets[0].aces[1]);
                        juego.sets[1].doblefaltas[1] = (doblesfaltas[1] - juego.sets[0].doblefaltas[1]);
                        juego.sets[1].winners[1] = (winners[1] - juego.sets[0].winners[1]);
                        juego.sets[1].primeros[1] = (primeros[1] - juego.sets[0].primeros[1]);
                        juego.sets[1].primerosGanados[1] = (primerosGanados[1] - juego.sets[0].primerosGanados[1]);
                        juego.sets[1].segundos[1] = (segundos[1] - juego.sets[0].segundos[1]);
                        juego.sets[1].segundosGanados[1] = (segundosGanados[1] - juego.sets[0].segundosGanados[1]);
                    }

                    else
                    {
                        juego.sets[2].noforzados[0] = (noforzados[0] - juego.sets[0].noforzados[0] - juego.sets[1].noforzados[0]);
                        juego.sets[2].aces[0] = (aces[0] - juego.sets[0].aces[0] - juego.sets[1].aces[0]);
                        juego.sets[2].doblefaltas[0] = (doblesfaltas[0] - juego.sets[0].doblefaltas[0] - juego.sets[1].doblefaltas[0]);
                        juego.sets[2].winners[0] = (winners[0] - juego.sets[0].winners[0] - juego.sets[1].winners[0]);
                        juego.sets[2].primeros[0] = (primeros[0] - juego.sets[0].primeros[0] - juego.sets[1].primeros[0]);
                        juego.sets[2].primerosGanados[0] = (primerosGanados[0] - juego.sets[0].primerosGanados[0] - juego.sets[1].primerosGanados[0]);
                        juego.sets[2].segundos[0] = (segundos[0] - juego.sets[0].segundos[0] - juego.sets[1].segundos[0]);
                        juego.sets[2].segundosGanados[0] = (segundosGanados[0] - juego.sets[0].segundosGanados[0] - juego.sets[1].segundosGanados[0]);

                        juego.sets[2].noforzados[1] = (noforzados[1] - juego.sets[0].noforzados[1] - juego.sets[1].noforzados[1]);
                        juego.sets[2].aces[1] = (aces[1] - juego.sets[0].aces[1] - juego.sets[1].aces[1]);
                        juego.sets[2].doblefaltas[1] = (doblesfaltas[1] - juego.sets[0].doblefaltas[1] - juego.sets[1].doblefaltas[1]);
                        juego.sets[2].winners[1] = (winners[1] - juego.sets[0].winners[1] - juego.sets[1].winners[1]);
                        juego.sets[2].primeros[1] = (primeros[1] - juego.sets[0].primeros[1] - juego.sets[1].primeros[1]);
                        juego.sets[2].primerosGanados[1] = (primerosGanados[1] - juego.sets[0].primerosGanados[1] - juego.sets[1].primerosGanados[1]);
                        juego.sets[2].segundos[1] = (segundos[1] - juego.sets[0].segundos[1] - juego.sets[1].segundos[1]);
                        juego.sets[2].segundosGanados[1] = (segundosGanados[1] - juego.sets[0].segundosGanados[1] - juego.sets[1].segundosGanados[1]);
                    }

                    var serPartido = JsonConvert.SerializeObject(juego);
                    var EstadisticasActivity = new Intent(this, typeof(EstadisticasActivity));
                    EstadisticasActivity.PutExtra("passPartido", serPartido);
                    StartActivity(EstadisticasActivity);

                }
            };

            Sets2.TextChanged += (sender, e) =>
            {
                #region Set1
                if ((Sets2.Text == "1") && (cambioSet[1] == false))
                {
                    cambioSet[1] = true;

                    juego.noforzados[0] = noforzados[0];
                    juego.aces[0] = aces[0];
                    juego.doblefaltas[0] = doblesfaltas[0];
                    juego.winners[0] = winners[0];
                    juego.primeros[0] = primeros[0];
                    juego.primerosGanados[0] = primerosGanados[0];
                    juego.segundos[0] = segundos[0];
                    juego.segundosGanados[0] = segundosGanados[0];

                    juego.noforzados[1] = noforzados[1];
                    juego.aces[1] = aces[1];
                    juego.doblefaltas[1] = doblesfaltas[1];
                    juego.winners[1] = winners[1];
                    juego.primeros[1] = primeros[1];
                    juego.primerosGanados[1] = primerosGanados[1];
                    juego.segundos[1] = segundos[1];
                    juego.segundosGanados[1] = segundosGanados[1];

                    if ((juego.sets[0].games[0] == 0) && (juego.sets[0].games[0] == 0))
                    {
                        juego.sets[0].noforzados[0] = noforzados[0];
                        juego.sets[0].aces[0] = aces[0];
                        juego.sets[0].doblefaltas[0] = doblesfaltas[0];
                        juego.sets[0].winners[0] = winners[0];
                        juego.sets[0].primeros[0] = primeros[0];
                        juego.sets[0].primerosGanados[0] = primerosGanados[0];
                        juego.sets[0].segundos[0] = segundos[0];
                        juego.sets[0].segundosGanados[0] = segundosGanados[0];

                        juego.sets[0].noforzados[1] = noforzados[1];
                        juego.sets[0].aces[1] = aces[1];
                        juego.sets[0].doblefaltas[1] = doblesfaltas[1];
                        juego.sets[0].winners[1] = winners[1];
                        juego.sets[0].primeros[1] = primeros[1];
                        juego.sets[0].primerosGanados[1] = primerosGanados[1];
                        juego.sets[0].segundos[1] = segundos[1];
                        juego.sets[0].segundosGanados[1] = segundosGanados[1];
                    }
                    else
                    {
                        juego.sets[1].noforzados[0] = (noforzados[0] - juego.sets[0].noforzados[0]);
                        juego.sets[1].aces[0] = (aces[0] - juego.sets[0].aces[0]);
                        juego.sets[1].doblefaltas[0] = (doblesfaltas[0] - juego.sets[0].doblefaltas[0]);
                        juego.sets[1].winners[0] = (winners[0] - juego.sets[0].winners[0]);
                        juego.sets[1].primeros[0] = (primeros[0] - juego.sets[0].primeros[0]);
                        juego.sets[1].primerosGanados[0] = (primerosGanados[0] - juego.sets[0].primerosGanados[0]);
                        juego.sets[1].segundos[0] = (segundos[0] - juego.sets[0].segundos[0]);
                        juego.sets[1].segundosGanados[0] = (segundosGanados[0] - juego.sets[0].segundosGanados[0]);

                        juego.sets[1].noforzados[1] = (noforzados[1] - juego.sets[0].noforzados[1]);
                        juego.sets[1].aces[1] = (aces[1] - juego.sets[0].aces[1]);
                        juego.sets[1].doblefaltas[1] = (doblesfaltas[1] - juego.sets[0].doblefaltas[1]);
                        juego.sets[1].winners[1] = (winners[1] - juego.sets[0].winners[1]);
                        juego.sets[1].primeros[1] = (primeros[1] - juego.sets[0].primeros[1]);
                        juego.sets[1].primerosGanados[1] = (primerosGanados[1] - juego.sets[0].primerosGanados[1]);
                        juego.sets[1].segundos[1] = (segundos[1] - juego.sets[0].segundos[1]);
                        juego.sets[1].segundosGanados[1] = (segundosGanados[1] - juego.sets[0].segundosGanados[1]);
                    }
                }
                #endregion

                if (Sets2.Text == "2")
                {
                    juego.noforzados[0] = noforzados[0];
                    juego.aces[0] = aces[0];
                    juego.doblefaltas[0] = doblesfaltas[0];
                    juego.winners[0] = winners[0];
                    juego.primeros[0] = primeros[0];
                    juego.primerosGanados[0] = primerosGanados[0];
                    juego.segundos[0] = segundos[0];
                    juego.segundosGanados[0] = segundosGanados[0];

                    juego.noforzados[1] = noforzados[1];
                    juego.aces[1] = aces[1];
                    juego.doblefaltas[1] = doblesfaltas[1];
                    juego.winners[1] = winners[1];
                    juego.primeros[1] = primeros[1];
                    juego.primerosGanados[1] = primerosGanados[1];
                    juego.segundos[1] = segundos[1];
                    juego.segundosGanados[1] = segundosGanados[1];

                    if ((juego.sets[1].games[0] == 0)&&(juego.sets[1].games[1] == 0))
                    {
                        juego.sets[1].noforzados[0] = (noforzados[0] - juego.sets[0].noforzados[0]);
                        juego.sets[1].aces[0] = (aces[0] - juego.sets[0].aces[0]);
                        juego.sets[1].doblefaltas[0] = (doblesfaltas[0] - juego.sets[0].doblefaltas[0]);
                        juego.sets[1].winners[0] = (winners[0] - juego.sets[0].winners[0]);
                        juego.sets[1].primeros[0] = (primeros[0] - juego.sets[0].primeros[0]);
                        juego.sets[1].primerosGanados[0] = (primerosGanados[0] - juego.sets[0].primerosGanados[0]);
                        juego.sets[1].segundos[0] = (segundos[0] - juego.sets[0].segundos[0]);
                        juego.sets[1].segundosGanados[0] = (segundosGanados[0] - juego.sets[0].segundosGanados[0]);

                        juego.sets[1].noforzados[1] = (noforzados[1] - juego.sets[0].noforzados[1]);
                        juego.sets[1].aces[1] = (aces[1] - juego.sets[0].aces[1]);
                        juego.sets[1].doblefaltas[1] = (doblesfaltas[1] - juego.sets[0].doblefaltas[1]);
                        juego.sets[1].winners[1] = (winners[1] - juego.sets[0].winners[1]);
                        juego.sets[1].primeros[1] = (primeros[1] - juego.sets[0].primeros[1]);
                        juego.sets[1].primerosGanados[1] = (primerosGanados[1] - juego.sets[0].primerosGanados[1]);
                        juego.sets[1].segundos[1] = (segundos[1] - juego.sets[0].segundos[1]);
                        juego.sets[1].segundosGanados[1] = (segundosGanados[1] - juego.sets[0].segundosGanados[1]);
                    }
                    else
                    {
                        juego.sets[2].noforzados[0] = (noforzados[0] - juego.sets[0].noforzados[0] - juego.sets[1].noforzados[0]);
                        juego.sets[2].aces[0] = (aces[0] - juego.sets[0].aces[0] - juego.sets[1].aces[0]);
                        juego.sets[2].doblefaltas[0] = (doblesfaltas[0] - juego.sets[0].doblefaltas[0] - juego.sets[1].doblefaltas[0]);
                        juego.sets[2].winners[0] = (winners[0] - juego.sets[0].winners[0] - juego.sets[1].winners[0]);
                        juego.sets[2].primeros[0] = (primeros[0] - juego.sets[0].primeros[0] - juego.sets[1].primeros[0]);
                        juego.sets[2].primerosGanados[0] = (primerosGanados[0] - juego.sets[0].primerosGanados[0] - juego.sets[1].primerosGanados[0]);
                        juego.sets[2].segundos[0] = (segundos[0] - juego.sets[0].segundos[0] - juego.sets[1].segundos[0]);
                        juego.sets[2].segundosGanados[0] = (segundosGanados[0] - juego.sets[0].segundosGanados[0] - juego.sets[1].segundosGanados[0]);

                        juego.sets[2].noforzados[1] = (noforzados[1] - juego.sets[0].noforzados[1] - juego.sets[1].noforzados[1]);
                        juego.sets[2].aces[1] = (aces[1] - juego.sets[0].aces[1] - juego.sets[1].aces[1]);
                        juego.sets[2].doblefaltas[1] = (doblesfaltas[1] - juego.sets[0].doblefaltas[1] - juego.sets[1].doblefaltas[1]);
                        juego.sets[2].winners[1] = (winners[1] - juego.sets[0].winners[1] - juego.sets[1].winners[1]);
                        juego.sets[2].primeros[1] = (primeros[1] - juego.sets[0].primeros[1] - juego.sets[1].primeros[1]);
                        juego.sets[2].primerosGanados[1] = (primerosGanados[1] - juego.sets[0].primerosGanados[1] - juego.sets[1].primerosGanados[1]);
                        juego.sets[2].segundos[1] = (segundos[1] - juego.sets[0].segundos[1] - juego.sets[1].segundos[1]);
                        juego.sets[2].segundosGanados[1] = (segundosGanados[1] - juego.sets[0].segundosGanados[1] - juego.sets[1].segundosGanados[1]);
                    }

                    var serPartido = JsonConvert.SerializeObject(juego);
                    var EstadisticasActivity = new Intent(this, typeof(EstadisticasActivity));
                    EstadisticasActivity.PutExtra("passPartido", serPartido);
                    StartActivity(EstadisticasActivity);

                }
            };
        }
        
    }

    
}