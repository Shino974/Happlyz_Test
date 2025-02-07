using UnityEngine;

public class T_BlueBird : T_Birds
{
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
    
    // Duplicate the bird (Blue Bird Power)
    private void DuplicateBird()
    {
        // Positions of the duplicated birds
        Vector3[] positions = {
            transform.position + new Vector3(-1f, 0, 0),
            transform.position + new Vector3(1f, 0, 0)
        };
    
        // Velocity of the duplicated birds
        Vector2 velocity = rb.velocity;

        // Create the duplicated birds
        foreach (Vector3 position in positions)
        {
            GameObject bird = Instantiate(gameObject, position, Quaternion.identity);
            bird.GetComponent<Rigidbody2D>().velocity = velocity;
            Destroy(bird, 4f);
        }
    }
}