using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAl : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f;

    private enum State
    {
        Roaming
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private State state;
    private EnemyPathing enemyPathing;
    private void Awake()
    {
        enemyPathing = GetComponent<EnemyPathing>();
        state = State.Roaming;
    }
    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }
    private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamPosition();
            enemyPathing.MoveTo(roamPosition);
            yield return new WaitForSeconds(roamChangeDirFloat);
        }
    }
    private Vector2 GetRoamPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    
    void Update()
    {
        
    }
}
