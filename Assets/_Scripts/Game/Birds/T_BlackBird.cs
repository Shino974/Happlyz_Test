using UnityEngine;

public class T_BlackBird : T_Birds
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
            IncreaseSize();
        }
    }

    // Increase the size of the bird (Black Bird Power)
    private void IncreaseSize()
    {
        transform.localScale *= 4;
    }
}