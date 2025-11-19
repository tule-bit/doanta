using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
   
    [SerializeField] private GameObject DeathVfx;
    [SerializeField] private float knockBackThurt = 15f;
    [SerializeField] private int health = 3;
    public int currentHealth;
    private Flash flash; 
    private KnockBack knockBack;
    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth   = health;
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockBack.GetKnockdBack(PlayerController.instance.transform,knockBackThurt);
        StartCoroutine(flash.FlashRouTine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreDefaultMatime());
        Die();
    }
    public void Die()
    {if (currentHealth < 0)
        {
            Instantiate(DeathVfx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
