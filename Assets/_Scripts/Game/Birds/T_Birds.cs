using UnityEngine;

public class T_Birds : MonoBehaviour
{
    public float launchForce;
    public Rigidbody2D rb;
    private T_ScoreHandler _scoreHandler;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _scoreHandler = FindObjectOfType<T_ScoreHandler>();
    }

    public virtual void Launch(Vector2 direction)
    {
        rb.AddForce(direction * launchForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Props"))
        {
            _scoreHandler.IncrementScore();
            collision.gameObject.GetComponent<Collider2D>().enabled = false; // Disable the collider
            Destroy(collision.gameObject, 2f); // Destroy the Props object after 2 seconds
        }
    }
}