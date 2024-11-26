using UnityEngine;

public class Pellets : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Pacman")
        {
            gameObject.SetActive(false);
        }
        
    }
}

