using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ConlenySpawner : MonoBehaviour
{
    [Header("Conleny Settings")]
    public Sprite[] conlenySprites;
    public float maxExtraRadius;
    public float minSpawnRadius;
    public GameObject conlenyPerfab;

    private Camera _camera; // the main camera
    private void Start()
    {
        _camera = Camera.main;  // get the main camera 
        SpawnConleny();
    }

    public void SpawnConleny()
    {
        FindRandomSpawn();
        Sprite conlenySprite = conlenySprites[Random.Range(0, conlenySprites.Length)];  // Gets a random sprite for the coleny

        GameObject newConleny = Instantiate(conlenyPerfab, transform.position, quaternion.Euler(Vector3.zero));
        newConleny.GetComponent<SpriteRenderer>().sprite = conlenySprite;
        newConleny.transform.localScale = new Vector2(2.5f, 2.5f);
    }

    /// <summary>
    /// turns a random angel before going forward a random amount
    /// </summary>
    private void FindRandomSpawn()
    {
        transform.rotation = quaternion.Euler(0, 0, Random.Range(0, 360));
        transform.position += transform.right * (minSpawnRadius + Random.Range(0, maxExtraRadius));
    }
}
