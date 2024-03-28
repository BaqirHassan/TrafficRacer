using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Text CurrentSpeedText;

    [SerializeField] float MaxSpeed = 30f;
    [SerializeField] float MinSpeed = 5f;
    [SerializeField] float Acceleration = 1;
    [SerializeField] float BreakPower = 3;
    [SerializeField] float TimeToTurn = .5f;

    float CurrentSpeed;
    float TimeSinceLastTurn = .6f;
    bool Accelerating = false;
    bool Breaking = false;
    public int currentLine = 1;

    Coroutine GoLeftCoTemp, GoRightCoTemp;
    
    // Start is called before the first frame update
    void Start()
    {
        CurrentSpeed = MinSpeed;
        currentLine = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.GameState == GameState.Playing || GameManager.Instance.GameState == GameState.Starting)
        {
            if (GameManager.Instance.GameState == GameState.Playing)
            {
                if(Breaking)
                {
                    if (CurrentSpeed > MinSpeed)
                    {
                        CurrentSpeed -= BreakPower * Time.deltaTime;
                        if (CurrentSpeed < MinSpeed)
                        {
                            CurrentSpeed = MinSpeed;
                        }
                    }
                }
                else if(Accelerating)
                {
                    if (CurrentSpeed < MaxSpeed)
                    {
                        CurrentSpeed += Acceleration * Time.deltaTime;
                        if (CurrentSpeed > MaxSpeed)
                        {
                            CurrentSpeed = MaxSpeed;
                        }
                    }
                }
                else
                {
                    if (CurrentSpeed > MinSpeed)
                    {
                        CurrentSpeed -= Acceleration * Time.deltaTime;
                        if (CurrentSpeed < MinSpeed)
                        {
                            CurrentSpeed = MinSpeed;
                        }
                    }
                }

                TimeSinceLastTurn += Time.deltaTime;
            }

            transform.position += transform.forward * CurrentSpeed * Time.deltaTime;
            CurrentSpeedText.text = "Speed: " + ((int)CurrentSpeed).ToString();
        }
    }

    IEnumerator GoLeftCo()
    {
        float TimePassed = 0;
        
        while (TimePassed < 1)
        {
            transform.position = Vector3.Lerp(transform.position,
                                               new Vector3(2.6f * (currentLine - 2), 0.2614193f, transform.position.z), TimePassed);
            TimePassed += Time.deltaTime / TimeToTurn;
            yield return null;
        }
        transform.position = new Vector3(2.6f * (currentLine - 2), 0.2614193f, transform.position.z);
        currentLine--;
    }

    IEnumerator GoRightCo()
    {
        float TimePassed = 0;

        while (TimePassed < 1)
        {
            transform.position = Vector3.Lerp(transform.position,
                                              new Vector3(2.6f * (currentLine), 0.2614193f, transform.position.z), TimePassed);
            TimePassed += Time.deltaTime / TimeToTurn;
            yield return null;
        }
        transform.position = new Vector3(2.6f * (currentLine), 0.2614193f, transform.position.z);
        currentLine++;
    }

    public void GoLeft()
    {
        if(currentLine > 1 && TimeSinceLastTurn > TimeToTurn)
        {
            if(GoLeftCoTemp != null)
                StopCoroutine(GoLeftCoTemp);
            GoLeftCoTemp = StartCoroutine(GoLeftCo());
            TimeSinceLastTurn = 0;
        }
    }
    public void GoRight()
    {
        if (currentLine < 4 && TimeSinceLastTurn > TimeToTurn)
        {
            if (GoRightCoTemp != null)
                StopCoroutine(GoRightCoTemp);
            GoRightCoTemp = StartCoroutine(GoRightCo());
            TimeSinceLastTurn = 0;
        }
    }

    public void BreakPressed()
    {
        Breaking = true;
    }

    public void BreakReleased()
    {
        Breaking = false;
    }


    public void AccelerationPressed()
    {
        Accelerating = true;
    }

    public void AccelerationReleased()
    {
        Accelerating = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EnemyCar")
        {
            GameManager.Instance.GameFailed();
        }
    }
}
