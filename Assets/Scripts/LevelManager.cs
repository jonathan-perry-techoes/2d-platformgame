using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject currentCheckpoint;

    private PlayerController player;

    // Particle Game Objects
    public GameObject deathParticle;
    public GameObject respawnParticle;

    public float respawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RespawnPlayer()
    {
        StartCoroutine("RespawnPlayerCo");
    }

    public IEnumerator RespawnPlayerCo()
    {
        // Launch the blood death particle system anim
        Instantiate(deathParticle, player.transform.position, player.transform.rotation);
        // Disable the player
        player.enabled = false;
        player.GetComponent<Renderer>().enabled = false;
        // Put a pause between death and respawn
        yield return new WaitForSeconds(respawnDelay);
        // Re-enable both the player and renderer
        player.enabled = true;
        player.GetComponent<Renderer>().enabled = true;
        // Respawn player to first checkpoint
        player.transform.position = currentCheckpoint.transform.position;
        Instantiate(respawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);

    }
}
