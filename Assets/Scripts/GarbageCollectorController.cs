﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollectorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colector");
        Destroy(collision.gameObject);
    }
}
