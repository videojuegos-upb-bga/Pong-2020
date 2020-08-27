using UnityEngine;

/// <summary>
/// Scores point on Game Manager on Trigger enter
/// Author: Sebastian Gomez Rosas
/// </summary>
public class Score : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GoalLeft"))
            _gameManager.ScoreP1++;
        else if (other.gameObject.CompareTag("GoalRight"))
            _gameManager.ScoreP2++;
        _gameManager.currentGameState = GameManager.GameStates.SERVING;
        _gameManager._uiTimer = _gameManager.uiTime;
    }
}
