using System.Data;
using System.Drawing;
using System;

namespace MapaB
{
    class MapaBack
    {
        int size;
        public int SIZE{
            get{ return size; }
            //set{ size = value; }
        }
        Celda[,] map;
        public Celda[,] MAP{
            get{ return map; }
            set{ map = value; }
        }
        Random random = new Random();
        public MapaBack(int size)
        {
            this.size = size;
            this.map = new Celda[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    map[i, j] = new Celda();
                }
            }
        }
        public virtual bool Esvalid(int x, int y)
        {
            if(x > SIZE - 1|| x < 0 || y > SIZE - 1|| y < 0 || MAP[x, y].caminada == true)
            {
                return false;
            }
            return true;
        }
        public virtual bool Camino(int x, int y, int px, int py)
        {
            if(x > SIZE - 1|| x < 0 || y > SIZE - 1|| y < 0 || MAP[x, y].caminada == true)
            {
                return false;
            }
            else
            {
                MAP[x,y].caminada = true;
                //int[] dx = [-1,1,0,0];
                //int[] dy = [0,0,1,-1];
                // for (int i = 0; i < 4; i++)
                // {
                //     int r = random.Next(i ,4);
                //     int aux = dx[0];
                //     dx[0] = dx[r];
                //     dx[r] = aux;
                //     aux = dy[0];
                //     dy[0] = dy[r];
                //     dy[r] = aux;

                // }
                if(x > px){MAP[px, py].paredR = false;}
                if(x < px){MAP[x, y].paredR = false;}
                if(y > py){MAP[x, y].paredU = false;}
                if(y < py){MAP[px, py].paredU = false;}
                Camino(x + 1, y, x, y);
                //return true;
            }
            return true;
        }
        public virtual void Show()
        {
            string[,] maps = new string[SIZE + SIZE - 1, SIZE + SIZE - 1];
            for (int i = 1; i <= SIZE; i++)
            {
                for (int j = 1; j <= SIZE; j++)
                {
                    if(MAP[i - 1,j - 1].paredR)
                    {
                        maps[i - 1,j] = "█";
                    }
                    else
                    {
                        maps[i - 1,j] = " ";
                    }
                    if(MAP[i - 1,j - 1].paredU)
                    {
                        maps[i, j - 1] = "█";
                    }
                    else
                    {
                        maps[i, j - 1] = " ";
                    }
                }
            }
            for (int i = 0; i < maps.GetLength(0); i++)
            {
                for (int j = 0; j < maps.GetLength(1); j++)
                {
                    Console.Write(maps[i,j] + "");
                }
                System.Console.WriteLine("");
            }
        }
 
        static void Main()
        {
            MapaBack mapaBack = new MapaBack(3);
            mapaBack.Camino(0,0,0,0);
            mapaBack.Show();
        }
    }
    public class Celda
    {
        public bool paredR;
        public bool paredU;
        public bool caminada;
        public Celda()
        {
            paredR = true;
            paredU = true;
            caminada = false;
        }
    }
}