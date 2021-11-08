using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text gameOverText;
    [SerializeField] private float spawningRate;
    [SerializeField] private int nbMaxWizard;
    [SerializeField] private GameObject greenWizard;
    [SerializeField] private GameObject blueWizard;

    private float timerRespawn;
    private List<GameObject> greenTowers;
    private List<GameObject> blueTowers;
    private GameObject[] greenWizards;
    private GameObject[] blueWizards;

    // Start is called before the first frame update
    void Start()
    {
        timerRespawn = spawningRate;
        greenTowers = new List<GameObject>(GameObject.FindGameObjectsWithTag("GreenTower"));
        blueTowers = new List<GameObject>(GameObject.FindGameObjectsWithTag("BlueTower"));
        instantiateWizard();
    }

    // Update is called once per frame
    void Update()
    {
        spawn();
    }

    private void spawn()
    {
        if(timerRespawn > 0)
        {
            timerRespawn -= Time.deltaTime;
        }else
        {
            GameObject tower = randomTower(greenTowers);
            GameObject wizard = firstNotActive(greenWizards);
            if (wizard != null) wizard.GetComponent<WizardManager>().spawn(tower.transform.position);
            tower = randomTower(blueTowers);
            wizard = firstNotActive(blueWizards);
            if (wizard != null) wizard.GetComponent<WizardManager>().spawn(tower.transform.position);
            timerRespawn = spawningRate;
        }
    }

    private GameObject firstNotActive(GameObject[] wizards)
    {
        for(int i = 0; i < wizards.Length; i++)
        {
            if (!wizards[i].activeSelf)
            {
                return wizards[i];
            }
        }
        return null;
    }

    private GameObject randomTower(List<GameObject> towers)
    {
        return towers[UnityEngine.Random.Range(0, towers.Count)];
    }

    private void instantiateWizard()
    {
        blueWizards = new GameObject[nbMaxWizard];
        greenWizards = new GameObject[nbMaxWizard];
        for (int i = 0; i < nbMaxWizard; i++)
        {
            blueWizards[i] = Instantiate(blueWizard);
            greenWizards[i] = Instantiate(greenWizard);
            blueWizards[i].SetActive(false);
            greenWizards[i].SetActive(false);
        }
        
    }

    public void towerDestroyed(GameObject towerDestroyed)
    {
        greenTowers.Remove(towerDestroyed);
    }
}
