using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool live;
    private Transform playerTransform;
    private Vector3 look;
    public float speed;
    public bool walking;
    public bool idle;

    void Start()
    {
        live = true;
        playerTransform = GameObject.Find("Player").transform;
        // StartCoroutine(WalkingRandom());
    }


    void Update()
    {
        if (live)
        {
            LookAtPlayer();
        }
    }

    private void LookAtPlayer()
    {
        look = playerTransform.position;
        look.y = transform.position.y;
        transform.forward = (look - transform.position).normalized;
    }

    public IEnumerator Struck(float delay, Vector3 direction)
    {
        yield return new WaitForSeconds(delay);
        live = false;
        gameObject.tag = "Carcass";
        gameObject.layer = LayerMask.NameToLayer("Carcass");

        direction.y = 2;
        direction *= 200.0f;
        GetComponent<Rigidbody>().AddForce(direction, ForceMode.Force);
    }

    IEnumerator WalkingRandom()
    {
        float x = Random.Range(-11.0f, 11.0f);
        float z = Random.Range(-9.0f, 9.0f);
        Vector3 finalPoint = new(x, transform.position.y, z);
        Vector3 iniPoint = transform.position;

        transform.forward = (finalPoint - iniPoint).normalized;

        float elapsed = 0.0f;
        float time = Random.Range(1.0f, 3.0f);
        while (true)
        {
            while (elapsed < time)
            {
                elapsed += Time.deltaTime;
                transform.position = Vector3.Lerp(iniPoint, finalPoint, elapsed / time);
                yield return null;
            }

            yield return new WaitForSeconds(Random.Range(0.5f, 3.0f));

            elapsed = 0.0f;
            iniPoint = transform.position;

            if (Random.Range(0, 2) == 1)
            {
                finalPoint.x = playerTransform.position.x;
                finalPoint.z = playerTransform.position.z;
            }
            else
            {
                finalPoint.x = Random.Range(-11.0f, 11.0f);
                finalPoint.z = Random.Range(-9.0f, 9.0f);
            }
            
            time = Random.Range(1.0f, 3.0f);
            
        }
    }
}
