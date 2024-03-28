using UnityEngine;

public class ProceduralRoadGenerator : MonoBehaviour
{
    [SerializeField] float BufferZone = 80;
    [SerializeField] int NumberOfConcurrentParts = 10;


    float DistanceBeingCovered = 0;

    ObjectPooler objectPooler;
    Transform Cam;
    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        Cam = Camera.main.transform;

        for(int i = 0; i < NumberOfConcurrentParts; i++)
        {
            objectPooler.SpawnFromPool("Road", new Vector3(3.75f,0, DistanceBeingCovered), Quaternion.identity);
            DistanceBeingCovered += 20; // 20 is the Length of one Road Part
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Cam.position.z > DistanceBeingCovered - BufferZone)
        {
            objectPooler.SpawnFromPool("Road", new Vector3(3.75f, 0, DistanceBeingCovered), Quaternion.identity);
            DistanceBeingCovered += 20; // 20 is the Length of one Road Part
        }
    }
}
