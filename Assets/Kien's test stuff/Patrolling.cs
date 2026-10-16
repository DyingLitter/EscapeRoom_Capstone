using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class Patrolling : MonoBehaviour
{
    public GameObject player;
    public PlayerController playerController;
    public Animator deathScreen;
    public GameObject spawnPoint;
    public GameObject momSpawnPoint;
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 10f * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.rb.constraints = RigidbodyConstraints.FreezeAll;
            playerController.enabled = false;
            deathScreen.gameObject.SetActive(true);
            deathScreen.SetTrigger("FadeIn");
            Invoke("SpawnPlayer", 2f);
        }
    }
    public void SpawnPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
