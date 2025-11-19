using System;
using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public bool GettingKnockedBack { get; private set; }
    [SerializeField] private float knockBackDuration = 0.5f;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void GetKnockdBack( Transform damageSource, float knockBackthurt)
    {
        GettingKnockedBack = true;
        Vector2 diffirence = (transform.position - damageSource.position).normalized * knockBackthurt * rb.mass;
        rb.AddForce(diffirence, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutime());
    }
    private IEnumerator KnockRoutime()
    {
        yield return new WaitForSeconds(knockBackDuration);
        rb.linearVelocity = Vector2.zero;
        GettingKnockedBack = false;
    }

    internal void GetKnockdBack(object instance)
    {
        throw new NotImplementedException();
    }
}
