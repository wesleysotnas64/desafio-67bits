using System.Collections.Generic;
using UnityEngine;

public class EnemyDirection : MonoBehaviour
{
    public bool findEnemys;
    public bool findSkulls;
    public SpriteRenderer directionSprite;
    private Vector3 closeObjPosition;
    private float currentDist;
    private float objDist;
    private Vector3 direction;
    private Color newColor;

    void Update()
    {
        FindNearestEnemy();
    }

    private void FindNearestEnemy()
    {
        GameObject[] allObjTag = new GameObject[0];
        
        if (findEnemys) allObjTag = GameObject.FindGameObjectsWithTag("Enemy");
        if(findSkulls) allObjTag = GameObject.FindGameObjectsWithTag("Carcass");

        if (allObjTag.Length == 0)
        {
            // Nenhum inimigo na cena — tornar o sprite totalmente transparente
            Color hiddenColor = directionSprite.color;
            hiddenColor.a = 0.0f;
            directionSprite.color = hiddenColor;
            return;
        }

        currentDist = 100.0f;

        foreach (GameObject obj in allObjTag)
        {
            objDist = (obj.transform.position - transform.position).magnitude;

            if (objDist < currentDist)
            {
                currentDist = objDist;
                closeObjPosition = obj.transform.position;
            }
        }

        closeObjPosition.y = transform.position.y;
        direction = (closeObjPosition - transform.position).normalized;
        transform.forward = direction;

        // newColor = Color.HSVToRGB(0.0f, 0.5f, 1.0f);
        newColor = directionSprite.color;

        if (currentDist > 3.0f)
        {
            newColor.a = 0.5f;
        }
        else
        {
            newColor.a = 0.0f;
        }

        directionSprite.color = newColor;
    }



}
