using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject SlashAnimPre;
    [SerializeField] private Transform SlashPoint;
    [SerializeField] private Transform WeaponCollider;
    [SerializeField] private float swordAttackCd = .5f;

    private PlayerCOntroll playerControll;
    private Animator anim;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;
    private bool attackButtonDown,isAttacking = false;

    private GameObject slashAnim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        anim = GetComponentInChildren<Animator>();
        playerControll = new PlayerCOntroll();
    }

    // Update is called once per frame
    private void OnEnable()
    {
        playerControll.Enable();

    }
    private void Start()
    {
        playerControll.Combat.Attack.started += _ => StartAttacking();
        playerControll.Combat.Attack.canceled += _ => StopAttacking();
    }
    private void StartAttacking()
    {
        attackButtonDown = true;
    }
    private void StopAttacking()
    {
        attackButtonDown = false;
    }
    private void Update()
    {
        MouseFollowWithOffset();
        Attack();
    }
    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        if (mousePos.x < playerScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            WeaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);

        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            WeaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void Attack()
    {
        if (attackButtonDown && !isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("Attack");
            WeaponCollider.gameObject.SetActive(true);
            slashAnim = Instantiate(SlashAnimPre, SlashPoint.position, Quaternion.identity);
            slashAnim.transform.parent = this.transform.parent;
            StartCoroutine(AttackCdRoutime());
        }
    }
    private IEnumerator AttackCdRoutime()
    {
                yield return new WaitForSeconds(swordAttackCd);
        isAttacking = false;

    }

    public void DoneAttackingAnimEvent()
    {
        WeaponCollider.gameObject.SetActive(false);
    }
    public void SwingUpFlipAnimEvent()
    {
       slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180,0,0);
        if (playerController.FacingLeft)
        {
          slashAnim.GetComponent<SpriteRenderer>().flipX = true;

        }
    }
    public void SwingDownFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
