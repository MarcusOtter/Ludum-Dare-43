using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(EnvironmentVariables.PlayerTag))
        {
            other.GetComponent<ShipController>().Sacrificer.ModifyHitpoints(1);
            Destroy(gameObject);
        }
    }
}
