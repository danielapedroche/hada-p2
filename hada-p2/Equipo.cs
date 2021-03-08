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
        public static int maxNumeroMovimientos { get; set; }
        public int movimientos { get; private set; }
        public string nombreEquipo { get; private set; }
        private List<Jugador> jugadores = new List<Jugador>();
        private List<Jugador> expulsados = new List<Jugador>();
        private List<Jugador> lesionados = new List<Jugador>();
        private List<Jugador> retirados = new List<Jugador>(); 
        public Equipo (int nj, string nom)
        {
            for (int i = 0; i < nj; i++)
            {
                string n = "jugador_" + i;
                int a, f, p;
                a = f = p = 0;
                int e = 50;
                Jugador j = new Jugador(n, a, f, p, e);
                j.amonestacionesMaximoExcedido += cuandoAmonestacionesMaximoExcedido;
                j.faltasMaximoExcedido += cuandoFaltasMaximoExcedido;
                j.energiaMinimaExcedida += cuandoEnergiaMinimaExcedida;
                jugadores.Add(j);
            }
        }
        public bool moverJugadores()
        {
            bool ok = false;
            foreach(Jugador j in jugadores)
            {
                if(j.todoOk())
                {
                    j.mover();
                    ok = true;
                }
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
            foreach ( Jugador j in jugadores)
            {
                suma += j.puntos;
            }
            return suma;
        }
        public List<Jugador> getJugadoresExcedenLimiteAmonestaciones ()
        {
            return new List<Jugador>(expulsados);
        }
       private void cuandoAmonestacionesMaximoExcedido(object sender, AmonestacionesMaximoExcedidoArgs args)
       {
            Jugador j = (Hada.Jugador)sender;
            if(!expulsados.Contains(j))
            {
                lesionados.Add(j);
            }
            Console.WriteLine("¡¡Número máximo excedido de amonestaciones. Jugador expulsado!!\n" +
                              "Jugador: " + j.nombre +
                              "\n Equipo: " + this.nombreEquipo +
                              "\n Amonestaciones: " + args.amonestaciones);
       }
       public List<Jugador> getJugadoresExcedenLimiteFaltas ()
       {
            return new List<Jugador>(lesionados);
       }
       private void cuandoFaltasMaximoExcedido(object sender, FaltasMaximoExcedidoArgs args)
       {
           Jugador j = (Hada.Jugador)sender;
           if(!lesionados.Contains(j))
            {
                lesionados.Add(j);
            }
           Console.WriteLine("¡¡Número máximo excedido de faltas. Jugador retirado!!\n" +
                             "Jugador: " + j.nombre +
                             "\n Equipo: " + this.nombreEquipo +
                             "\n Faltas: " + args.faltas);
       }
       public List<Jugador> getJugadoresExcedenMinimoEnergia()
       {
            return new List<Jugador>(retirados);
       }
       private void cuandoEnergiaMinimaExcedida(object sender, EnergiaMinimaExcedidaArgs args)
       {
           Jugador j = (Hada.Jugador)sender;
           if ( !retirados.Contains(j))
           {
                retirados.Add(j);
           }
           Console.WriteLine("¡¡Energía´mínima excedida. Jugador retirado!!\n" +
                             "Jugador: " + j.nombre +
                             "\n Equipo: " + this.nombreEquipo +
                             "\n Energía " + args.energia + "%");

       }
        public override string ToString()
        { 
           string output = "[" + this.nombreEquipo + "] Puntos: " +
                            this.sumarPuntos() + "; Expulsados: " + 
                            expulsados.Count + "; Lesionados: " + 
                            lesionados.Count + "; Retirados: " +
                            retirados.Count + "\n";
            return output;
        }
    }
}
