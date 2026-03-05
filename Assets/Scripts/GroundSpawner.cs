//using UnityEngine;
//using System.Collections.Generic;

//public class GroundSpawner : MonoBehaviour
//{
//    [Header("Ground")]
//    public GameObject groundPrefab;      // Ground segment prefab
//    public Transform player;             // Player cube
//    public int numberOfSegments = 5;     // Active segments at a time
//    public float segmentLength = 50f;    // Z length of each segment

//    [Header("Obstacles & Collectables")]
//    public GameObject blueObstacle;
//    public GameObject greenObstacle;
//    public GameObject bigObstacle;
//    public GameObject smallObstacle;
//    public GameObject redCube;           // Collectable

//    [Header("Lane Positions")]
//    public float leftX = -3f;            // Left lane X position
//    public float centerX = 0f;           // Center lane X position
//    public float rightX = 3f;            // Right lane X position

//    private List<GameObject> activeGrounds = new List<GameObject>();        // Track active ground segments
//    private int patternIndex = 0;        // Lane swap pattern index

//    // Pool for obstacles (we reuse objects instead of destroying)
//    private List<GameObject> obstaclePool = new List<GameObject>();

//    void Start()
//    {
//        // Spawn initial ground segments
//        for (int i = 0; i < numberOfSegments; i++)
//        {
//            float segmentZ = i * segmentLength;                           // Z position of this segment
//            GameObject segment = Instantiate(groundPrefab, new Vector3(0, 0, segmentZ), Quaternion.identity);
//            activeGrounds.Add(segment);

//            SwapObstacleLanes();                                         // Decide lane positions for this segment
//            SpawnObstacles(segmentZ, segment);                            // Spawn obstacles for this segment
//        }
//    }

//    void Update()
//    {
//        // Check if player passed the first segment
//        if (player.position.z > activeGrounds[0].transform.position.z + segmentLength)
//        {
//            GameObject firstSegment = activeGrounds[0];
//            activeGrounds.RemoveAt(0);

//            // Instead of destroying obstacles, just hide them
//            SegmentObstacles segObs = firstSegment.GetComponent<SegmentObstacles>();
//            if (segObs != null)
//            {
//                foreach (GameObject obs in segObs.obstaclesList)
//                    obs.SetActive(false);       // Hide obstacle instead of Destroy
//            }

//            // Move segment in front of last segment
//            GameObject lastSegment = activeGrounds[activeGrounds.Count - 1];
//            float newZ = lastSegment.transform.position.z + segmentLength;
//            firstSegment.transform.position = new Vector3(0, 0, newZ);
//            activeGrounds.Add(firstSegment);

//            SwapObstacleLanes();                                         // Swap lanes for new segment
//            SpawnObstacles(newZ, firstSegment);                           // Spawn/reuse obstacles for this segment
//        }
//    }

//    // Rotate lane positions: left -> center -> right -> left
//    void SwapObstacleLanes()
//    {
//        float temp = leftX;
//        leftX = centerX;
//        centerX = rightX;
//        rightX = temp;
//    }

//    // Spawn or reuse obstacles on a segment
//    void SpawnObstacles(float segmentZ, GameObject segment)
//    {
//        List<GameObject> segmentObstacles = new List<GameObject>();  // Track obstacles for this segment

//        float startZ = segmentZ + 5f;                                 // Start Z a little ahead of segment start
//        int obstaclesPerLane = 5;                                     // Obstacles per lane
//        float gap = (segmentLength - 10f) / obstaclesPerLane;         // Gap between obstacles

//        for (int i = 0; i < obstaclesPerLane; i++)
//        {
//            float z = startZ + i * gap;

//            // Reuse obstacles from pool or create new if pool is empty
//            GameObject blue = GetObstacleFromPool(blueObstacle, new Vector3(leftX, 1, z));
//            GameObject red = GetObstacleFromPool(redCube, new Vector3(centerX, 1, z));
//            GameObject green = GetObstacleFromPool(greenObstacle, new Vector3(rightX, 1, z));

//            segmentObstacles.Add(blue);
//            segmentObstacles.Add(red);
//            segmentObstacles.Add(green);
//        }

//        // End of segment obstacles: either 2 small or 1 big
//        float endZ = segmentZ + segmentLength - 5f;
//        if (Random.value > 0.5f)
//        {
//            GameObject small1 = GetObstacleFromPool(smallObstacle, new Vector3(leftX, 1, endZ));
//            GameObject small2 = GetObstacleFromPool(smallObstacle, new Vector3(rightX, 1, endZ));
//            segmentObstacles.Add(small1);
//            segmentObstacles.Add(small2);
//        }
//        else
//        {
//            GameObject big = GetObstacleFromPool(bigObstacle, new Vector3(centerX, 1, endZ));
//            segmentObstacles.Add(big);
//        }

