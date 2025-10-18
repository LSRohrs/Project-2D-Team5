using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public float attackcooldown;
    private Animator anim;
    private PlayerController playermovement;
    private float cooldownTimer = Mathf.Infinity;

    //audio
    public AudioClip swordSFX;
    private AudioSource audioSource;
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
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        audioSource.clip = swordSFX;
        audioSource.Play();
        cooldownTimer = 0;
    }
}
