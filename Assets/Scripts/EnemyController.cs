using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float health;
    public float damage;
    public float cooldown = 1f;
    public GameObject canvas;
    public HealthBar healthBar;

    private bool atTower = false;
    private TowerController tower;
    private float time = 0f;

    private void Start()
    {
        canvas = transform.GetChild(0).gameObject;
        healthBar = canvas.transform.GetChild(0).gameObject.GetComponent<HealthBar>();
        healthBar.SetMaxHealth((int) health);
    }

    void Update()
    {
        if (health < 0)
        {
            Destroy(canvas);
            Destroy(gameObject);
        }
        if (atTower)
        {
            time -= Time.deltaTime;
            if (time < 0) { 
                bool isDestroyed = tower.Damage(damage);
                time = cooldown;
                if (isDestroyed)
                {
                    atTower = false;
                    tower = null;
                }
            }
        } else
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Gold")
        {
            LevelManager.main.EndGame();
        } else if (collision.gameObject.tag == "Projectile")
        {
            health -= collision.gameObject.GetComponent<BulletController>().damage;
            healthBar.SetHealth((int)health);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.tag == "Tower")
        {
            tower = collision.gameObject.GetComponent<TowerController>();
            atTower = true;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
