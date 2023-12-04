using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    public static int enemyCount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = transform.childCount;
        Debug.Log(enemyCount);
    }
}
