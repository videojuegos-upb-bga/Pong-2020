using UnityEngine;

/// <summary>
/// Moves the player alongside the y axis
/// /// Author: Sebastian Gomez Rosas
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PadMovement : MonoBehaviour
{
    //movement velocity
    [Tooltip("Velocity in unity units!")]
    [SerializeField] private float velocity = 5f;

    [Header("Controles para el game pad:")] 
    [SerializeField] private KeyCode upControl = KeyCode.W;
    [SerializeField] private KeyCode downControl = KeyCode.S;

    private Rigidbody2D _rigidbody2D;
    private GameManager _gameManager;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //leer el control de teclas
        //aplicar movimiento
        //mover hacia arriba
        if (!_gameManager.currentGameState.Equals(GameManager.GameStates.PREGAME))
        {
            if(Input.GetKey(upControl))
                transform.Translate(0f,velocity,0f);
            //mover hacia abajo
            else if (Input.GetKey(downControl))
                transform.Translate(0f, -velocity, 0f);
            //limitar traslación
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 3.8f), transform.position.z);
        }
    }
}
