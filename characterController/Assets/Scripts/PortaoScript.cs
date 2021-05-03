using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaoScript : MonoBehaviour
{
    Animator animator;
    AudioSource som;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        som=GetComponent<AudioSource>();
    }

    public void Ranger(){
        som.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            animator.SetBool("abre",true);
            animator.SetBool("fecha",false);

        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")){
            animator.SetBool("abre",false);
            animator.SetBool("fecha",true);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
