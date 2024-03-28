using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 Offset;
    [SerializeField] private float FollowSpeed;
    [SerializeField] private float Deacceleration;


    Transform Player;
    float CurrentSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameManager.Instance.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.GameState == GameState.Pause) { return; }

        if(Player)
        {
            if(GameManager.Instance.GameState == GameState.Playing ||
               GameManager.Instance.GameState == GameState.Starting)
            {
                //Camera Moving With Player in Play Mode
                Vector3 NextPosition = Vector3.Lerp(transform.position, Player.position + Offset,FollowSpeed * Time.deltaTime);
                CurrentSpeed = (NextPosition.z - transform.position.z) / Time.deltaTime;
                transform.position = NextPosition;
            }
            else
            {
                //Camera Slowly Coming To Stop if Game Failes
                if (CurrentSpeed > 0)
                    CurrentSpeed -= Deacceleration * Time.deltaTime;
                else
                    CurrentSpeed = 0;

                transform.position += new Vector3(0,0,1) * CurrentSpeed * Time.deltaTime;
            }
        }
    }
}
