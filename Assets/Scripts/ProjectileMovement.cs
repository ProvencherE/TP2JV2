using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private int damage;
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActiveAndEnabled)
        {
            travelToTarget();
        }
    }

    public void fire(Vector3 position, GameObject target)
    {
        transform.position = position;
        this.target = target;
        gameObject.SetActive(true);
    }

    private void travelToTarget()
    {
        /*if (Vector3.Distance(transform.position, target.transform.position) < 0.1)
        {
            if (target.tag == "GreenTower" || target.tag == "BlueTower")
            {
                target.GetComponent<TowerManager>().setDamage(damage);
            }
            gameObject.SetActive(false);
        }
        else
        {*/
            Vector3 translate = new Vector3(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, 0);
            transform.Translate(translate.normalized * Time.deltaTime * projectileSpeed);
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameObject.tag == "BlueProjectile")
        {
            if (collision.gameObject.tag == "GreenTower")
            {
                collision.gameObject.GetComponent<TowerManager>().setDamage(damage);
                gameObject.SetActive(false);
            }else if(collision.gameObject.tag == "GreenWizard")
            {
                collision.gameObject.GetComponent<WizardManager>().setDamage(damage);
                gameObject.SetActive(false);
            }
        }
        else if(gameObject.tag == "GreenProjectile")
        {
            if (collision.gameObject.tag == "BlueTower")
            {
                collision.gameObject.GetComponent<TowerManager>().setDamage(damage);
                gameObject.SetActive(false);
            }
            else if (collision.gameObject.tag == "BlueWizard")
            {
                collision.gameObject.GetComponent<WizardManager>().setDamage(damage);
                gameObject.SetActive(false);
            }
        }
    }
}
