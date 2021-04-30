using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class controllerScript : MonoBehaviour
{
    public Transform[] Covis;
    private bool stop = false;
    public static int Total = 0;
    public static int Pontos = 0;
    void Start()
    {
        StartCoroutine(DesovaEnumerator(1.5f));
    }

    private void Update()
    {
        if (Pontos > 2) StopCoroutine(DesovaEnumerator(2));
    }

    IEnumerator DesovaEnumerator(float tempo)
    {
        int index;
        while (true)
        {
            if (Total < 5)
            {
                index = Random.RandomRange(0, Covis.Length);
                GameObject ini = (GameObject)Resources.Load("inimigo", typeof(GameObject));
                ini = Instantiate(ini, Covis[0].position, Quaternion.identity);
                Total++;
            }
            yield return new WaitForSeconds(tempo);
            if (Pontos > 3) break;

        }
    }
}
