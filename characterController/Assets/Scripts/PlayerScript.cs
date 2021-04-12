using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Transform sensor;    
    private Transform mira;
    CharacterController cc;
    private LayerMask piso;
    Vector3 altura = Vector3.zero;

    void Start()
    {
        piso = LayerMask.GetMask("piso");
        sensor = GameObject.FindGameObjectWithTag("Sensor").transform;
        mira = GameObject.FindGameObjectWithTag("Mira").transform;
        cc = GetComponent<CharacterController>();
        cc.Move(new Vector3(1, 0, 0));
    }

    private bool noChao;
    private float distChao = 0.6f;
    public float speed = 100f;
    private float gravidade = -9.8f;
    public float jump = 100f;

    // Update is called once per frame
    void Update()
    {

        noChao = Physics.CheckSphere(sensor.position, distChao, piso, QueryTriggerInteraction.Collide);
        if (noChao && altura.y < 0) altura.y = -2;
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector3 anda = transform.right * x + transform.forward * z;
        cc.Move(anda);

        if (noChao && Input.GetButtonDown("Jump")) altura.y = Mathf.Sqrt(gravidade * -2.0f * jump);
        altura.y += gravidade;


        cc.Move(altura);

        if (Input.GetKeyDown(KeyCode.Mouse0)) shoot();
    }

    protected void shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(mira.position,mira.forward, out hit, Mathf.Infinity))
        {
            Debug.Log(hit.collider.gameObject.name);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        float forca = 100f;
        if (hit.gameObject.layer == LayerMask.NameToLayer("obstaculo"))
        {
            if (hit.moveDirection.y < 0) return;
            Rigidbody rb = hit.gameObject.GetComponent<Rigidbody>();
            if (rb != null || rb.isKinematic != true)
            {
                rb.velocity = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z) * forca;
            }

            Debug.Log("Yey :D");
        }
    }
}
