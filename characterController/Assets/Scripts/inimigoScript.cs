using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class inimigoScript : MonoBehaviour
{
    private Transform _player;
    private NavMeshAgent _agente;
    private AudioSource _sonido;
    void Start()
    {
        _sonido = GetComponent<AudioSource>();
        _agente = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Matou()
    {
        _sonido.Play();
        GameObject.Destroy(transform.gameObject,0.5f);
    }

    void Update()
    {
        _agente.destination = _player.position;
        if (Vector3.Distance(transform.position, _player.position) < 25)
        {
            _agente.isStopped = true;
        }
    }
}
