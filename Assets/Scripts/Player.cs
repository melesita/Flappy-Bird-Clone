using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    public Sprite[] sprites;
    private int spriteIndex;

    private Vector3 direction;
    public float gravity = -9.8f;
    public float strength = 5f;

    private void Awake()
    {
        // Initialization of object
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Start()
    {
        // Gets called the first frame 
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0;
        transform.position = position;
        
        direction = Vector3.zero;
    }

    private void Update()
    {
        // You can press space bar or the left mouse click
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            direction = Vector3.up * strength;
        }

        // Tells you how many fingers are touching the screen
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                direction = Vector3.up * strength;
            }
        }

        direction.y += gravity * Time.deltaTime; // velocity
        transform.position += direction * Time.deltaTime;
    }

    private void AnimateSprite()
    {
        spriteIndex++;

        if(spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // check tag of other
        if (other.gameObject.tag == "Obstacle")
        {
            FindObjectOfType<GameManager>().GameOver(); // there are better ways to do this 
        }
        else if (other.gameObject.tag == "Scoring")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }

    }
}
