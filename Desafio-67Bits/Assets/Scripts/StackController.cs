using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StackController : MonoBehaviour
{
    public List<GameObject> stack;
    public GameObject stackGameObject;
    public int maxStack;
    public int currentStack;

    void Start()
    {
        stack = new List<GameObject>();
        maxStack = 3;
        currentStack = 0;
    }


    public void AddElementAtStack(GameObject carcassGameObject)
    {
        if (currentStack < maxStack)
        {
            carcassGameObject.GetComponent<BoxCollider>().enabled = false;

            if (currentStack == 0)
            {
                carcassGameObject.GetComponent<StackElement>().target = gameObject.transform;
            }
            else
            {
                carcassGameObject.GetComponent<StackElement>().target = stack.Last().transform;
            }
            stack.Add(carcassGameObject);
            currentStack++;
        }

    }

    public void RemoveAllElements()
    {
        StartCoroutine(RemoveAllElementsEnum());
    }

    IEnumerator RemoveAllElementsEnum()
    {
        while (currentStack > 0)
        {
            yield return new WaitForSeconds(0.2f);

            GameObject lastElement = stack.Last();
            lastElement.GetComponent<BoxCollider>().enabled = true;

            StackElement element = lastElement.GetComponent<StackElement>();
            if (element != null)
            {
                element.target = GameObject.Find("FireDTarget").transform;
            }

            stack.RemoveAt(stack.Count - 1);
            currentStack--;
        }

    }
}
