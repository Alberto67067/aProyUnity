using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class Jugable : MonoBehaviour
{
    float velocidad = 5f;
    Vector3 destino;
    public bool moving;
    public IEnumerator MoverDestino(Vector3 nuevo)
    {
        destino = nuevo;
        moving = true;
        while(Vector3.Distance(transform.position, destino) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destino, velocidad);
            yield return new WaitForSeconds(1);
        }
        transform.position = destino;
        moving = false;
    } 

}
