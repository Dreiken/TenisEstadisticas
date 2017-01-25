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
using Java.Interop;

namespace Core
{
    public class Partido
    {
        public string[] jugador = new string[2] {"0", "0" };
        public Set[] sets = new Set[3];
        public int[] noforzados = new int[2] { 0, 0 };
        public int[] aces = new int[2] { 0, 0 };
        public int[] winners = new int[2] { 0, 0 };
        public int[] doblefaltas = new int[2] { 0, 0 };
        public int[] primeros = new int[2] { 0, 0 };
        public int[] primerosGanados = new int[2] { 0, 0 };
        public int[] segundos = new int[2] { 0, 0 };
        public int[] segundosGanados = new int[2] { 0, 0 };
        public bool[] sacador = new bool[] { false , false };
        public int mostrarEstadisticas = 0;

        public Partido(string[] Jugador, Set[] Sets, int[] NoForzados, int[] Aces, int[] Winners, int[] DobleFaltas, int[] Primeros, int[] PrimerosGanados
            , int[] Segundos, int[] SegundosGanados, bool[] Sacador, int MostrarEstadisticas)
        {
            jugador = Jugador; sets = Sets; noforzados = NoForzados; aces = Aces; winners = Winners; doblefaltas = DobleFaltas;
            primeros = Primeros; primerosGanados = PrimerosGanados; segundos = Segundos; segundosGanados = SegundosGanados; sacador = Sacador; mostrarEstadisticas = MostrarEstadisticas ;
        }
    }



    public class Set
    {
        public string[] jugador = new string[2] { "0", "0" };
        public bool[] jugado = new bool[1] { false };
        public int[] games = new int[2] { 0, 0 };
        public int[] noforzados = new int[2] { 0, 0 };
        public int[] aces = new int[2] { 0, 0 };
        public int[] winners = new int[2] { 0, 0 };
        public int[] doblefaltas = new int[2] { 0, 0 };
        public int[] primeros = new int[2] { 0, 0 };
        public int[] primerosGanados = new int[2] { 0, 0 };
        public int[] segundos = new int[2] { 0, 0 };
        public int[] segundosGanados = new int[2] { 0, 0 };

        public Set(string[] Jugador, bool[] Jugado, int[] Games, int[] NoForzados, int[] Aces, int[] Winners, int[] DobleFaltas, int[] Primeros, int[] PrimerosGanados
            , int[] Segundos, int[] SegundosGanados)
        {
            jugador = Jugador; jugado = Jugado; games = Games; noforzados = NoForzados; aces = Aces; winners = Winners; doblefaltas = DobleFaltas;
            primeros = Primeros; primerosGanados = PrimerosGanados; segundos = Segundos; segundosGanados = SegundosGanados;
        }
        
    }

    
    class Estadisticas
    {
        public static void GuardarEstadisticas(ref Partido juego, ref int[] games)
        {
            for (int a = 0; a<3; a++)
            {
                if (juego.sets[a].jugado[0] == false)
                {
                    juego.sets[a].games[0] = games[0];
                    juego.sets[a].games[1] = games[1];

                    juego.sets[a].jugado[0] = true;
                    break;
                }
            }
        }

        public static string SacarPorcentajeGanados(int Saques, int SaquesGanados)
        {
            double fSaques = Saques;
            double fSaquesGanados = SaquesGanados;
            double pog = (fSaquesGanados/fSaques)*100;
            return (pog.ToString("0.00") + "%");
        }

    }
}