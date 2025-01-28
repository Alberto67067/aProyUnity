using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using Mapa;
using Partida;
using Heroes;
using System.Collections;

public class GameGraphic : MonoBehaviour
{
    //[SerializeField]public mapa MAP;
    public Game game;
    [SerializeField]public List<GameObject> _Heroes;
    [SerializeField]public List<GameObject> _Villanos;
    [SerializeField]public Transform[,] tilesmap;
    [SerializeField]public GameObject Techo;
    [SerializeField]public GameObject Techo2;
    [SerializeField]public GameObject Techo3;
    [SerializeField]public GameObject Pared1;
    [SerializeField]public GameObject Pared2;
    [SerializeField]public GameObject Pared3;
    [SerializeField]public GameObject Pared4;
    [SerializeField]public GameObject Camino;
    bool Vict = false;
    int turno = 0;
    bool ismoving = false;
    bool cambioturno = false;
    bool HoV = false;
    int velPA = 0;
    Heroe heroe;
    GameObject heroegraph;
    //int contador = 0;
    public void Start()
    {
        Construir();
        Generar(game.MAP);
        for (int i = 0; i < _Heroes.Count; i++)
        {
            _Heroes[i].transform.position = new Vector2(game.HeroesGame[i].pos0.Item1 + 0.5f, game.HeroesGame[i].pos0.Item2 + 0.5f);
            _Heroes[i].AddComponent<Jugable>();
            _Villanos[i].transform.position = new Vector2(game.VillansGame[i].pos0.Item1 + 0.5f, game.VillansGame[i].pos0.Item2 + 0.5f);
            _Villanos[i].AddComponent<Jugable>();
        }
        
    }
    public void Update()
    {
        if(!HoV)
        {
            heroe = game.HeroesGame[turno];
            heroegraph = _Heroes[turno];
        }
        else if(HoV)
        {
            heroe = game.VillansGame[turno];
            heroegraph = _Villanos[turno];
        }
        if(!Vict)
        {

            if (!ismoving && Input.anyKeyDown && velPA != heroe.velocidad)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    if (heroe.map.PosicionValida(heroe.map.MAP, heroe.pos.Item1, heroe.pos.Item2 - 1, false))
                    {
                        ismoving = true;
                        heroe.pos = (heroe.pos.Item1, heroe.pos.Item2 - 1);
                        StartCoroutine(heroegraph.GetComponent<Jugable>().MoverDestino(heroegraph.transform.position + Vector3.up));
                        ismoving = false;
                        // if (velPA == heroe.velocidad)
                        //     cambioturno = true;
                        // //contador++;
                        // else
                        //     velPA++;
                    }
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    if (heroe.map.PosicionValida(heroe.map.MAP, heroe.pos.Item1, heroe.pos.Item2 + 1, false))
                    {
                        ismoving = true;
                        heroe.pos = (heroe.pos.Item1, heroe.pos.Item2 + 1);
                        StartCoroutine(heroegraph.GetComponent<Jugable>().MoverDestino(heroegraph.transform.position + Vector3.down));
                        ismoving = false;
                        // if (velPA == heroe.velocidad)
                        //     cambioturno = true;
                        // else
                        //     velPA++;
                        //contador++;
                    }
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    if (heroe.map.PosicionValida(heroe.map.MAP, heroe.pos.Item1 - 1, heroe.pos.Item2, false))
                    {
                        ismoving = true;
                        heroe.pos = (heroe.pos.Item1 - 1, heroe.pos.Item2);
                        StartCoroutine(heroegraph.GetComponent<Jugable>().MoverDestino(heroegraph.transform.position + Vector3.left));
                        ismoving = false;
                        // if (velPA == heroe.velocidad)
                        //     cambioturno = true;
                        // else
                        //     velPA++;
                        //contador++;
                    }
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    if (heroe.map.PosicionValida(heroe.map.MAP, heroe.pos.Item1 + 1, heroe.pos.Item2, false))
                    {
                        ismoving = true;
                        heroe.pos = (heroe.pos.Item1 + 1, heroe.pos.Item2);
                        StartCoroutine(heroegraph.GetComponent<Jugable>().MoverDestino(heroegraph.transform.position + Vector3.right));
                        ismoving = false;
                        // if (velPA == heroe.velocidad)
                        //     cambioturno = true;
                        // else
                        //     velPA++;
                        //contador++;
                    }
                }
                else if (Input.GetKey(KeyCode.Space))
                {
                    Vict = true;
                }
            }
            if (cambioturno)
            {
                turno = (turno + 1) % game.HeroesGame.Count;
                cambioturno = false;
                velPA = 0;
                if (turno % game.HeroesGame.Count == 0)
                {
                    HoV = !HoV;
                }
            }
        }
    }

    public void Generar(mapa map)
    {
        for (int i = 0; i < map.SIZE; i++)
        {
            for (int j = 0; j < map.SIZE; j++)
            {
                Vector2 pos = new Vector2(i + 0.5f, j + 0.5f);
                if(map.MAP[i,j] == 0)
                {
                    GameObject clon = Instantiate(Camino, pos, Quaternion.identity);
                    tilesmap[i,j] = clon.transform;
                }
                else if(map.MAP[i,j] == 1)
                {
                    if(map.PosicionValida(map.MAP, i - 1, j, true) && map.PosicionValida(map.MAP, i, j+ 1, true) && map.PosicionValida(map.MAP, i + 1, j, true) && map.MAP[i,j + 1] == 0 && map.MAP[i - 1, j] == 0 && map.MAP[i + 1, j] == 0)
                    {
                        GameObject clon = Instantiate(Pared1, pos, Quaternion.identity);
                        tilesmap[i,j] = clon.transform;
                    }
                    else if(map.PosicionValida(map.MAP, i - 1, j, true) && map.PosicionValida(map.MAP, i, j+ 1, true) && map.MAP[i,j + 1] == 0 && map.MAP[i - 1, j] == 0)
                    {
                        GameObject clon2 = Instantiate(Pared2, pos, Quaternion.identity);
                        tilesmap[i,j] = clon2.transform;
                    }
                    else if(map.PosicionValida(map.MAP, i + 1, j, true) && map.PosicionValida(map.MAP, i, j+ 1, true) && map.MAP[i,j + 1] == 0 && map.MAP[i + 1, j] == 0)
                    {
                        GameObject clon3 = Instantiate(Pared3, pos, Quaternion.identity);
                        tilesmap[i,j] = clon3.transform;
                    }
                    else if(map.PosicionValida(map.MAP, i, j+ 1, true) && map.MAP[i,j + 1] == 0 /*&& map.MAP[i - 1, j] == 0 && map.PosicionValida(map.MAP, i - 1, j, true)*/)
                    {
                        GameObject clon4 = Instantiate(Pared4, pos, Quaternion.identity);
                        tilesmap[i,j] = clon4.transform;
                    }
                    else
                    {
                       if(map.PosicionValida(map.MAP,i - 1, j, true) && map.MAP[i - 1,j] == 0)
                       {
                            GameObject clon5 = Instantiate(Techo, pos, Quaternion.identity);
                            tilesmap[i,j] = clon5.transform;
                       }
                       else if(map.PosicionValida(map.MAP,i + 1, j, true) && map.MAP[i + 1,j] == 0)
                       {
                            GameObject clon6 = Instantiate(Techo2, pos, Quaternion.identity);
                            tilesmap[i,j] = clon6.transform;
                       }
                       else
                       {
                            GameObject clon7 = Instantiate(Techo3, pos, Quaternion.identity);
                            tilesmap[i,j] = clon7.transform;
                       }
                    }
                }
            }
        }
    }
    public IEnumerator Jugar(Game game)
    {
        while(!Vict)
        {
            for (int i = 0; i < game.HeroesGame.Count; i++)
            {
                yield return StartCoroutine(Mover(game.HeroesGame[i], _Heroes[i]));
                if(Vict)
                {
                    yield break;
                }
            }
        }
    }
    public IEnumerator Mover(Heroe heroe, GameObject heroegraph)
    {
        int contador = 0;
        while(contador < heroe.velocidad)
        {
            bool keypress = false;
            if(Input.GetKey(KeyCode.UpArrow))
            {
                if (heroe.map.PosicionValida(heroe.map.MAP, heroe.pos.Item1, heroe.pos.Item2 - 1, false))
                {
                    heroe.pos = (heroe.pos.Item1, heroe.pos.Item2 - 1);
                    yield return StartCoroutine(heroegraph.GetComponent<Jugable>().MoverDestino(heroegraph.transform.position + Vector3.up));
                    keypress = true;
                }
            }
            else if(Input.GetKey(KeyCode.DownArrow))
            {
                if (heroe.map.PosicionValida(heroe.map.MAP, heroe.pos.Item1, heroe.pos.Item2 + 1, false))
                {
                    heroe.pos = (heroe.pos.Item1, heroe.pos.Item2 + 1);
                    yield return StartCoroutine(heroegraph.GetComponent<Jugable>().MoverDestino(heroegraph.transform.position + Vector3.down));
                    keypress = true;
                }
            }
            else if(Input.GetKey(KeyCode.LeftArrow))
            {
                if (heroe.map.PosicionValida(heroe.map.MAP, heroe.pos.Item1 - 1, heroe.pos.Item2, false))
                {
                    heroe.pos = (heroe.pos.Item1 - 1, heroe.pos.Item2);
                    yield return StartCoroutine(heroegraph.GetComponent<Jugable>().MoverDestino(heroegraph.transform.position + Vector3.left));
                    keypress = true;
                }
            }
            else if(Input.GetKey(KeyCode.RightArrow))
            {
                if (heroe.map.PosicionValida(heroe.map.MAP, heroe.pos.Item1 + 1, heroe.pos.Item2, false))
                {
                    heroe.pos = (heroe.pos.Item1 + 1, heroe.pos.Item2);
                    yield return StartCoroutine(heroegraph.GetComponent<Jugable>().MoverDestino(heroegraph.transform.position + Vector3.right));
                    keypress = true;
                }
            }
            else if(Input.GetKey(KeyCode.Space))
            {
                Vict = true;
                yield break;
            }
            if(keypress)
            {
                contador++;
            }
        }
        yield return null;
    }
    public void Construir()
    {
        ConstructordePartida a = GameObject.Find("Constructor").GetComponent<ConstructordePartida>();
        Techo = a.Techo;
        Techo2 = a.Techo2;
        Techo3 = a.Techo3;
        Pared1 = a.Pared1;
        Pared2 = a.Pared2;
        Pared3 = a.Pared3;
        Pared4 = a.Pared4;
        Camino = a.Camino;
        tilesmap = a.tilesmap;
        game = a.game;
        _Heroes = a._Heroes;
        _Villanos = a._Villanos;
    }
}
