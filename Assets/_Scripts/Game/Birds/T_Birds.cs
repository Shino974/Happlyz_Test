using UnityEngine;

public class T_Birds : MonoBehaviour
{
    public float launchForce;
    public Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Launch(Vector2 direction)
    {
        rb.AddForce(direction * launchForce);
    }
}