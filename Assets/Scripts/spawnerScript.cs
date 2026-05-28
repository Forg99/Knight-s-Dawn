using System;
using UnityEngine;

public class spawnerScript : MonoBehaviour
{
    public GameObject Enemy;
    public float spawnRate = 10;
    private float timer = 0;
   
    void Update()
    {
        if(timer<spawnRate)
        {
            timer = timer + Time.deltaTime;
            

        }
        else
        {
            spawnEnemy();
            timer = 0;
        }

    }

    private void spawnEnemy()
    {
        Instantiate(Enemy, transform.position, transform.rotation);
    }


}
