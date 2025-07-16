using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StackController : MonoBehaviour
{
    public List<GameObject> stack;
    public GameObject stackGameObject;

    void Start()
    {
        stack = new List<GameObject>();
    }


    public void AddElementAtStack(GameObject carcassGameObject)
    {
        // carcassGameObject.GetComponent<BoxCollider>().enabled = false;
        // carcassGameObject.GetComponent<Rigidbody>().useGravity = false;
        if (stack.Count == 0)
        {
            carcassGameObject.GetComponent<StackElement>().target = gameObject.transform;
        }
        else
        {
            carcassGameObject.GetComponent<StackElement>().target = stack.Last().transform;
        }
        stack.Add(carcassGameObject);

    }
}
