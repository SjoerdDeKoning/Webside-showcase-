using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteRenderer))]
public class Planet : MonoBehaviour
{
    public float planetSize;
    public SpriteRenderer spriteRenderer;
    public float rotateSpeed;
    public void Start()
    {
        rotateSpeed = Random.value; // gets random rotate speed

        gameObject.AddComponent<PolygonCollider2D>();
        var position = transform.position;
        transform.position = new Vector3(position.x, position.y, 0);

    }

    private void Update()
    {
        transform.Rotate(0, 0, 0.005f * rotateSpeed); // rotate the planet
    }
}
