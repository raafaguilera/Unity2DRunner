using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGeneratorScript : MonoBehaviour
{
    public GameObject PlantPrefab;

    public float NewPlatSeparation;

    private float InternalPlatSeparation = 0.0f;


    public GameObject Player;

    private int NumPlataforma;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCoroutine());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnCoroutine()
    {

        while (Player.transform.position.x < InternalPlatSeparation)
        {
            yield return null;
        }

        InternalPlatSeparation += NewPlatSeparation;


        Instantiate(PlantPrefab, transform.position , Quaternion.identity);

        StartCoroutine(SpawnCoroutine());

    }
}
