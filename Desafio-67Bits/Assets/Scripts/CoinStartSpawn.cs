using System.Collections;
using UnityEngine;

public class CoinStartSpawn : MonoBehaviour
{

    public bool enable;
    public float translateTime;

    void Start()
    {
        enable = false;
        StartCoroutine(TranslateEnable());
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

    IEnumerator TranslateEnable()
    {
        GetComponent<BoxCollider>().enabled = false;

        Vector3 initial;
        Vector3 final;

        if (enable)
        {
            enable = false;
            initial = new(0, 2, 0);
            final = Vector3.zero;
        }
        else
        {
            enable = true;
            final = new(0, 2, 0);
            initial = Vector3.zero;
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
