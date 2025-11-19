using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteflashmat;
    [SerializeField] private float restoreDefaultMatime = 0.2f;
    private Material defaultMat;
    private SpriteRenderer sprit;
    private void Awake()
    {
        
        sprit = GetComponent<SpriteRenderer>();
        defaultMat = sprit.material;
    }
    public float GetRestoreDefaultMatime()
    {
        return restoreDefaultMatime;
    }
    public IEnumerator FlashRouTine()
    {
        sprit.material = whiteflashmat;
        yield return new WaitForSeconds(restoreDefaultMatime);
        sprit.material = defaultMat;
        
    }
}
