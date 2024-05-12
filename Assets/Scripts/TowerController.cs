using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TowerController : MonoBehaviour
{

    [SerializeField] private GameObject bullet;

    [SerializeField] private float health;
    [SerializeField] private float cooldown;

    private float timeSince;
    
    void Start()
    {
        
    }

    void Update()
    {
        timeSince += Time.deltaTime;

        if (timeSince > cooldown)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            timeSince = 0;
        }
    }

    // returns true if destroyed
    public bool Damage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health < 0)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }



    
}
