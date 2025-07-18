using UnityEngine;

public class Grate : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        GameObject.Find("Canvas").GetComponent<SceneController>().Point();
        audioSource.Play();
    }
}
