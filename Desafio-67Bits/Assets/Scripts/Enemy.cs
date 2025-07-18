using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool live;
    private Transform playerTransform;
    public float speed;
    public bool walk;
    public bool idle;
    public bool loose;
    public bool run;

    private Animator animatorController;
    public GameObject stackElement;

    void Start()
    {
        live = true;
        playerTransform = GameObject.Find("Player").transform;
        animatorController = GetComponent<Animator>();
        StartCoroutine(WalkingRandom());
    }


    void Update()
    {
        AnimControll();
    }

    private void AnimControll()
    {
        animatorController.SetBool("Idle", idle);
        animatorController.SetBool("Walk", walk);
        animatorController.SetBool("Run", run);
        animatorController.SetBool("Loose", loose);
    }

    public IEnumerator Struck(float delay, Vector3 direction, float force)
    {
        if (live)
        {
            GetComponent<BoxCollider>().enabled = false;
            yield return new WaitForSeconds(delay);
            live = false;
            idle = false;
            run = false;
            loose = true;


            transform.forward = -direction.normalized;

            float elapsed = 0.0f;

            GetComponent<AudioSource>().Play();

            while (elapsed < 0.75f)
            {
                yield return null;

                elapsed += Time.deltaTime;

                transform.position += force * Time.deltaTime * direction;
            }

            GameObject goStackElement = Instantiate(stackElement);
            goStackElement.transform.position = transform.position + Vector3.up*0.75f;
            GameObject.Find("SkullEnemySpawner").GetComponent<SkullEnemySpawner>().EnemyDestroied();
            Destroy(gameObject);
        }

    }

    IEnumerator WalkingRandom()
    {
        float x = Random.Range(-11.0f, 11.0f);
        float z = Random.Range(-9.0f, 9.0f);
        Vector3 finalPoint = new(x, transform.position.y, z);
        Vector3 iniPoint = transform.position;

        // transform.forward = (finalPoint - iniPoint).normalized;

        float elapsed = 0.0f;
        float time = Random.Range(3.0f, 5.0f);
        while (true)
        {

            if (!live) break;

            transform.forward = (finalPoint - iniPoint).normalized;


            idle = false;
            run = true;
            while (elapsed < time)
            {
                if (!live)
                {
                    idle = false;
                    run = true;
                    break;
                }
                elapsed += Time.deltaTime;
                transform.position = Vector3.Lerp(iniPoint, finalPoint, elapsed / time);
                yield return null;
            }
            if (live)
            {
                idle = true;
                run = false;

                yield return new WaitForSeconds(Random.Range(2.5f, 7.0f));

                elapsed = 0.0f;
                iniPoint = transform.position;

                if (Random.Range(0, 6) == 1)
                {
                    finalPoint.x = playerTransform.position.x;
                    finalPoint.z = playerTransform.position.z;
                }
                else
                {
                    finalPoint.x = Random.Range(-11.0f, 11.0f);
                    finalPoint.z = Random.Range(-9.0f, 9.0f);
                }

                time = Random.Range(3.0f, 5.0f);
            }
            else
            {
                break;
            }

            
        }
    }
}
