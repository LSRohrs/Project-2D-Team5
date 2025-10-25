using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public float attackcooldown;
    private Animator anim;
    private PlayerController playermovement;
    private float cooldownTimer = Mathf.Infinity;
    public bool blockIFrame = false;

    //audio
    public AudioClip swordSFX;
    public AudioClip shieldSFX;
    private AudioSource audioSource;

    public float attackRange = 1f;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playermovement = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackcooldown && playermovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(1) && cooldownTimer > attackcooldown && playermovement.canAttack())
            startBlock();

        if (Input.GetMouseButtonUp(1))
        {
            StopBlock();
        }
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        audioSource.clip = swordSFX;
        audioSource.Play();
        cooldownTimer = 0;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Destroy(enemy.gameObject); 
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void startBlock()
    {
        anim.SetBool("block", true);
        audioSource.clip = shieldSFX;
        audioSource.Play();
        cooldownTimer = 0;

        blockIFrame = true;
    }

    private void StopBlock()
    {
        anim.SetBool("block", false);
        blockIFrame = false;
    }

    private void DisableBlockIFrame()
    {
        blockIFrame = false;
    }
}
