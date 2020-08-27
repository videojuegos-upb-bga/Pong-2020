using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Handles showing or hiding GUI elements
/// Author: Sebastian Gomez Rosas
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("Colocar elementos GUI aquí ->")] [SerializeField]
    private TMP_Text textTitle;
    
    [SerializeField] private TMP_Text textScore1;
    [SerializeField] private TMP_Text textScore2;
    
    [SerializeField] private TMP_Text textServe1;
    [SerializeField] private TMP_Text textServe2;

    [SerializeField] private TMP_Text textWinner;
    
    private GameObject _panelHelp;

    private void Awake()
    {
        _panelHelp = GameObject.FindWithTag("PanelHelp");
    }

    // Start is called before the first frame update
    void Start()
    {
        _panelHelp.SetActive(false);
    }
    
    /// <summary>
    /// Shows title based on an event
    /// </summary>
    public void ShowTitle()
    {
        textTitle.gameObject.SetActive(true);
    }
    
    /// <summary>
    /// Hides title based on an event
    /// </summary>
    public void HideTitle()
    {
        textTitle.gameObject.SetActive(false);
    }

    /// <summary>
    /// Hides panel based on an event
    /// </summary>
    public void HidePanel()
    {
        _panelHelp.SetActive(false);
    }
    
    /// <summary>
    /// Shows serve notice
    /// </summary>
    /// <param name="_player"></param>
    public void ShowWinner(int _player)
    {
        textWinner.gameObject.SetActive(true);
        switch (_player)
        {
            case 1:
                textWinner.text = "Player 1 wins";
                break;
            case 2:
                textWinner.text = "Player 2 wins";
                break;
            default:
                Debug.LogWarning("Wrong parameter for Show Winner!");
                break;
        }
    }
    
    /// <summary>
    /// Hides winner based on an event
    /// </summary>
    public void HideWinner()
    {
        textWinner.gameObject.SetActive(false);
    }

    /// <summary>
    /// Shows serve notice
    /// </summary>
    /// <param name="_player"></param>
    public void ShowServe(int _player)
    {
        switch (_player)
        {
            case 1:
                textServe1.gameObject.SetActive(true);
                break;
            case 2:
                textServe2.gameObject.SetActive(true);
                break;
            default:
                Debug.LogWarning("Wrong parameter for Show Serve!");
                break;
        }
    }

    /// <summary>
    /// Hides serve notice
    /// </summary>
    /// <param name="_player"></param>
    public void HideServe(int _player)
    {
        switch (_player)
        {
            case 1:
                textServe1.gameObject.SetActive(false);
                break;
            case 2:
                textServe2.gameObject.SetActive(false);
                break;
            default:
                Debug.LogWarning("Wrong parameter for Hide Serve!");
                break;
        }
    }

    /// <summary>
    /// Updates player score
    /// </summary>
    /// <param name="_player"></param>
    /// <param name="_score"></param>
    public void UpdateScore(int _player, int _score)
    {
        switch (_player)
        {
            case 1:
                textScore1.text = _score.ToString();
                break;
            case 2:
                textScore2.text = _score.ToString();
                break;
            default:
                Debug.LogWarning("Wrong parameter for Update Score!");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
            _panelHelp.SetActive(true);
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.O) ||
                 Input.GetKeyDown(KeyCode.L))
        {
            _panelHelp.SetActive(false);
        }
    }
}
