using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    int shipHealth = 20;
    public Image damageImg;
    public Slider healthSlider;
    public Gradient healthSliderGradient;
    public Image healthSliderFill;

    public float moveSpeed = 10f;

    public float maxXValue = 2f;

    public Rigidbody rb;

    public ParticleSystem partSys;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (healthSlider != null)
        {
            healthSlider.maxValue = shipHealth;
            healthSlider.value = shipHealth;
        }
        
        if(partSys == null)
            partSys = transform.GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (GameManager.gameOver && GameManager.gamePause) return;

        if (Input.GetKey(KeyCode.A))
            transform.Translate(moveSpeed * Time.deltaTime * Vector3.left);

        if (Input.GetKey(KeyCode.D))
            transform.Translate(moveSpeed * Time.deltaTime * Vector3.right);

        Vector3 clampedPosition = new()
        {
            x = Mathf.Clamp(transform.position.x, -maxXValue, maxXValue),
            y = transform.position.y,
            z = 0
        };        
        transform.position = clampedPosition;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.gameOver) return;

        if(other.CompareTag("Obstacle"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        shipHealth -= 2;

        if (shipHealth <= 0)
            gameManager.OnGameOver();

        AudioManager.Instance.PlayClip(3);

        LeanTween.value(damageImg.gameObject, 0, 0.3f, 0.1f).setOnUpdate(
            (float val) =>
            {
                Color c = damageImg.color;
                c.a = val;
                damageImg.color = c;
            });

        LeanTween.value(damageImg.gameObject, 0.3f, 0f, 0.5f).setOnUpdate(
            (float val) =>
            {
                Color c = damageImg.color;
                c.a = val;
                damageImg.color = c;
            });

        LeanTween.scale(healthSlider.gameObject, Vector3.one, 0.1f);

        healthSlider.value = shipHealth;
        healthSliderFill.color = healthSliderGradient.Evaluate(healthSlider.normalizedValue);
        LeanTween.delayedCall(1f, () => LeanTween.scale(healthSlider.gameObject, Vector3.one / 2, 1f));


    }
}
