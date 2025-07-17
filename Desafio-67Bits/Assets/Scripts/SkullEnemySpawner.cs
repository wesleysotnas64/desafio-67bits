using System.Collections;
using UnityEngine;

public class SkullEnemySpawner : MonoBehaviour
{
    public float spawnTime;
    public int quantity;
    public int currentQuantity;
    public GameObject skullGameObject;

    // void Start()
    // {
    //     StartCoroutine(SpawnSkulls());
    // }


    public IEnumerator SpawnSkulls()
    {
        for (int i = 0; i < quantity; i++)
        {
            Instantiate(skullGameObject).transform.position = transform.position;
            currentQuantity++;
            yield return new WaitForSeconds(spawnTime);
        }

    }

    public void EnemyDestroied()
    {
        currentQuantity--;
        if (currentQuantity <= 0)
        {

        }
    } 
}
