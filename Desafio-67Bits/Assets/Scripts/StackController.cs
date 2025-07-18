using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StackController : MonoBehaviour
{
    public List<GameObject> stack;
    public GameObject stackGameObject;
    public int maxStackSlot;
    public int currentStackSlot;

    void Start()
    {
        stack = new List<GameObject>();
        maxStackSlot = 1;
        currentStackSlot = 0;
    }


    public void AddElementAtStack(GameObject carcassGameObject)
    {
        if (NotMaximumLoad())
        {
            carcassGameObject.tag = "Taked";
            carcassGameObject.layer = LayerMask.NameToLayer("Taked");
            carcassGameObject.GetComponent<BoxCollider>().enabled = false;

            if (currentStackSlot == 0)
            {
                carcassGameObject.GetComponent<StackElement>().target = gameObject.transform;
            }
            else
            {
                carcassGameObject.GetComponent<StackElement>().target = stack.Last().transform;
            }
            stack.Add(carcassGameObject);
            currentStackSlot++;
        }

    }

    public bool NotMaximumLoad()
    {
        return currentStackSlot < maxStackSlot;
    }

    public void RemoveAllElements()
    {
        StartCoroutine(RemoveAllElementsEnum());
    }

    IEnumerator RemoveAllElementsEnum()
    {
        while (currentStackSlot > 0)
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
            currentStackSlot--;
        }

    }
}
