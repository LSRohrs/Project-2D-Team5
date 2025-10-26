using UnityEngine;
public class SimpleLever : MonoBehaviour 
{ 
    public GameObject trapdoor;
    public AudioClip leverSFX;
    private Animator anim;
    private AudioSource audioSource;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D key) 
    { 
        if (trapdoor != null) 
        { 
            trapdoor.SetActive(false);
            audioSource.PlayOneShot(leverSFX);
            anim.SetTrigger("Lever");
        } 
    } 
}