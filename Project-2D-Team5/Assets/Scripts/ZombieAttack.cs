using UnityEngine;

public class ZombieAttack : MonoBehaviour
{

    public float attackCooldown;
    public float range;
    public float colldierDistance;
    public BoxCollider2D boxCollider;
    private int damage = 1;
    private float cooldownTimer = Mathf.Infinity;
    public LayerMask playerLayer;

    private PlayerHealth health;


    private Animator anim;

    public AudioClip zombieAttackSFX;
    private AudioSource audioSource;

    private ZombieMovement zombiePatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        zombiePatrol = GetComponentInParent<ZombieMovement>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        
        if (targetPlayer())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("MeleeAttack");
                audioSource.clip = zombieAttackSFX;
                audioSource.Play();
            }
        }

        if (zombiePatrol != null)
        {
            zombiePatrol.enabled = !targetPlayer();
        }
    }

    private bool targetPlayer()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colldierDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            health = hit.transform.GetComponent<PlayerHealth>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colldierDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void damagePlayer()
    {
        if (targetPlayer() && health != null)
        {
            PlayerAttacks playerAttacks = health.GetComponent<PlayerAttacks>();

            if (playerAttacks == null || !playerAttacks.blockIFrame)
            {
                health.TakeDamage(damage);
            }
            // Optional: else play a block sound or animation
        }
    }
}
