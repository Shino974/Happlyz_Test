using UnityEngine;

public class T_BlueBird : T_Birds
{
    public GameObject birdPrefab; // Reference to the bird prefab

    protected override void Start()
    {
        base.Start();
    }

    public override void Launch(Vector2 direction)
    {
        base.Launch(direction);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DuplicateBird();
        }
    }

    private void DuplicateBird()
    {
        Vector3 position = transform.position;
        Vector2 velocity = rb.velocity;

        GameObject bird1 = Instantiate(birdPrefab, position + new Vector3(-0.5f, 0, 0), Quaternion.identity);
        GameObject bird2 = Instantiate(birdPrefab, position, Quaternion.identity);
        GameObject bird3 = Instantiate(birdPrefab, position + new Vector3(0.5f, 0, 0), Quaternion.identity);

        bird1.GetComponent<Rigidbody2D>().velocity = velocity;
        bird2.GetComponent<Rigidbody2D>().velocity = velocity;
        bird3.GetComponent<Rigidbody2D>().velocity = velocity;

        Destroy(gameObject);
    }
}