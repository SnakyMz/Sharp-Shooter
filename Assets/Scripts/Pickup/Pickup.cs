using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 90f;

    const string PLAYER_TAG = "Player";

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
            OnPickup(activeWeapon);
            // Destroy the pickup after it has been picked up
            Destroy(gameObject); 
        }
    }

    abstract protected void OnPickup(ActiveWeapon activeWeapon);
}
