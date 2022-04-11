using UnityEngine;
using Random = UnityEngine.Random;

public class PlanetSpawner : MonoBehaviour
{
    public static PlanetSpawner Instance;
    
    [Header("Planet settings")] 
    [SerializeField] private float maxPlanetSize; // maximum size of a planet
    [SerializeField] private float minPlanetSize; // minimum size of a planet
    [SerializeField] private Sprite[] planetsSprites; // all the sprites that a planets can take 

    [Header("Spawn checks settings")]
    public float checkRadius; // The radius to check for other planets
    public GameObject planetPrefab;

    public GameObject alien;
    private Camera _camera; // the main camera
    private float _spawnRadiusScaleSize;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    private void Start()
    {
        _camera = Camera.main;  // get the main camera 
        if (_camera != null) _spawnRadiusScaleSize = _camera.ViewportToWorldPoint(new Vector3(1, 1, 0)).magnitude * 2f;
    }

    private void SpawnPlanet()
    {
        float randomInt = Random.Range(minPlanetSize, maxPlanetSize);
        Vector2 planetSize = new Vector2(randomInt,randomInt); // Get a random size to the planet
        Sprite planetSprite = planetsSprites[Random.Range(0,planetsSprites.Length)];  // Gets a random sprite for the planet 
        
        Vector2 rPos = Random.insideUnitCircle * _spawnRadiusScaleSize;
        Vector3 spawnPosition = _camera.transform.position + new Vector3(rPos.x ,rPos.y,0);

        if (!IsInsideCamera(spawnPosition)) 
        {
            if(HasRoom(spawnPosition))
            {   // spawn's the planets 
                GameObject newPlant = Instantiate(planetPrefab, spawnPosition, Quaternion.Euler(Vector3.zero),transform);
                newPlant.GetComponent<SpriteRenderer>().sprite = planetSprite;
                newPlant.transform.localScale= planetSize;
                if (Random.Range(0,35) == 2)
                {
                    Instantiate(alien,spawnPosition + Vector3.right,Quaternion.Euler(Vector3.zero));
                }
            }
        }
    }
    /// <summary>
    /// checks if the planet is not to close to a different planets 
    /// </summary>
    /// <param name="position">The position of were you want to do the check</param>
    /// <returns></returns>
    private bool HasRoom(Vector3 position)
    {
        Collider2D[] colliderArry = Physics2D.OverlapCircleAll(position, checkRadius);
        foreach (Collider2D collider2D in colliderArry)
        {
            if (collider2D.CompareTag("Planet"))
            {
                return false;
            }

            if (collider2D.CompareTag("Alien"))
            {
                return false;
            }
        }

        return true;
    }
    /// <summary>
    /// checks if the planet is the camera's view 
    /// </summary>
    /// <param name="position">The position of were you want to do the check</param>
    /// <returns></returns>
    private bool IsInsideCamera(Vector3 position)
    {
        Vector3 screenPos = _camera.WorldToViewportPoint(position);

        if (screenPos.x is < 1.1f and > -0.1f)
            if (screenPos.y is < 1.1f and > -0.1f)
                return true;

        return false;
    }
    private void Update()
    {
        // ones per frame we try to spawn a new planet 
        SpawnPlanet();
    }
}
