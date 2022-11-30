using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   
    public GameObject prefab;
    public float minTime;
    public float maxTime;
    
// Start is called before the first frame update
    void Start()
    {
        Spawn();
        
    }

    
    private void Spawn()
    {
        
    Instantiate(prefab, transform.position, Quaternion.identity);
    Invoke(nameof(Spawn), Random.Range(minTime, maxTime));
        
    }
}




