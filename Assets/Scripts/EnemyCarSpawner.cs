using UnityEngine;

public class EnemyCarSpawner : MonoBehaviour
{
    [SerializeField] float BufferZone = 80;
    [SerializeField] int SpawnRatePerSecond = 1;
    [SerializeField, Range(0, 1),Tooltip("How often shall we neglect spawn rate rule, 0 being not all and 1 mean every Time.")]
    float Randomness = .5f;


    ObjectPooler objectPooler;
    Transform Player;

    float LastSpawnTime = 0;
    float TimeBetweenTwoSpawn;
    int LineOfLastSpawn = 0;
    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        Player = GameManager.Instance.GetPlayer();
        TimeBetweenTwoSpawn = 1 / SpawnRatePerSecond;
        Player = GameManager.Instance.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.GameState == GameState.Starting || GameManager.Instance.GameState == GameState.Pause)
        {
            return;
        }
        if(LastSpawnTime > TimeBetweenTwoSpawn)
        {
            if(Random.Range(0f, 1f) > Randomness)
            {
                int PossibleLineOfSpawn = Random.Range(0, 3);
                if(PossibleLineOfSpawn != LineOfLastSpawn)
                    LineOfLastSpawn = PossibleLineOfSpawn;
                else
                {
                    PossibleLineOfSpawn += 1;
                    PossibleLineOfSpawn %= 4;

                    LineOfLastSpawn = PossibleLineOfSpawn;
                }
                objectPooler.SpawnFromPool("EnemyCar", new Vector3(2.6f * LineOfLastSpawn, 0.2614193f, Player.position.z + BufferZone), Quaternion.identity);
            }
            LastSpawnTime = 0;
        }
        LastSpawnTime += Time.deltaTime;
    }
}
