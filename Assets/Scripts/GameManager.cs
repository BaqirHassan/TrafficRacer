using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public enum GameState
{
    Starting, Playing, Pause, Failed
}

public class GameManager : MonoBehaviour
{
    [SerializeField] Text CurrentScoreText;
    [SerializeField] GameObject FailScreen;
    [SerializeField] GameObject PlayScreen;



    private GameState gameState;
    public GameState GameState
    {
        get { return gameState; }
        private set { gameState = value;  }
    }

    Transform _Player;
    Transform Player
    {
        get 
        {
            if(_Player)
                return _Player;

            _Player = GameObject.FindGameObjectWithTag("Player").transform;
            if (_Player == null)
            {
                Debug.LogError("Player Not Found");
                return null;
            }
            return _Player;
        }
        
    }

    float CurrentScore = 0;
    float GameStartPosition = 0;


    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 30;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameState = GameState.Starting;
        if(!CurrentScoreText)
        {
            CurrentScoreText.text = "0";
        }
    }

    void Update()
    {
        if(gameState == GameState.Playing)
        {
            CurrentScore = (Player.position.z - GameStartPosition)/1000;
            CurrentScoreText.text = "Km: " + CurrentScore.ToString("F1");
        }
    }

    public void GameFailed()
    {
        GameState = GameState.Failed;
        FailScreen.SetActive(true);
        PlayScreen.SetActive(false);
    }

    public void GameStarted()
    {
        if(GameStartPosition == 0)
            GameStartPosition = Player.position.z;
        GameState = GameState.Playing;
    }

    public Transform GetPlayer()
    {
        return Player;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
