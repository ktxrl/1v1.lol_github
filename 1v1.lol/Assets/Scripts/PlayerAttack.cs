using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform attackPointLeft;
    [SerializeField] Transform attackPointRight;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask enemyLayers;

    Collider2D[] hitEnemies;
    bool direction = true;
    float damage = 20f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            direction = true;
        }
    }
    void Attack()
    {
        if (!direction) hitEnemies = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, enemyLayers);
        else hitEnemies = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, enemyLayers);
        Invoke("Destroy", 0.5f);
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPointLeft == null || attackPointRight == null) return;
        Gizmos.DrawWireSphere(attackPointLeft.position, attackRange);
        Gizmos.DrawWireSphere(attackPointRight.position, attackRange);
    }
    public void Destroy()
    {
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.tag == "skeleton")
            {
                enemy.GetComponent<Skeleton>().TakeDamage(damage);
            }
            else if (enemy.gameObject.tag == "flying")
            {
                enemy.GetComponent<Flying>().TakeDamage(damage);
            }
            else if (enemy.gameObject.tag == "lizard")
            {
                enemy.GetComponent<Lizard>().TakeDamage(damage);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "damage")
        {
            damage *= 2;
            Invoke("Damage", 3f);
            Destroy(collision.gameObject);
        }
    }
    public void Damage()
    {
        damage /= 2;
    }
}
