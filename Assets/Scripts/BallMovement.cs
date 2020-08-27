using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Moves ball within a 2D space for pong game
/// Author: Sebastian Gomez Rosas
/// </summary>
public class BallMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Vector2 _velocity;
    [SerializeField] private float movementForce = 1f;
    [SerializeField] private ParticleSystem collisionParticles;

    [SerializeField] private AudioClip audioBounceWall;
    [SerializeField] private AudioClip audioBouncePlayer;

    private Vector3 direction;
    private bool canKick;
    
    private GameManager _gameManager;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    void Start()
    {
     }

    void KickBall()
    {
        direction = _gameManager.serveTurn.Equals(1) ? new Vector3(1, Random.Range(-0.5f, 0.5f), 0f) : new Vector3(-1, Random.Range(-0.5f, 0.5f), 0f);
        _rigidbody2D.AddForce(direction.normalized * movementForce, ForceMode2D.Impulse);
        collisionParticles.gameObject.transform.position = transform.position;
        collisionParticles.Play();
        canKick = false;
    }

    void Update()
    {
        _velocity = _rigidbody2D.velocity;

        if (_gameManager.currentGameState.Equals(GameManager.GameStates.SERVING))
        {
            _rigidbody2D.velocity = Vector2.zero;
            canKick = true;
            transform.position = Vector3.zero;
        }
        else if(_gameManager._uiTimer < 0 && canKick)
            KickBall();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        collisionParticles.gameObject.transform.position = transform.position;
        collisionParticles.Play();
        var speed = _velocity.magnitude;
        if (other.gameObject.CompareTag("Player"))
        {
            speed *= 1.2f;
            PlaySound(audioBouncePlayer);
        }
        else
        {
            PlaySound(audioBounceWall);
        }
        var newDirection = Vector3.Reflect(_velocity.normalized, other.contacts[0].normal);
        _rigidbody2D.velocity = newDirection * Mathf.Max(speed, 0f);
    }

    private void PlaySound(AudioClip _clip)
    {
        var audioSource = GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        audioSource.clip = _clip;
        audioSource.Play();
    }
    
}
