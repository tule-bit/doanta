using UnityEngine;

public class SlashAnime : MonoBehaviour
{
    private ParticleSystem ps;
    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if(ps && !ps.IsAlive())
        {
            Destoyself();
        }
    }
    public void Destoyself() { 
       Destroy(gameObject);
    }

}
