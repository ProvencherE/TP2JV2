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
            if(wizard != null) wizard.GetComponent<WizardManager>().spawn(tower.transform.position);
            tower = randomTower(greenTowers);
            wizard = firstNotActive(greenWizards);
            if (wizard != null) wizard.GetComponent<WizardManager>().spawn(tower.transform.position);
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

    public void towerDestroyed(GameObject towerDestroyed)
    {
        greenTowers.Remove(towerDestroyed);
    }
}