//        // Add SegmentObstacles component if not exists
//        SegmentObstacles segObs = segment.GetComponent<SegmentObstacles>();
//        if (segObs == null)
//            segObs = segment.AddComponent<SegmentObstacles>();

//        segObs.obstaclesList = segmentObstacles;                       // Assign obstacles list to segment
//    }

//    // Get an obstacle from pool or create if none available
//    GameObject GetObstacleFromPool(GameObject prefab, Vector3 position)
//    {
//        foreach (GameObject obs in obstaclePool)
//        {
//            if (!obs.activeInHierarchy && obs.name.Contains(prefab.name))
//            {
//                obs.transform.position = position;
//                obs.SetActive(true);     // Reactivate the obstacle
//                return obs;
//            }
//        }

//        // If none available, instantiate new
//        GameObject newObs = Instantiate(prefab, position, Quaternion.identity);
//        obstaclePool.Add(newObs);
//        return newObs;
//    }
//}

//// Helper component to track obstacles for each ground segment
//public class SegmentObstacles : MonoBehaviour
//{
//    public List<GameObject> obstaclesList = new List<GameObject>();
//}


using UnityEngine;
using System.Collections.Generic;

public class GroundSpawner : MonoBehaviour
{
    [Header("Ground")]
    public GameObject groundPrefab;      // Ground segment prefab
    public Transform player;             // Player cube
    public int numberOfSegments = 3;     // Active segments at a time
    public float segmentLength = 50f;    // Z length of each segment

    [Header("Obstacles & Collectables")]
    public GameObject blueObstacle;
    public GameObject greenObstacle;
    public GameObject bigObstacle;
    public GameObject smallObstacle;
    public GameObject redCube;           // Collectable

    [Header("Lane Positions")]
    public float leftX = -3f;            // Left lane X position
    public float centerX = 0f;           // Center lane X position
    public float rightX = 3f;            // Right lane X position

    private List<GameObject> activeGrounds = new List<GameObject>();       // Track active ground segments
    private int patternIndex = 0;        // Lane swap pattern index

    void Start()
    {
        // Spawn initial ground segments
        for (int i = 0; i < numberOfSegments; i++)
        {
            float segmentZ = i * segmentLength;                           // Z position of this segment
            GameObject segment = Instantiate(groundPrefab, new Vector3(0, 0, segmentZ), Quaternion.identity);
            activeGrounds.Add(segment);

            SwapObstacleLanes();                                         // Decide lane positions for this segment
            SpawnObstacles(segmentZ, segment);                            // Spawn obstacles on this segment
        }
    }

    void Update()
    {
        // Check if player passed the first segment
        if (player.position.z > activeGrounds[0].transform.position.z + segmentLength)
        {
            GameObject firstSegment = activeGrounds[0];
            activeGrounds.RemoveAt(0);

            // Destroy its obstacles before moving
            SegmentObstacles segObs = firstSegment.GetComponent<SegmentObstacles>();
            if (segObs != null)
            {
                foreach (GameObject obs in segObs.obstaclesList)
                    Destroy(obs);
                segObs.obstaclesList.Clear();
            }

            // Move segment in front of last segment
            GameObject lastSegment = activeGrounds[activeGrounds.Count - 1];
            float newZ = lastSegment.transform.position.z + segmentLength;
            firstSegment.transform.position = new Vector3(0, 0, newZ);
            activeGrounds.Add(firstSegment);

            SwapObstacleLanes();                                         // Swap lanes for new segment
            SpawnObstacles(newZ, firstSegment);                           // Spawn obstacles on this segment
        }
    }

    // SwapObstacleLanes: rotates lanes for obstacle pattern
    void SwapObstacleLanes()
    {
        // Rotate lane positions: left -> center -> right -> left
        float temp = leftX;
        leftX = centerX;
        centerX = rightX;
        rightX = temp;
    }

    // SpawnObstacles: instantiate obstacles and red cubes for given segment
    void SpawnObstacles(float segmentZ, GameObject segment)
    {
        List<GameObject> segmentObstacles = new List<GameObject>();  // Track obstacles for this segment

        float startZ = segmentZ + 5f;                                    // Start Z a little ahead of segment start
        int obstaclesPerLane = 5;                                        // Obstacles per lane
        float gap = (segmentLength - 10f) / obstaclesPerLane;            // Gap between obstacles

        for (int i = 0; i < obstaclesPerLane; i++)
        {
            float z = startZ + i * gap;

            // Instantiate obstacles in each lane
            GameObject blue = Instantiate(blueObstacle, new Vector3(leftX, 1, z), Quaternion.identity);
            GameObject red = Instantiate(redCube, new Vector3(centerX, 1, z), Quaternion.identity);
            GameObject green = Instantiate(greenObstacle, new Vector3(rightX, 1, z), Quaternion.identity);

            // Add to this segment list
            segmentObstacles.Add(blue);
            segmentObstacles.Add(red);
            segmentObstacles.Add(green);
        }

        // End of segment obstacles: either 2 small or 1 big
        float endZ = segmentZ + segmentLength - 5f;
        if (Random.value > 0.5f)
        {
            GameObject small1 = Instantiate(smallObstacle, new Vector3(leftX, 1, endZ), Quaternion.identity);
            GameObject small2 = Instantiate(smallObstacle, new Vector3(rightX, 1, endZ), Quaternion.identity);
            segmentObstacles.Add(small1);
            segmentObstacles.Add(small2);
        }
        else
        {
            GameObject big = Instantiate(bigObstacle, new Vector3(centerX, 1, endZ), Quaternion.identity);
            segmentObstacles.Add(big);
        }

        // Add SegmentObstacles component if not exists
        SegmentObstacles segObs = segment.GetComponent<SegmentObstacles>();
        if (segObs == null)
            segObs = segment.AddComponent<SegmentObstacles>();

        segObs.obstaclesList = segmentObstacles;                          // Assign obstacles list to segment
    }
}

