using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingMonster : Entity
{
   private float speed = 2f;
    private SpriteRenderer sprite;
    private Vector3 dir;
    private int lives = 2;


    //реализуем уничтожение персонажа
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject==Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            lives--;
            Debug.Log($"Осталось {lives} жизней ");
        }
        if (lives<1)
        {
            Die();
        }
    }


    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    void Start()
    {
        dir = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        Moove();
        
    }
    /// <summary>
    /// Метод движения Монстра туда обратно
    /// </summary>
    private void Moove() {

        Collider2D[] collider2s = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.1f + transform.right * dir.x * 0.7f, 0.1f);
        if (collider2s.Length > 0) dir *= -1f;
        sprite.flipX = dir.x>0.0f;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, Time.deltaTime);
        }
        
    }

