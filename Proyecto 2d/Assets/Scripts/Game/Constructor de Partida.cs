using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using MySerializeJson;
using System.IO;
using Mapa;
using Partida;
using Heroes;
using Unity.VisualScripting;

public class ConstructordePartida : MonoBehaviour
{
    public Game game;
    [SerializeField]public List<GameObject> _Heroes;
    [SerializeField]public List<GameObject> _Villanos;
    public List<Sprite> HeroesGra;
    public List<Sprite> Heroes
    {
        get{ return HeroesGra; }
        set{HeroesGra = value;}
    }
    public List<Heroe> HeroesGame;
    public List<Heroe> VillanosGame;
    [SerializeField] public Transform[,] tilesmap;
    [SerializeField]public GameObject Techo;
    [SerializeField]public GameObject Techo2;
    [SerializeField]public GameObject Techo3;
    [SerializeField]public GameObject Pared1;
    [SerializeField]public GameObject Pared2;
    [SerializeField]public GameObject Pared3;
    [SerializeField]public GameObject Pared4;
    [SerializeField]public GameObject Camino;
    GameGraphic gamegra;
    void Start()
    {
        var rutaH = Path.Combine(Application.persistentDataPath, "DatosGame.json");
        var Datostext = File.ReadAllText(rutaH);
        MySerInit Datos = JsonUtility.FromJson<MySerInit>(Datostext);
        Construir(Datos);
    }
    public void Construir(MySerInit datos)
    {
        int cantEntr = 0;
        for (int i = 0; i < 6; i++)
        {
            if (datos.Heroes[i])
            {
                cantEntr++;
            }
        }
        mapa Map = new mapa(datos.MapSize, cantEntr, cantEntr);
        Map.Paredes(Map.ENTRDS, Map.SALDS);
        for (int i = 0; i < 6; i++)
        {
            if (datos.Villians[i] == true)
            {
                ConstruirV(i, Map);
            }
            if (datos.Heroes[i] == true)
            {
                ConstruirH(i, Map);
            }
        }

        game = new Game(Map, HeroesGame, VillanosGame);
        tilesmap = new Transform[Map.SIZE,Map.SIZE];
        // gamegra.Techo = Techo;
        // gamegra.Techo2 = Techo2;
        // gamegra.Techo3 = Techo3;
        // gamegra.Pared1 = Pared1;
        // gamegra.Pared2 = Pared2;
        // gamegra.Pared3 = Pared3;
        // gamegra.Pared4 = Pared4;
        // gamegra.Camino = Camino;
        Instantiate(Resources.Load<GameObject>("GameGraphic"), new Vector3(0,0,0), Quaternion.identity);
        Destroy(this.gameObject);
    }
    public void ConstruirH(int n, mapa Map)
    {
        switch (n)
        {
            case 0:
            Tipo.AtacarPoder FireBall = new Tipo.AtacarPoder("Fire Ball", 10, 4, 25, 20);
            Tipo Knight = new Tipo("Knight", (int)Elemento.Fuego, (int)Elemento.Fuego, (int)Elemento.Agua, 1, FireBall);
            token Pawn = new token("Pawn", 10, 50, 50, 4, 10);
            Heroe KnightFire = new Heroe("Knight Fire", Map, 10, 1, Knight, 4, 10, 90, 110, new token[]{Pawn});
            GameObject KnightFireO = new GameObject("KnightFire");
            SpriteRenderer sprite1  = KnightFireO.AddComponent<SpriteRenderer>();
            sprite1.sortingLayerName = "Knight";
            sprite1.sprite = HeroesGra[0];
            HeroesGame.Add(KnightFire);
            _Heroes.Add(KnightFireO);
            break;
            case 1:
            Tipo.AtacarPoder MagicStorm = new Tipo.AtacarPoder("Magic Storm", 12, 3, 25, 15);
            Tipo Magician = new Tipo("Magician", (int)Elemento.Agua, (int)Elemento.Agua, (int)Elemento.Fuego, 1, MagicStorm);
            token MagicPawn = new token("Magic Pawn", 5, 40, 40, 6, 12);
            Heroe BlueMagician = new Heroe("Blue Magician", Map, 10, 1, Magician, 4, 10, 90, 110, new token[]{MagicPawn});
            GameObject BlueMagicianO = new GameObject("BlueMagician");
            SpriteRenderer sprite2  = BlueMagicianO.AddComponent<SpriteRenderer>();
            sprite2.sortingLayerName = "Knight";
            sprite2.sprite = HeroesGra[1];
            HeroesGame.Add(BlueMagician);
            _Heroes.Add(BlueMagicianO);
            break;
            // case 1:
            // Tipo.AtacarPoder MagicStorm = new Tipo.AtacarPoder("Magic Storm", 12, 3, 25, 15);
            // Tipo Magician = new Tipo("Magician", (int)Elemento.Agua, (int)Elemento.Agua, (int)Elemento.Fuego, 1, MagicStorm);
            // token MagicPawn = new token("Magic Pawn", 5, 40, 40, 6, 12);
            // Heroe BlueMagician = new Heroe("Blue Magician", Map, 10, 1, Magician, 4, 10, 90, 110, new token[]{MagicPawn});
            // break;
            // case 1:
            // Tipo.AtacarPoder MagicStorm = new Tipo.AtacarPoder("Magic Storm", 12, 3, 25, 15);
            // Tipo Magician = new Tipo("Magician", (int)Elemento.Agua, (int)Elemento.Agua, (int)Elemento.Fuego, 1, MagicStorm);
            // token MagicPawn = new token("Magic Pawn", 5, 40, 40, 6, 12);
            // Heroe BlueMagician = new Heroe("Blue Magician", Map, 10, 1, Magician, 4, 10, 90, 110, new token[]{MagicPawn});
            // break;
            // case 1:
            // Tipo.AtacarPoder MagicStorm = new Tipo.AtacarPoder("Magic Storm", 12, 3, 25, 15);
            // Tipo Magician = new Tipo("Magician", (int)Elemento.Agua, (int)Elemento.Agua, (int)Elemento.Fuego, 1, MagicStorm);
            // token MagicPawn = new token("Magic Pawn", 5, 40, 40, 6, 12);
            // Heroe BlueMagician = new Heroe("Blue Magician", Map, 10, 1, Magician, 4, 10, 90, 110, new token[]{MagicPawn});
            // break;
            // case 1:
            // Tipo.AtacarPoder MagicStorm = new Tipo.AtacarPoder("Magic Storm", 12, 3, 25, 15);
            // Tipo Magician = new Tipo("Magician", (int)Elemento.Agua, (int)Elemento.Agua, (int)Elemento.Fuego, 1, MagicStorm);
            // token MagicPawn = new token("Magic Pawn", 5, 40, 40, 6, 12);
            // Heroe BlueMagician = new Heroe("Blue Magician", Map, 10, 1, Magician, 4, 10, 90, 110, new token[]{MagicPawn});
            // break;case 1:
            // Tipo.AtacarPoder MagicStorm = new Tipo.AtacarPoder("Magic Storm", 12, 3, 25, 15);
            // Tipo Magician = new Tipo("Magician", (int)Elemento.Agua, (int)Elemento.Agua, (int)Elemento.Fuego, 1, MagicStorm);
            // token MagicPawn = new token("Magic Pawn", 5, 40, 40, 6, 12);
            // Heroe BlueMagician = new Heroe("Blue Magician", Map, 10, 1, Magician, 4, 10, 90, 110, new token[]{MagicPawn});
            // break;
        }
    }
    public void ConstruirV(int n, mapa Map)
    {
        switch (n)
        {
            case 0:
            Tipo.AtacarPoder FireBall = new Tipo.AtacarPoder("Fire Ball", 10, 4, 25, 20);
            Tipo Knight = new Tipo("Knight", (int)Elemento.Fuego, (int)Elemento.Fuego, (int)Elemento.Agua, 1, FireBall);
            token DarkPawn = new token("Pawn", 10, 50, 50, 4, 10);
            Heroe KnightFireVillian = new Heroe("Knight Fire", Map, 10, 0, Knight, 4, 10, 90, 110, new token[]{DarkPawn});
            GameObject KnightFireVillianO = new GameObject("KnightFireVillian");
            SpriteRenderer sprite3  = KnightFireVillianO.AddComponent<SpriteRenderer>();
            sprite3.sortingLayerName = "Knight";
            sprite3.sprite = HeroesGra[0];
            VillanosGame.Add(KnightFireVillian);
            _Villanos.Add(KnightFireVillianO);
            break;
            case 1:
            Tipo.AtacarPoder MagicStorm = new Tipo.AtacarPoder("Magic Storm", 12, 3, 25, 15);
            Tipo Magician = new Tipo("Magician", (int)Elemento.Agua, (int)Elemento.Agua, (int)Elemento.Fuego, 1, MagicStorm);
            token DarkMagicPawn = new token("Magic Pawn", 5, 40, 40, 6, 12);
            Heroe BlueMagicianVillian = new Heroe("Blue Magician", Map, 10, 0, Magician, 4, 10, 90, 110, new token[]{DarkMagicPawn});
            GameObject BlueMagicianVillianO = new GameObject("BlueMagicianVillian");
            SpriteRenderer sprite4  = BlueMagicianVillianO.AddComponent<SpriteRenderer>();
            sprite4.sortingLayerName = "Knight";
            sprite4.sprite = HeroesGra[1];
            VillanosGame.Add(BlueMagicianVillian);
            _Villanos.Add(BlueMagicianVillianO);
            break;
            // case 1:
            // Tipo.AtacarPoder MagicStorm = new Tipo.AtacarPoder("Magic Storm", 12, 3, 25, 15);
            // Tipo Magician = new Tipo("Magician", (int)Elemento.Agua, (int)Elemento.Agua, (int)Elemento.Fuego, 1, MagicStorm);
            // token MagicPawn = new token("Magic Pawn", 5, 40, 40, 6, 12);
            // Heroe BlueMagician = new Heroe("Blue Magician", Map, 10, 1, Magician, 4, 10, 90, 110, new token[]{MagicPawn});
            // break;
            // case 1:
            // Tipo.AtacarPoder MagicStorm = new Tipo.AtacarPoder("Magic Storm", 12, 3, 25, 15);
            // Tipo Magician = new Tipo("Magician", (int)Elemento.Agua, (int)Elemento.Agua, (int)Elemento.Fuego, 1, MagicStorm);
            // token MagicPawn = new token("Magic Pawn", 5, 40, 40, 6, 12);
            // Heroe BlueMagician = new Heroe("Blue Magician", Map, 10, 1, Magician, 4, 10, 90, 110, new token[]{MagicPawn});
            // break;
            // case 1:
            // Tipo.AtacarPoder MagicStorm = new Tipo.AtacarPoder("Magic Storm", 12, 3, 25, 15);
            // Tipo Magician = new Tipo("Magician", (int)Elemento.Agua, (int)Elemento.Agua, (int)Elemento.Fuego, 1, MagicStorm);
            // token MagicPawn = new token("Magic Pawn", 5, 40, 40, 6, 12);
            // Heroe BlueMagician = new Heroe("Blue Magician", Map, 10, 1, Magician, 4, 10, 90, 110, new token[]{MagicPawn});
            // break;
            // case 1:
            // Tipo.AtacarPoder MagicStorm = new Tipo.AtacarPoder("Magic Storm", 12, 3, 25, 15);
            // Tipo Magician = new Tipo("Magician", (int)Elemento.Agua, (int)Elemento.Agua, (int)Elemento.Fuego, 1, MagicStorm);
            // token MagicPawn = new token("Magic Pawn", 5, 40, 40, 6, 12);
            // Heroe BlueMagician = new Heroe("Blue Magician", Map, 10, 1, Magician, 4, 10, 90, 110, new token[]{MagicPawn});
            // break;case 1:
            // Tipo.AtacarPoder MagicStorm = new Tipo.AtacarPoder("Magic Storm", 12, 3, 25, 15);
            // Tipo Magician = new Tipo("Magician", (int)Elemento.Agua, (int)Elemento.Agua, (int)Elemento.Fuego, 1, MagicStorm);
            // token MagicPawn = new token("Magic Pawn", 5, 40, 40, 6, 12);
            // Heroe BlueMagician = new Heroe("Blue Magician", Map, 10, 1, Magician, 4, 10, 90, 110, new token[]{MagicPawn});
            // break;
        }
    }
}
