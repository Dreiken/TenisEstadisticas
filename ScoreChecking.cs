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

namespace Core
{
    class ScoreChecking
    {
        public static void SacadorTiebreakSwap(ref bool[] sacador, ref int[] score)
        {
            if (((score[0]+score[1])%2)!= 0)
            {
                if (sacador[0] == true)
                {
                    sacador[0] = false; sacador[1] = true;
                }
                else
                {
                    sacador[0] = true; sacador[1] = false;
                }
            }
        }

        public static void ScoreDrawer(ref int[] score, ref TextView Score1, ref TextView Score2)
        {
            if (score[0] == 0) Score1.Text = "0";
            if (score[0] == 1) Score1.Text = "15";
            if (score[0] == 2) Score1.Text = "30";
            if (score[0] == 3) Score1.Text = "40";
            if (score[0] == 4) Score1.Text = "VEN";
            if (score[1] == 0) Score2.Text = "0";
            if (score[1] == 1) Score2.Text = "15";
            if (score[1] == 2) Score2.Text = "30";
            if (score[1] == 3) Score2.Text = "40";
            if (score[1] == 4) Score2.Text = "VEN";
        }

        public static void CheckVentaja(ref int[] score)
        {
            if ((score[0] == 4)&&(score[1] == 4))
            {
                score[0] = 3; score[1] = 3;
            }
        }

        public static void CheckScore(ref int[] score, ref int[] games, ref int[] sets, ref bool tiebreak, ref bool ganogame, ref Partido juego)
        {
            if ((score[0] == 4) && ((score[1] != 3) && (score[1] != 4)))
            {
                score[0] = 0; score[1] = 0;
                games[0]++;
                ganogame = true;
                CheckGames(ref games, ref sets, ref tiebreak, ref juego);
            }
            if ((score[1] == 4) && ((score[0] != 3) && (score[0] != 4)))
            {
                score[0] = 0; score[1] = 0;
                games[1]++;
                ganogame = true;
                CheckGames(ref games, ref sets, ref tiebreak, ref juego);
            }
            if (score[0] == 5)
            {
                score[0] = 0; score[1] = 0;
                games[0]++;
                ganogame = true;
                CheckGames(ref games, ref sets, ref tiebreak, ref juego);
            }
            if (score[1] == 5)
            {
                score[0] = 0; score[1] = 0;
                games[1]++;
                ganogame = true;
                CheckGames(ref games, ref sets, ref tiebreak, ref juego);
            }
        }

        public static void CheckGames(ref int[] games, ref int[] sets, ref bool tiebreak, ref Partido juego)
        {
            if ((games[0] == 6)&&(games[1] != 5)&&(games[1] != 6))
            {
                Estadisticas.GuardarEstadisticas(ref juego, ref games);
                sets[0]++;
                games[0] = 0; games[1] = 0;
                
            }
            if ((games[1] == 6) && (games[0] != 5) && (games[0] != 6))
            {
                Estadisticas.GuardarEstadisticas(ref juego, ref games);
                sets[1]++;
                games[0] = 0; games[1] = 0;
                
            }
            if ((games[1] == 6) && (games[0] == 6)) tiebreak = true;
        }

        public static void CheckTiebreakScore(ref int[] score, ref int[] games, ref int[] sets, ref bool tiebreak, ref bool ganoset, ref Partido juego)
        {
            if ( (score[0]>(score[1]+1)) && (score[0]>6) )
            {
                Estadisticas.GuardarEstadisticas(ref juego, ref games);
                sets[0]++;
                score[0] = 0; score[1] = 0;
                games[0] = 0; games[1] = 0;
                ganoset = true;
                tiebreak = false;
            }

            if ((score[1] > (score[0] + 1)) && (score[1] > 6))
            {
                Estadisticas.GuardarEstadisticas(ref juego, ref games);
                sets[1]++;
                score[0] = 0; score[1] = 0;
                games[0] = 0; games[1] = 0;
                ganoset = true;
                tiebreak = false;
            }
        }

        public static void SumarSaques(ref bool[] sacador, ref int[] primeros, ref bool ganoconsaque, ref int[] primerosGanados, ref int[] segundos, 
            ref int[] segundosGanados, ref Button primerSaqueButton1, ref Button primerSaqueButton2)
        {
            if (ganoconsaque)
            {
                if (sacador[0] == true)
                {
                    if (primerSaqueButton1.Enabled == true)
                    {
                        primeros[0]++;
                        primerosGanados[0]++;
                    }
                    else
                    {
                        segundos[0]++;
                        segundosGanados[0]++;
                    }
                }
                else
                {
                    if (primerSaqueButton2.Enabled == true)
                    {
                        primeros[1]++;
                        primerosGanados[1]++;
                    }
                    else
                    {
                        segundos[1]++;
                        segundosGanados[1]++;
                    }
                }
            }
            else
            {
                if (sacador[0] == true)
                {
                    if (primerSaqueButton1.Enabled == true)
                    {
                        primeros[0]++;
                    }
                    else
                    {
                        segundos[0]++;
                    }
                }
                else
                {
                    if (primerSaqueButton2.Enabled == true)
                    {
                        primeros[1]++;
                    }
                    else
                    {
                        segundos[1]++;
                    }
                }
            }
            ganoconsaque = false;
        }
    }
}