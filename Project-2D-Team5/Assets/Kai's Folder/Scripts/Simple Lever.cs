using UnityEngine;
public class SimpleLever : MonoBehaviour 
{ 
    public GameObject trapdoor; 
    
    private void OnTriggerEnter2D(Collider2D key) 
    { 
        if (trapdoor != null) 
        { 
            trapdoor.SetActive(false); 
        } 
    } 
}