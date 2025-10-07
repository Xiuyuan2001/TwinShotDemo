using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction;
    private BoxCollider2D boxCollider;

    private float arrowSpeed;
    private float arrowGravity;
    [SerializeField] private int arrowDamage = 1;
    private Transform player;

    [Header("Platform Info")]
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private float platformDisappearTime = 3f;
    private bool isStuckInWall = false;

    [Header("Timer Info")]
    private float disappearTimer;

    [Header("Flash Effect")]
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float flashStartTime = 1.5f;
    private bool isFlashing = false;
    private float flashTimer = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        rb.velocity = direction * arrowSpeed;
        rb.gravityScale = arrowGravity;
    }

    public void SetUpArrow(float _arrowSpeed, float _arrowGravity, Transform _playerDir)
    {
        arrowSpeed = _arrowSpeed;
        arrowGravity = _arrowGravity;
        player = _playerDir;

        direction = player.localScale.x > 0 ? Vector2.right : Vector2.left;
        transform.localScale = new Vector3(player.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void Update()
    {
        if (isStuckInWall)
        {
            disappearTimer -= Time.deltaTime;

            if (disappearTimer <= flashStartTime && !isFlashing)
            {
                isFlashing = true;
            }

            if (isFlashing)
            {
                HandleFlashing();
            }

            if (disappearTimer <= 0)
            {
                DestroyMe();
            }
        }
    }

    private void HandleFlashing()
    {
        // 利用插值改变频率 - 10f -> 30f ; 插值参数[0,1] - 切随时间参数逐渐接近于1，频率增高；
        float flashFrequency = Mathf.Lerp(10f, 30f, 1f - (disappearTimer / flashStartTime));

        flashTimer += Time.deltaTime * flashFrequency;

        if (spriteRenderer != null)
        {
            // 调整alpha通道值 - [-1,1] -> [0,2] -> [0,1]; 利用sin达到[0,1]的波动效果，将变化速度不断增加的flashTimer作为sin参数，达到波动加速的效果；
            float alpha = (Mathf.Sin(flashTimer) + 1f) * 0.5f;
            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(0.3f, 1f, alpha);
            spriteRenderer.color = color;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            EnemyStats enemyStats = collision.gameObject.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(arrowDamage);
            }

            DestroyMe();
            return;
        }

        if (((1 << collision.gameObject.layer) & platformLayer) != 0)
        {
            StickToWall();
        }
    }

    private void StickToWall()
    {
        if (isStuckInWall) return;

        isStuckInWall = true;
        disappearTimer = platformDisappearTime;

        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        rb.bodyType = RigidbodyType2D.Kinematic;

        gameObject.layer = LayerMask.NameToLayer("Ground");

        PlatformEffector2D platformEffector = gameObject.AddComponent<PlatformEffector2D>();
        platformEffector.useOneWay = true;      
        platformEffector.surfaceArc = 90;      
        platformEffector.rotationalOffset = 0;  

        if (boxCollider != null)
        {
            boxCollider.usedByEffector = true; 
        }
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }
}
