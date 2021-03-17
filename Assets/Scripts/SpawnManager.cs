using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] PlatformsPrefabs;
    public GameObject[] CoinPrefabs;
    public GameObject EnemyPrefab;

    public float MinTime = 0.1f;
    public float MaxTime = 0.2f;

    public float NewPlatformSeparation;

    private float InternalPlatformSeparation = 0.0f;
    

    public GameObject Player;

    private int NumPlataforma;
    private GameObject NuevaPlataforma;

    

    void Start()
    {
        StartCoroutine(SpawnCoroutine());
        
    }

    // Update is called once per frame
    void Update()
    {

        


    }

    IEnumerator SpawnCoroutine() {

        while (Player.transform.position.x<InternalPlatformSeparation) {
            yield return null;
        }

        InternalPlatformSeparation += NewPlatformSeparation;


        NuevaPlataforma = Instantiate(PlatformsPrefabs[Random.Range(0, PlatformsPrefabs.Length)],transform.position+new Vector3(0,Random.Range(-0.5f,0.5f),10), Quaternion.identity);

        if (Random.Range(0, 4) == 0)
        {
            Instantiate(CoinPrefabs[Random.Range(0, CoinPrefabs.Length)], NuevaPlataforma.transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity);
        }
        else if (Random.Range(0, 5) == 0) {
            Instantiate(EnemyPrefab, NuevaPlataforma.transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity);
        }


        StartCoroutine(SpawnCoroutine());

    }

}
