using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Colisao cc next aula
public class PlayerScript : MonoBehaviour
{
    Transform sensor;
    Transform Mira;
    AudioSource som;
    CharacterController cc;
    LayerMask piso;
    Vector3 altura = Vector3.zero;
    void Start()
    {
        som = GetComponent<AudioSource>();
        piso = LayerMask.GetMask("piso");
        sensor = GameObject.FindGameObjectWithTag("Sensor").transform;
        Mira = GameObject.FindGameObjectWithTag("Mira").transform;
        cc = GetComponent<CharacterController>();
        cc.Move(new Vector3(1, 0, 0));
    }

    bool noChao;
    float distanciaChao = 0.6f;
    public float speed = 100f;
    float gravidade = -9.8f;
    public float salto = 100f;

    void Update()
    {

        noChao = Physics.CheckSphere(
            sensor.position,
            distanciaChao,
            piso,
            QueryTriggerInteraction.Collide);
        if (noChao && altura.y < 0) altura.y = -2;
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector3 anda = transform.right * x + transform.forward * z;
        cc.Move(anda);
        if (noChao && Input.GetButtonDown("Jump")) altura.y = Mathf.Sqrt(gravidade * -2.0f * salto);
        altura.y += gravidade;
        cc.Move(altura);
        if (Input.GetKeyDown(KeyCode.Mouse0)) Atira();

    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) lanca();
    }
    public float forcaimpacto = 100f;

    bool stopfire;

    IEnumerator permiteFogo(float segs)
    {
        yield return new WaitForSeconds(segs);
        stopfire = false;
    }

    public AudioClip estouro;
    void lanca()
    {

        if (!stopfire)
        {
            stopfire = true;
            StartCoroutine(permiteFogo(0.5f));
            GameObject bala = (GameObject)Resources.Load("bala", typeof(GameObject));
            bala = Instantiate(bala, Mira.position, Quaternion.identity);
            som.PlayOneShot(estouro);
            Rigidbody rb = bala.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Mira.forward * forcaimpacto;
            }
            GameObject.Destroy(bala.gameObject, 3);
        }
    }



    protected void Atira()
    {

        Debug.DrawRay(Mira.position, Mira.forward * 200f, Color.red, 0.2f);
        som.Play();
        RaycastHit hit;
        if (Physics.Raycast(Mira.position, Mira.forward, out hit, Mathf.Infinity))
        {
            ParticleSystem fogo = (ParticleSystem)Resources.Load("fogo", typeof(ParticleSystem));
            fogo = Instantiate(fogo, hit.point + new Vector3(0, 0, -1), Quaternion.identity);
            GameObject.Destroy(fogo.gameObject, fogo.main.duration);
            if (hit.collider.gameObject.CompareTag("Inimigo"))
            {
                inimigoScript inimigoScript = hit.collider.gameObject.GetComponent<inimigoScript>();
                if (inimigoScript != null) inimigoScript.Matou();

            }
            // Debug.Log(hit.collider.gameObject.name);  
        }
    }

    public float forca = 100f;
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.gameObject.layer == LayerMask.NameToLayer("obstaculo"))
        {
            if (hit.moveDirection.y < 0) return;
            Rigidbody rb = hit.gameObject.GetComponent<Rigidbody>();
            if (rb != null || rb.isKinematic != true)
            {
                rb.velocity = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z) * forca;
            }



        }
    }

}