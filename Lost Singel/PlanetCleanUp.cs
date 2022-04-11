using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlanetCleanUp : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Planet"))
        {
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Asteroid"))
        {
            Destroy(other.gameObject);
        }
    }
    
}
