using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform player;
    private Vector3 playerRotation, transformRotation;
    private float sensi = 100f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Mouse X") * sensi * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * sensi * Time.deltaTime;
        transformRotation.x -= y;
        transformRotation.x = Mathf.Clamp(transformRotation.x, -30f, 30f);
        transform.localRotation = Quaternion.Euler(transformRotation);

        playerRotation.y += x;
        player.localRotation=Quaternion.Euler(playerRotation);

        if (Input.GetKeyDown(KeyCode.LeftControl)) playerRotation.y += 180f;
    }
}
