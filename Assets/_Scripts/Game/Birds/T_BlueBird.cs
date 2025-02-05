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
        Vector3[] positions = {
            transform.position + new Vector3(-1f, 0, 0),
            transform.position + new Vector3(1f, 0, 0)
        };

        Vector2 velocity = rb.velocity;

        foreach (Vector3 position in positions)
        {
            GameObject bird = Instantiate(birdPrefab, position, Quaternion.identity);
            bird.GetComponent<Rigidbody2D>().velocity = velocity;
            Destroy(bird, 4f);
        }
    }
}