using System.Collections;
using UnityEngine;

public class SkullEnemySpawner : MonoBehaviour
{
    public float spawnTime;
    public int quantity;
    public int currentQuantity;
    public GameObject skullGameObject;

    void Start()
    {
        currentQuantity = 0;
        quantity = 5;
    }

    public IEnumerator SpawnSkulls()
    {
        for (int i = currentQuantity; i < quantity; i++)
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
            GameObject.Find("CoinStartSpawn").GetComponent<CoinStartSpawn>().EnableDisable();
        }
    } 
}
