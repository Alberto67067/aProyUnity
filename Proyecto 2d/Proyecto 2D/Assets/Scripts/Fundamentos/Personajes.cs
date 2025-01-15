using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using Mapa;
namespace Heroes
{
    /// <summary>
    /// 
    /// </summary>
    public class Heroe// De aqui en adelante en donde sea que este escrito "Personaje" me refiero a Heroe, es que el nombre este lo cambie despues de haberlo hecho todo
    //Perdon, a quien este leyendo mi codigo.
    {
        public string nombre{get;} 
        public (int, int) pos;
        public enum Movimiento {Norte, Este, Sur, Oeste};
        public Queue<huella> rastro {get; set;}
        int tamanRastro;
        public int TAMANRASTRO{
            get { return tamanRastro;}
            set { tamanRastro = value;}
        }
        public int Bando;// 0 sera el bando Hunter y 1 sera el bando Explorer
        public Tipo tipo{get;}
        public int velocidad{get; set;}
        public int vision{get; set;}//Me dara un radio de la vision que tiene el personaje, esto de seguro sera de las partes mas dificiles de programar
        public int energia{get; set;}
        public int energiaMax{get; set;}
        public int vidaMax{get; set;}
        public int vida{get; set;}
        public token[] tokens{get;}//Me dara los tokens que cada heroe podra invocar
        //public int invocaciones{get;}// Me dara la cantidad de invocaiones de tokens cada X turnos que podra hacer el Heroe
        /// <summary>
        /// Esto crea un objeto que rellena todos los campos.
        /// </summary>
        /// <param name="nombre">Bueno como indica esto, sera el nombre del Personaje</param>
        /// <param name="map">El mapa para el que esta ficha sera colocado</param>
        /// <param name="tamañoderastro">Esto me dara el tamano del rastro definido previamente pra cada jugador</param>
        /// <param name="Bando">El Bando sera {0: para Hunters, 1: Para Explorer}</param>
        /// <param name="tipo">Los tipos los definire despues, y estos me seran utlies dependiendo del personaje</param>
        /// <param name="velocidad">La cantidad k de casillas que los jugadores podran moverse por turno</param>
        /// <param name="vision">El radio de casillas que podra ver el jugador en si</param>
        /// <param name="energiaMax">La energiaMax que tendra el jugador</param>
        /// <param name="vidaMax">La vida Max que tendra el jugador</param>
        /// <param name="tokens">Los tokens que podra invocar el Heroe</param>
        public Heroe(string nombre, mapa map, int tamanRastro, int Bando, Tipo tipo, int velocidad, int vision, int energiaMax, int vidaMax, token[] tokens)
        {
            this.nombre = nombre;
            this.Bando = Bando;
            pos = Posicionador(map, Bando);//Tendra que ser igual a la posicion inicial
            rastro = new Queue<huella>();
            this.tamanRastro = tamanRastro;
            this.tipo = tipo;
            this.velocidad = velocidad;
            this.vision = vision;
            this.energia = energiaMax;
            this.energiaMax = energiaMax;
            this.vidaMax = vidaMax;
            this.vida = vidaMax;
            this.tokens = tokens;
        }
        public virtual (int, int) Posicionador(mapa map, int band)
        {
            (int, int) posi = (0, 0);
            Random random = new Random();
            if(band == 0)
            {
                posi = map.PosicionadorOut();
                (int, int) temp = (posi.Item1 - random.Next(0, map.SIZE/2), posi.Item2 - random.Next(0, map.SIZE/2));
                if(map.MAP[temp.Item1, temp.Item2] == 0)
                {
                    posi = (temp.Item1, temp.Item2);
                }
                else
                {
                    Posicionador(map, band);
                }
            }
            if(band == 1)
            {
                posi = map.ENTRDS[random.Next(0, map.ENTRDS.Count)];
            }
            return posi;
        }
        static void Main()
        {}
    }
        public class Tipo
        {
            //Enum que me dara su elemento
            string nombre;
            public List<Poder> poderes;
            int elemento;
            int afinidad;
            int debilidad;
            int BiomaAfin;//Esto lo definire en mapa mas tarde, seran los biomas que ubicare en el mapa.
            public Tipo(string nombre, int elemento, int afinidad, int debilidad, int BiomaAfin, params Poder[] poderes)
            {
                this.nombre = nombre;
                this.elemento = elemento;
                this.afinidad = afinidad;
                this.debilidad = debilidad;
                this.BiomaAfin = BiomaAfin;
                this.poderes = poderes.ToList();
            }
            /*      El poder de cada personaje sera unico, ya me ocupare en el Main del juego de crearlos
                hsta ahora esta sera la plantilla que usare:
                -nombre, me parece que no necesito explicarlo
                -poder, ya con los poderes creados, y los personajes me ocupare de asegnarlos, los poderes
                por supuesto responderan a un elemento, puede que me de por crear un personaje con mas de 
                un poder.
                -elemento, asignare a cada Heroe un elemnto, esto tendra relacion conceptual con su afinidad 
                al bioma, su afinidad, y su debilidad.
                    los elementos hasta ahora 5/12/24
                    -0: Fuego
                    -1: Agua
                    -2: Hielo
                    -3: Viento
                    -4: Tierra
                    -5: Acero
                    -6: Luz
                    -7: Oscuridad
                afinidad, con lo explicado anteriormente creo que evidente lo que esto significara, ya pondre
                una imagen, que ademas hare en Paint seguramente explicando la relacion, pero sera parecida a 
                Pokemon, sujeto a algunas libertades que me tomare como autor.
                debilidad, || Iden ||
                BiomaAfin, mas de lo mismo.
            */
            public class Poder
            {
                string nombre;
                public int recarga;
                public int damage;
                public int healt;
                public bool activo;
                public int tactivo;
                public int duracion;
                public int energiaNec;
                public int rango;
                public Poder(string nombre,int damage, int healt, int duracion, int rango,int recarga, int energiaNec)
                {
                    this.nombre = nombre;
                    this.recarga = recarga;
                    this.energiaNec = energiaNec;
                    this.damage = damage;
                    this.healt = healt;
                    this.duracion = duracion;
                    this.rango = rango;
                    tactivo = 0;
                    activo = false;
                }
            }
        }
        public class token
        {
            string nombre;
            public string NOMBRE{
                get{ return nombre; }
            }
            (int, int) pos;
            public (int, int) POS{
                get { return pos; }
                set { pos = value;}
            }
            int damage1;
            public int Damage1{
                get { return damage1; }
                set { damage1 = value;}
            }
            int damage;
            public int Damage{
                get { return damage; }
                set { damage = value;}
            }
            int vidaMax;
            public int VIDAMAX{
                get {return vidaMax;}
            }
            int vida;// Mas tarde tendre que hacer que vida y damage sean mutables desde fuera de este Namespace
            public int VIDA{
                get {return vida;}
                set {vida = value;}
            }
            int velocidad;
            public int VELOCIDAD{
                get {return velocidad;}
                set { velocidad = value;}
            }
            int duracion;//La cantidad de turnos que podra estar el token invocado a menos que lo maten antes
            public int DURACION{
                get {return duracion;}
                set {duracion = value;}
            }
            bool summon;
            public bool SUMMON{
                get {return summon;}
                set {summon = value;}
            }
            int tsummon;
            public int TSUMON{
                get {return tsummon;}
                set {tsummon = value;}
            }
            public token(string nombre, int damage, int vidaMax, int vida, int velocidad, int duracion)
            {
                this.nombre = nombre;
                this.damage1 = damage;
                this.damage = damage;
                this.vidaMax = vidaMax;
                this.vida = vida;
                this.velocidad = velocidad;
                this.duracion = duracion;
                summon = false;
                tsummon = 0;
            }
            public void Reinicio()
            {
                Damage = Damage1;
                VIDA = VIDAMAX;
                TSUMON = 0;
                SUMMON = false;
            }
        }
        public class huella
        {
            public (int, int) pos;//Esto me dara la posicion de la huella
            public int direccion;//Esto la direccion del movimiento, no se si sera mas optimo esto o ver en donde esta el proximo mov
            public enum Direccion{Norte, Este, Sur, Oeste};
            public bool activa;
            public huella((int, int) pos, int direccion, bool activa)
            {
                this.pos = pos;
                this.direccion = direccion;
                this.activa = activa;
                //Tengo que usar un tipo Enum aqui, ls direcciones por ahora seran
                // 0 = Norte
                // 1 = Este
                // 2 = Sur
                // 3 = Oeste
            }
        }
}
