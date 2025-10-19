using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    void sceneOver()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public int startingHealth = 3;
    public GameObject heartOne;
    public GameObject heartTwo;
    public GameObject heartThree;
    public AudioClip playerhurtSFX;

    private AudioSource audioSource;
    private int currentHealth;

    public float iFrameDuration = 1f; 
    private bool isInvincible = false;

    private void Awake()
    {
        currentHealth = startingHealth;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hazard") && !isInvincible)
        {
            TakeDamage(1);
        }
    }

    private void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 2)
            heartThree.SetActive(false);
        audioSource.clip = playerhurtSFX;
        audioSource.Play();

        if (currentHealth <= 1)
            heartTwo.SetActive(false);
        audioSource.clip = playerhurtSFX;
        audioSource.Play();

        if (currentHealth <= 0)
        {
            heartOne.SetActive(false);
            audioSource.clip = playerhurtSFX;
            audioSource.Play();
            Debug.Log("Player died!");
            Invoke("sceneOver", 2);
        }

        StartCoroutine(IFrames());
    }

    private System.Collections.IEnumerator IFrames()
    {
        isInvincible = true;
        yield return new WaitForSeconds(iFrameDuration);
        isInvincible = false;
    }
}
