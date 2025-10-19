using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D body;
    private Animator animator;
    private bool grounded;
    private float horizontalInput;

    //audio
    public AudioClip jumpSFX;
    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(2f, 2f, 2f);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-2f, 2f, 2f);

        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        //animator parameters
        animator.SetBool("walk", horizontalInput != 0);
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        audioSource.clip = jumpSFX;
        audioSource.Play();
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Hazard")
            grounded = true;
    }

    private void OnTriggerEnter2D(Collider2D key)
    {
        if (key.gameObject.tag == "Key")
           SceneManager.LoadScene("Level Two");
    }


    public bool canAttack()
    {
        return horizontalInput == 0 && grounded;
    }
}
