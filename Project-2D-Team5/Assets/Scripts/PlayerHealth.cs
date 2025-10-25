using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    //-----------------------------------------------------------------------------------------------------------------------------\/
    //Edited by Kai
    //void sceneOver()
    //{
    //    string currentSceneName = SceneManager.GetActiveScene().name;
    //    SceneManager.LoadScene(currentSceneName);
    //}
    //-----------------------------------------------------------------------------------------------------------------------------/\
    //-----------------------------------------------------------------------------------------------------------------------------\/
    //Added by Kai
    [Header("UI Panels")]
    public GameObject losePanel;
    //-----------------------------------------------------------------------------------------------------------------------------/\

    public int startingHealth = 3;
    public GameObject heartOne;
    public GameObject heartTwo;
    public GameObject heartThree;
    public AudioClip playerhurtSFX;

    private AudioSource audioSource;
    private int currentHealth;

    public float iFrameDuration = 1f; 
    private bool isInvincible = false;

    //-----------------------------------------------------------------------------------------------------------------------------\/
    //Added by Kai
    public float damageInterval = 1f;
    private float damageTimer = 0f;
    private bool touchingHazard = false;

    private void Update()
    {
        if (touchingHazard && !isInvincible)
        {
            damageTimer += Time.deltaTime;

            if (damageTimer >= damageInterval)
            {
                TakeDamage(1);
                damageTimer = 0f;
            }
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------------/\

    private void Awake()
    {
        currentHealth = startingHealth;
        audioSource = GetComponent<AudioSource>();
    }

    //-----------------------------------------------------------------------------------------------------------------------------\/
    //Added by Kai
    private void OnTriggerEnter2D(Collider2D other)
    {
        //-------------------------------------------------------------------------------------------------------------------------||
        //Edited by Kai
        //if (other.CompareTag("Hazard") && !isInvincible)
        //{
        //    TakeDamage(1);
        //}
        //-------------------------------------------------------------------------------------------------------------------------||

        if (other.CompareTag("Hazard"))
        {
            touchingHazard = true;
            damageTimer = damageInterval;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hazard"))
        {
            touchingHazard = false;
            damageTimer = 0f;
        }
        if (other.CompareTag("Heart"))
        {
            if (currentHealth >= 3)
            {
                //nothing
            }
            if (currentHealth == 2)
            {
                heartThree.SetActive(true);
                currentHealth++;
                Destroy(other.gameObject);
            }
            if (currentHealth == 1)
            {
                heartTwo.SetActive(true);
                currentHealth++;
                Destroy(other.gameObject);
            }
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------------/\

    public void TakeDamage(int amount)
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
            //----------------------------------------------------------------------------------------------------------------------\/
            //Edited by Kai
            //Invoke("sceneOver", 2);
            //----------------------------------------------------------------------------------------------------------------------||
            //added by Kai
            Time.timeScale = 0f;
            if (losePanel != null)
                losePanel.SetActive(true);
            //----------------------------------------------------------------------------------------------------------------------/\
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
