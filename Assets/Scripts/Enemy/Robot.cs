using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    FirstPersonController player;
    NavMeshAgent agent;

    const string PLAYER_TAG = "Player";

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!player) return;
        player = FindFirstObjectByType<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            EneemyHealth eneemyHealth = GetComponent<EneemyHealth>();
            eneemyHealth.SelfDestruct();
        }
    }
}
