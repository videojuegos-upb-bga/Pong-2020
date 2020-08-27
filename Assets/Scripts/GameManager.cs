using UnityEngine;

/// <summary>
/// Stores game data and validates victory
/// Author: Sebastian Gomez Rosas
/// </summary>
[RequireComponent(typeof(UIManager))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private int maxScore = 5;
    public float uiTime = 5f;
    
    private UIManager _uiManager;
    
    public float _uiTimer;
    public int serveTurn = 1;
    public int winner = 0;
    
    /// <summary>
    /// Current game state
    /// </summary>
    [HideInInspector] public GameStates currentGameState = GameStates.PREGAME;
    
    public int ScoreP1
    {
        get;
        set;
    }
    
    public int ScoreP2
    {
        get;
        set;
    }
    
    public enum GameStates
    {
        PREGAME,      //match over
        PLAYING,      //game running
        SERVING,      //Serve stop
    }

    private void resetGame()
    {
        if (ScoreP1 > 0 || ScoreP2 > 0)
        {
            _uiManager.HideTitle();
            _uiManager.ShowWinner(winner);
        }
        ScoreP1 = 0;
        ScoreP2 = 0;
        serveTurn = 1;
        _uiManager.UpdateScore(1,ScoreP1);
        _uiManager.UpdateScore(2,ScoreP2);
        _uiManager.HidePanel();
        _uiManager.HideServe(1);
        _uiManager.HideServe(2);
    }

    private void UpdateScores()
    {
        _uiManager.UpdateScore(1,ScoreP1);
        _uiManager.UpdateScore(2,ScoreP2);
    }

    private void Awake()
    {
        _uiManager = GetComponent<UIManager>();
    }

    private void Start()
    {
        ScoreP1 = ScoreP2 = 0;
        currentGameState = GameStates.PREGAME;
    }

    private void Update()
    {
        //Game flow check
        if (currentGameState.Equals(GameStates.PLAYING))
        {
            if (ScoreP1 >= maxScore)
            {
                currentGameState = GameStates.PREGAME;
                winner = 1;
                _uiManager.ShowWinner(winner);
            }
                
            else if (ScoreP2 >= maxScore)
            {
                currentGameState = GameStates.PREGAME;
                winner = 2;
                _uiManager.ShowWinner(winner);
            }
            
        }
        else if (currentGameState.Equals(GameStates.PREGAME))
        {
            _uiManager.HideWinner();
            resetGame();
        }
        else if (currentGameState.Equals(GameStates.SERVING))
        {
            _uiManager.ShowServe(serveTurn);
            UpdateScores();
            //turn switch
            _uiTimer -= Time.deltaTime;
            if (_uiTimer < 0)
            {
                _uiManager.HideServe(serveTurn);
                serveTurn = serveTurn.Equals(1) ? 2 : 1;
                currentGameState = GameStates.PLAYING;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _uiManager.HideTitle();
            currentGameState = GameStates.SERVING;
            _uiManager.HideWinner();
        }
    }
}
