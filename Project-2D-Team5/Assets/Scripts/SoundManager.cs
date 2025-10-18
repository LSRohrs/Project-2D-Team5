using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
}
