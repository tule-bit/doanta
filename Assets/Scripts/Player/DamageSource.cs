using UnityEngine;

public class DamageSource : MonoBehaviour
{
    public int Damage = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        enemyHealth?.TakeDamage(Damage);
    }
}
