using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    Transform Mira, Arma;
    Transform player;
    Transform Ponta;
    Vector3 playerRot, transformRot;
    float sensi = 100f;
	void Start () {
        Ponta = GameObject.FindGameObjectWithTag("Ponta").transform;
        Mira = GameObject.FindGameObjectWithTag("Mira").transform;
        Arma = GameObject.FindGameObjectWithTag("Arma").transform;
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	
	void Update () {
        float x = Input.GetAxis("Mouse X") * sensi * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * sensi * Time.deltaTime;
        //camara roda sobre x
        transformRot.x -= y;
        transformRot.x = Mathf.Clamp(transformRot.x, -60f, 60f);
        transform.localRotation = Quaternion.Euler(transformRot);
        Arma.localRotation = Quaternion.Euler(transformRot.x - 90, 0, 0);
        Mira.localRotation= Quaternion.Euler(transformRot);
        Mira.position = Ponta.position;
        if (Input.GetKeyDown(KeyCode.F2)) playerRot.y += 180f;
        playerRot.y += x;
        player.localRotation = Quaternion.Euler(playerRot);
	}
}
