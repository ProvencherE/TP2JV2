using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private int life;

    private GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setDamage(int damage)
    {
        life -= damage;
        if(life <= 0)
        {
            gameManager.GetComponent<GameManager>().towerDestroyed(this.gameObject);
        }
    }
}
