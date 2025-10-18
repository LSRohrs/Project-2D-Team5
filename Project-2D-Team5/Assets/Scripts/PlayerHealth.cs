using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float startingHealth;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void enemyDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            //player damaged
        }
        else
        {
            //player dead
        }
    }
}
