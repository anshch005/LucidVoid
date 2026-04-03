using UnityEngine;
using System.Collections;

public class HardRespawn : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;//reference to respawn point
    [SerializeField] private float respawnDelay = 0.5f;//delay before respawn(will be helpful in animation)

    private Rigidbody2D rb;
    private bool isRespawning;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetRespawnPoint(Transform newPoint)
    {
        respawnPoint = newPoint;
    }

    public void Respawn()
    {
        if (isRespawning) return;
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine() //IEnumerator is used to create a Coroutine.
    //coroutine is basically: A function that can pause, wait, and then continue later.
    {
        isRespawning = true;

        // Stop movement
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;

        yield return new WaitForSeconds(respawnDelay);

        // Move to respawn point
        transform.position = respawnPoint.position;

        rb.simulated = true;
        isRespawning = false;
    }
}