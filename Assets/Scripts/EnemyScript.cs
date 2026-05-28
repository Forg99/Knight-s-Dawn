
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private LayerMask groundLayer;

    private bool lookingRight;
    private bool lookingLeft;

    private float direction = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        // Check for walls
        lookingRight = Physics2D.Raycast(
            transform.position,
            Vector2.right,
            0.6f,
            groundLayer
        );

        lookingLeft = Physics2D.Raycast(
            transform.position,
            Vector2.left,
            0.6f,
            groundLayer
        );

        // Turn around if moving right and hit wall
        if (lookingRight && direction > 0)
        {
            direction = -1;
            sprite.flipX = true;
        }

        // Turn around if moving left and hit wall
        if (lookingLeft && direction < 0)
        {
            direction = 1;
            sprite.flipX = false;
        }

        // Apply movement every frame
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Despawner"))
        {
            Destroy(gameObject);
        }
    }
}