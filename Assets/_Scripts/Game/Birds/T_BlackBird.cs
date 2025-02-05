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

    private void IncreaseSize()
    {
        transform.localScale *= 4; // Increase the size by 4 times
    }
}