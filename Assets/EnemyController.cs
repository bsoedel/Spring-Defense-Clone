using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float health;

    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Gold")
        {
            LevelManager.main.EndGame();
        } else if (collision.gameObject.tag == "Projectile")
        {

        }
    }
}
