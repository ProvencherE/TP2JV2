using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateFlee : WizardState
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().flipY = true;
        speed = 3f;
        attackRange = 4f;
        potentialForestToHideTo = new GameObject[2];
        if (isBlue)
        {
            towersToFleeTo = new GameObject[GameObject.FindGameObjectsWithTag("BlueTower").Length];
            towersToFleeTo = GameObject.FindGameObjectsWithTag("BlueTower");
            potentialForestToHideTo = GameObject.FindGameObjectsWithTag("BlueForest");
        }
        else if (isGreen)
        {
            towersToFleeTo = new GameObject[GameObject.FindGameObjectsWithTag("GreenTower").Length];
            towersToFleeTo = GameObject.FindGameObjectsWithTag("GreenTower");
            potentialForestToHideTo = GameObject.FindGameObjectsWithTag("BlueForest");
        }
        determineNearestFleeingPosition();
    }

    private void determineNearestFleeingPosition()
    {
        GameObject nearestTower = new GameObject();
        GameObject nearestForest = new GameObject();
        //Trouver le code ici: https://forum.unity.com/threads/clean-est-way-to-find-nearest-object-of-many-c.44315/
        float minDist = Mathf.Infinity;
        Vector2 currentPos = transform.position;
        foreach (GameObject tower in towersToFleeTo)
        {
            float dist = Vector2.Distance(tower.transform.position, currentPos);
            if (dist < minDist)
            {
                nearestTower = tower;
                minDist = dist;
            }
        }
        foreach (GameObject forest in potentialForestToHideTo)
        {
            float dist = Vector2.Distance(forest.transform.position, currentPos);
            if (dist < minDist)
            {
                nearestTower = forest;
                minDist = dist;
            }
        }
        if(Vector2.Distance(nearestTower.transform.position, currentPos) < Vector2.Distance(nearestForest.transform.position, currentPos))
        {
            fleeingPosition = nearestTower;
        }
        else if(Vector2.Distance(nearestTower.transform.position, currentPos) > Vector2.Distance(nearestForest.transform.position, currentPos))
        {
            fleeingPosition = nearestForest;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveWizard();
    }

    public override void ManageStateChange()
    {
        
    }

    public override void MoveWizard()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBlue)
        {
            if(collision.gameObject.tag == "BlueForest")
            {

            }
        }
        else if (isGreen)
        {

        }
    }

}