// Helper component to track obstacles for each ground segment
public class SegmentObstacles : MonoBehaviour
{
    public List<GameObject> obstaclesList = new List<GameObject>();
}



//using UnityEngine;
//using System.Collections.Generic;

//public class GroundSpawner : MonoBehaviour
//{
//    [Header("Ground")]
//    public GameObject groundPrefab;      // Drag your GroundSegment prefab here
//    public Transform player;             // Drag your player cube here
//    public int numberOfSegments = 5;     // How many segments exist at a time
//    public float segmentLength = 50f;    // Z-length of each segment

//    private List<GameObject> activeGrounds = new List<GameObject>();
//    // this list will keep track of all ground segment in the scene

//    void Start()
//    {


//        // Initialize all segments and place them in front of the player
//        for (int i = 0; i < numberOfSegments; i++)
//        {
//            GameObject segment = Instantiate(groundPrefab, new Vector3(0, 0, i * segmentLength), Quaternion.identity);
//            activeGrounds.Add(segment);
//        }
//    }

//    void Update()
//    {
//        // Check if player has passed the first ground segment
//        if (player.position.z > activeGrounds[0].transform.position.z + segmentLength)
//        {
//            // Take the first segment
//            GameObject firstSegment = activeGrounds[0];
//            activeGrounds.RemoveAt(0);

//            // Move it in front of the last segment
//            GameObject lastSegment = activeGrounds[activeGrounds.Count - 1];
//            firstSegment.transform.position = lastSegment.transform.position + new Vector3(0, 0, segmentLength);

//            // Add it back to the end of the list
//            activeGrounds.Add(firstSegment);
//        }
//    }
//}

//using System.Collections.Generic;
//using System.Diagnostics;
//using UnityEngine;
//using UnityEngine.Pool;

//public class GroundSpawner : MonoBehaviour
//{
//    [Header("Ground Settings")]
//    public GameObject groundPrefab;      // Your ground segment prefab
//    public int numberOfSegments = 5;     // How many segments exist at a time
//    public float segmentLength = 50f;    // Length of each segment in Z

//    [Header("Player")]
//    public Transform player;             // Your moving player cube

//    // List to track active ground segments
//    private List<GameObject> activeGrounds = new List<GameObject>();

//    // Object Pool for ground segments
//    private ObjectPool<GameObject> groundPool;

//    void Awake()
//    {
//        // Initialize the pool
//        groundPool = new ObjectPool<GameObject>(
//            createFunc: () =>
//            {
//                GameObject obj = Instantiate(groundPrefab, transform);
//                obj.SetActive(false);
//                return obj;
//            },
//            actionOnGet: obj => obj.SetActive(true),
//            actionOnRelease: obj => obj.SetActive(false),
//            actionOnDestroy: obj => Destroy(obj),
//            Debug.Log(other.gameObject.name);
//    collectionCheck: false,
//            defaultCapacity: numberOfSegments,
//            maxSize: numberOfSegments * 2
//        );
//    }

//    void Start()
//    {
//        // Spawn initial ground segments
//        for (int i = 0; i < numberOfSegments; i++)
//        {
//            GameObject segment = groundPool.Get();
//            segment.transform.position = new Vector3(0, 0, i * segmentLength);
//            activeGrounds.Add(segment);
//        }
//    }

//    void Update()
//    {
//        // Check if player passed the first ground segment
//        if (player.position.z > activeGrounds[0].transform.position.z + segmentLength)
//        {
//            // Recycle the first segment
//            GameObject firstSegment = activeGrounds[0];
//            activeGrounds.RemoveAt(0);

//            // Move it in front of the last segment
//            GameObject lastSegment = activeGrounds[activeGrounds.Count - 1];
//            firstSegment.transform.position = lastSegment.transform.position + new Vector3(0, 0, segmentLength);

//            // Add it back to the list
//            activeGrounds.Add(firstSegment);
//        }
//    }
//}




