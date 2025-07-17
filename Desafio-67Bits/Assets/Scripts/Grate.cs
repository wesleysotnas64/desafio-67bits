using UnityEngine;

public class Grate : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        GameObject.Find("Canvas").GetComponent<SceneController>().Point();
    }
}
