using UnityEngine;

public class Ball : MonoBehaviour
{
    //Config Parameters
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] AudioClip ballRespawnSound;
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 20f;
    [SerializeField] float randomFactor = 0.2f;

    //State
    Vector2 paddleToBallVector;
    private bool hasStarted = false;

    public void ResetBall()
    {
        myAudioSource.PlayOneShot(ballRespawnSound);
        hasStarted = false;

    }

    //Cached Component References
    AudioSource myAudioSource;
    Rigidbody2D myRigidbody2D;
    GameSession myGameSession;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myGameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnClick();
        } 
    }

    private void LaunchOnClick()
    {
        if (Input.GetMouseButtonDown(0) || myGameSession.IsAutoPlayEnabled())
        {
            hasStarted = true;
            myRigidbody2D.velocity = new Vector2(2f, 15f);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePosition = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePosition + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasStarted)
        {
            //Vector2 velocityTweak = new Vector2(RandomVector(), RandomVector());
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidbody2D.velocity = Quaternion.Euler(0, 0, RandomVector()) * myRigidbody2D.velocity;
        }
    }

    private float RandomVector()
    {
        return Random.Range(-randomFactor, randomFactor);
    }
}
