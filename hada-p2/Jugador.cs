using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class AmonestacionesMaximoExcedidoArgs : EventArgs
    {
        public int amonestaciones { get; set; }
        public AmonestacionesMaximoExcedidoArgs(int a)
        {
            this.amonestaciones = a;
        }
    }
    public class FaltasMaximoExcedidoArgs : EventArgs
    {
        public int faltas { get; set; }
        public FaltasMaximoExcedidoArgs(int f)
        {
            this.faltas = f;
        }
    }
    public class EnergiaMinimaExcedidaArgs : EventArgs
    {
        public int energia { get; set; }
        public EnergiaMinimaExcedidaArgs(int e)
        {
            this.energia = e;
        }
    }

    class Jugador
    {
        public event EventHandler<AmonestacionesMaximoExcedidoArgs> amonestacionesMaximoExcedido;
        public event EventHandler<FaltasMaximoExcedidoArgs> faltasMaximoExcedido;
        public event EventHandler<EnergiaMinimaExcedidaArgs> energiaMinimaExcedida;
        public static int maxAmonestaciones { get; set; }
        public static int maxFaltas { get; set; }
        public static int minEnergia { get; set; }
        public static Random rand { private get; set; }
        private string _nombre;
        public string nombre
        {
            get
            {
                return _nombre;
            }
            private set
            {
                _nombre = value;
            }
        }
        public int puntos { get; set; }
        private int _amonestaciones;
        private int amonestaciones
        {
            get { return _amonestaciones; }
            set
            {
                if (value > maxAmonestaciones)
                {
                    EventHandler<AmonestacionesMaximoExcedidoArgs> a = amonestacionesMaximoExcedido;
                    if (amonestacionesMaximoExcedido != null && value > maxAmonestaciones)
                    {
                        a(this, new AmonestacionesMaximoExcedidoArgs(value));
                    }
                }
                else if (value < 0)
                {
                    _amonestaciones = 0;
                }
                else
                {
                    _amonestaciones = value;
                }
            }
        }

        private int _faltas;
        private int faltas
        {
            get { return _faltas; }
            set
            {
                if (value > maxFaltas)
                {
                    EventHandler<FaltasMaximoExcedidoArgs> f = faltasMaximoExcedido;
                    if (faltasMaximoExcedido != null && value > maxFaltas)
                    {
                        f(this, new FaltasMaximoExcedidoArgs(value));
                    }
                }
                else
                {
                    _faltas = value;
                }
            }
        }
        private int _energia;
        private int energia
        {
            get { return _energia; }
            set
            {
                if (value < minEnergia)
                {
                    EventHandler<EnergiaMinimaExcedidaArgs> e = energiaMinimaExcedida;
                    if (energiaMinimaExcedida != null && value > minEnergia)
                    {
                        e(this, new EnergiaMinimaExcedidaArgs(value));
                    }
                }
                if (_energia < 0)
                {
                    _energia = 0;
                }
                else if (_energia > 100)
                {
                    _energia = 100;
                }
                else
                {
                    _energia = value;
                }
            }
        }
        public Jugador(string nombre, int amonestaciones, int faltas, int energia, int puntos)
        {
            this.nombre = nombre;
            this.amonestaciones = amonestaciones;
            this.faltas = faltas;
            this.energia = energia;
            this.puntos = puntos;
        }
        public void incAmonestaciones()
        {
            int incremento = rand.Next(0, 3);
            amonestaciones += incremento;
        }
        public void incFaltas()
        {
            int incremento = rand.Next(0, 4);
            faltas += incremento;
        }
        public void decEnergia()
        {
            int decremento = rand.Next(1, 8);
            energia -= decremento;
        }
        public void incPuntos()
        {
            int incremento = rand.Next(0, 4);
            puntos += incremento;
        }
        public bool todoOk()
        {
            bool ok = false;

            if ((amonestaciones <= maxAmonestaciones) && (faltas <= maxAmonestaciones) && (energia >= minEnergia))
            {
                ok = true;
            }
            return ok;
        }
        public void mover()
        {
            if (this.todoOk())
            {
                this.incAmonestaciones();
                this.incFaltas();
                this.decEnergia();
                this.incPuntos();
            }
        }
        public override string ToString()
        {
            string output = "[" + nombre + "]" + " Puntos: " + puntos +
                             "; Amonestaciones: " + amonestaciones +
                             " ;Faltas: " + faltas +
                             "; Energia: " + energia + "%; "
                             + "Ok: " + todoOk();
            return output;
        }

    }

}