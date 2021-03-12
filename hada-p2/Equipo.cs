using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Equipo
    {
        public static int minJugadores { get; set; }
        private static int _maxNumeroMovimientos;
        public static int maxNumeroMovimientos
        {
            get
            {
                return _maxNumeroMovimientos;
            }
            set
            {
                if (value > 0)
                {
                    _maxNumeroMovimientos = value;
                }
                else
                {
                    _maxNumeroMovimientos = 1;
                }
            }
        }
        public int movimientos { get; private set; }
        public string nombreEquipo { get; private set; }

        private Jugador[] jugadores;
        private List<Jugador> expulsados = new List<Jugador>();
        private List<Jugador> lesionados = new List<Jugador>();
        private List<Jugador> retirados = new List<Jugador>();
        public Equipo(int nj, string nom)
        {
            this.nombreEquipo = nom;
            movimientos = 0;
            jugadores = new Jugador[nj];

            for (int i = 0; i < nj; i++)
            {
                string n = "jugador_" + i;
                int a, f, p, e;
                a = f = p = 0;
                e = 50;
                jugadores[i] = new Jugador(n, a, f, e, p);
                jugadores[i].amonestacionesMaximoExcedido += cuandoAmonestacionesMaximoExcedido;
                jugadores[i].faltasMaximoExcedido += cuandoFaltasMaximoExcedido;
                jugadores[i].energiaMinimaExcedida += cuandoEnergiaMinimaExcedida;
            }
        }
        public bool moverJugadores()
        {
            bool ok = false;
            int contarJugadores = 0;
            foreach (Jugador j in jugadores)
            {
                if (j.todoOk())
                {
                    j.mover();
                    if (j.todoOk())
                    {
                        contarJugadores++;
                    }
                }
            }
            movimientos++;
            if (contarJugadores >= minJugadores)
            {
                ok = true;
            }
            return ok;
        }
        public void moverJugadoresEnBucle()
        {
            while (this.moverJugadores()) ;
        }
        public int sumarPuntos()
        {
            int suma = 0;
            foreach (Jugador j in jugadores)
            {
                suma += j.puntos;
            }
            return suma;
        }
        public List<Jugador> getJugadoresExcedenLimiteAmonestaciones()
        {
            return expulsados;
        }
        public List<Jugador> getJugadoresExcedenLimiteFaltas()
        {
            return lesionados;
        }
        public List<Jugador> getJugadoresExcedenMinimoEnergia()
        {
            return retirados;
        }
        private void cuandoAmonestacionesMaximoExcedido(Object sender, AmonestacionesMaximoExcedidoArgs args)
        {
            Jugador j = (Hada.Jugador)sender;

            Console.WriteLine("¡¡Número máximo excedido de amonestaciones. Jugador expulsado!!\n" +
                              "Jugador: " + j.nombre +
                              "\n Equipo: " + this.nombreEquipo +
                              "\n Amonestaciones: " + args.amonestaciones);
            expulsados.Add(j);
        }
        private void cuandoFaltasMaximoExcedido(Object sender, FaltasMaximoExcedidoArgs args)
        {
            Jugador j = (Hada.Jugador)sender;

            Console.WriteLine("¡¡Número máximo excedido de faltas. Jugador retirado!!\n" +
                              "Jugador: " + j.nombre +
                              "\n Equipo: " + this.nombreEquipo +
                              "\n Faltas: " + args.faltas);
            lesionados.Add(j);
        }
        private void cuandoEnergiaMinimaExcedida(Object sender, EnergiaMinimaExcedidaArgs args)
        {
            Jugador j = (Hada.Jugador)sender;

            Console.WriteLine("¡¡Energía´mínima excedida. Jugador retirado!!\n" +
                              "Jugador: " + j.nombre +
                              "\n Equipo: " + this.nombreEquipo +
                              "\n Energía " + args.energia + "%");
            retirados.Add(j);
        }
        public override string ToString()
        {
            string output = "[" + this.nombreEquipo + "] Puntos: " +
                             this.sumarPuntos() + "; Expulsados: " +
                             expulsados.Count + "; Lesionados: " +
                             lesionados.Count + "; Retirados: " +
                             retirados.Count + "\n";
            foreach (Jugador j in jugadores)
            {
                output += j.ToString() + "\n";
            }
            return output;
        }
    }
}