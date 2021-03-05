using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class AmonestacionesMaximoExcedidoArgs : EventArgs
    {
        public AmonestacionesMaximoExcedidoArgs(int a)
        {
            this.amonestaciones = a;
        }
        public int amonestaciones { get; set; }
    }
    public class FaltasMaximoExcedidoArgs : EventArgs
    {
        public FaltasMaximoExcedidoArgs(int a)
        {
            this.faltas = a;
        }
        public int faltas { get; set; }
    }
    public class EnergiaMinimaExcedidaArgs : EventArgs
    {
        public EnergiaMinimaExcedidaArgs(int a)
        {
            this.energia = a;
        }
        public int energia { get; set; }
    }
    class Jugador
    {
        public static int maxAmonestaciones { get; set;}
        public static int maxFaltas { get; set; }
        public static int minEnergia { get; set; }
        public static Random rand
        {
           private get { return new Random(); }
           set { ; }
        }
        public string nombre { get; private set; }
        public int puntos { get; set; }
        private int _amonestaciones;
        public event EventHandler<AmonestacionesMaximoExcedidoArgs> amonestacionesMaximoExcedido;
        private int amonestaciones
        {
            get { return _amonestaciones; }
            set
            {
                if (value < 0)
                {
                    _amonestaciones = 0;
                }
                if ( (_amonestaciones > maxAmonestaciones) && (amonestacionesMaximoExcedido != null) )
                {
                    amonestacionesMaximoExcedido(this, new AmonestacionesMaximoExcedidoArgs(_amonestaciones));
                }
                else
                {
                    _amonestaciones = value;
                }
            }
                        
        }
        private int _faltas;
        public event EventHandler<FaltasMaximoExcedidoArgs> faltasMaximoExcedido;
        private int faltas
        {
            get { return _faltas; }
            set
            {
                if ( (_faltas > maxFaltas) && (faltasMaximoExcedido != null) )
                {
                    faltasMaximoExcedido(this, new FaltasMaximoExcedidoArgs(_faltas));
                }
                else
                {
                    _faltas = value;
                }
            }
        }
        private int _energia;
        public event EventHandler<EnergiaMinimaExcedidaArgs> energiaMinimaExcedida;
        private int energia
        {
            get { return _energia; }
            set
            {
                if ( value < 0 )
                {
                    _energia = 0;
                }
                if ( value > 100 )
                {
                    _energia = 100;
                }
                if ( (_energia < minEnergia) && (energiaMinimaExcedida != null) )
                {
                    energiaMinimaExcedida(this, new EnergiaMinimaExcedidaArgs(_energia));
                }
                else
                {
                    _energia = value;
                }
            }
         }
        public Jugador ( string nombre, int amonestaciones, int faltas, int energia, int puntos)
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
            _amonestaciones += incremento;
        }
        public void incFaltas()
        {
            int incremento = rand.Next(0, 4);
            _faltas += incremento;
        }
        public void decEnergia()
        {
            int decremento = rand.Next(0, 8);
            _energia -= decremento;
        }
        public void incPuntos()
        {
            int incremento = rand.Next(0, 4);
            this.puntos += incremento;
        }
        public bool todoOk()
        {
            if( (this.amonestaciones <= maxAmonestaciones) && (this.faltas <= maxFaltas) && (this.energia >= minEnergia))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void mover()
        {
            if(this.todoOk())
            {
                this.incAmonestaciones();
                this.incFaltas();
                this.decEnergia();
            }
        }
        public override string ToString()
        {
            return "Puntos: " + this.puntos + "; Amonestaciones: " + this.amonestaciones + "; Faltas: " + this.faltas + "; Energía: " + this.energia + "%; Ok: " + this.todoOk();
        }
    }
}
