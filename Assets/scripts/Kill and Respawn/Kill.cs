using UnityEngine;

public class Kill : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))//checks for player tag
        {
            other.GetComponent<HardRespawn>().Respawn();//calls respawn function from hardrespawn script
        }
    }
}
