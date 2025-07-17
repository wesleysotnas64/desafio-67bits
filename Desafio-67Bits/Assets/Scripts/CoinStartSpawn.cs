using System.Collections;
using UnityEngine;

public class CoinStartSpawn : MonoBehaviour
{

    public bool enable;
    public float translateTime;

    void Start()
    {
        enable = false;
        // StartCoroutine(TranslateEnable());
    }

    // Update is called once per frame
    void Update()
    {
        SpinAround();
    }

    private void SpinAround()
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.y += 90f * Time.deltaTime;
        transform.eulerAngles = rotation;
    }

    public void EnableDisable()
    {
        if (enable)
        {
            StartCoroutine(
                GameObject.Find("SkullEnemySpawner").GetComponent<SkullEnemySpawner>().SpawnSkulls()
            );
        }

        StartCoroutine(TranslateEnable());
    }

    public IEnumerator TranslateEnable()
    {
        GetComponent<BoxCollider>().enabled = false;

        Vector3 initial;
        Vector3 final;

        if (enable)
        {
            enable = false;
            initial = new(1, 2, 0);
            final = new(1,0,0);
        }
        else
        {
            enable = true;
            final = new(1, 2, 0);
            initial = new(1,0,0);
        }


        float elapsed = 0.0f;
        while (elapsed < translateTime)
        {
            yield return null;
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(initial, final, elapsed / translateTime);
        }

        transform.position = final;

        GetComponent<BoxCollider>().enabled = true;
    }
}
