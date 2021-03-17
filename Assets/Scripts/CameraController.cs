using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform targetPlayer;

    // Start is called before the first frame update
    void Start()
    {
        // Si no esta asignado en el inspector
         if (targetPlayer == null)
         {
            targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
         }
    }

    // Update is called once per frame
    void Update()
    {

            transform.position = new Vector3(targetPlayer.position.x + 1.5f, 0, -10);
    }
}
