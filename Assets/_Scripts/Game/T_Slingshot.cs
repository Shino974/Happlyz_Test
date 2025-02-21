using UnityEngine;

public class T_Slingshot : MonoBehaviour
{
    [Header("Slingshot")]
    public LineRenderer[] lineRenderers;
    public LineRenderer trajectoryLineRenderer;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;

    private Vector3 _currentPosition;
    private float _maxLength = 3;
    private float _bottomBoundary = -3.5f;
    private bool _isMouseDown;

    [Header("Bird")]
    public GameObject birdPrefab;

    private float _birdPositionOffset = -0.4f;
    private T_Birds _bird;
    private Collider2D _birdCollider;

    // References
    public T_ScoreHandler scoreHandler;
    public T_LifeHandler lifeHandler;

    void Start()
    {
        InitializeLineRenderers();
        CreateBird();
    }
    
    void Update()
    {
        if (_isMouseDown && _bird != null)
            HandleMouseDrag();
        else
            ResetStrips();
    }
    
    // Initialize the line renderers for slingshot strips
    private void InitializeLineRenderers()
    {
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);
    }

    // Create a bird on the slingshot
    private void CreateBird()
    {
        if (lifeHandler != null && lifeHandler.lives > 0)
        {
            _bird = Instantiate(birdPrefab).GetComponent<T_Birds>();
            _birdCollider = _bird.GetComponent<Collider2D>();
            _birdCollider.enabled = false;
            _bird.rb.isKinematic = true;
            ResetStrips();
        }
        else if (scoreHandler != null)
        {
            scoreHandler.ShowVictoryCanvas(); // Show the victory canvas if no lives are left
        }
    }

    // Handle the mouse drag for slingshot
    private void HandleMouseDrag()
    {
        // Get the mouse position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10;

        // Get the current position of the slingshot
        _currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        _currentPosition = center.position + Vector3.ClampMagnitude(_currentPosition - center.position, _maxLength);
        _currentPosition = ClampBoundary(_currentPosition);

        // Set the strips and display the trajectory
        SetStrips(_currentPosition);
        DisplayTrajectory();

        // Enable the bird collider
        if (_birdCollider)
            _birdCollider.enabled = true;
    }

    private void OnMouseDown()
    {
        if (_bird != null)
        {
            _isMouseDown = true;
        }
    }

    private void OnMouseUp()
    {
        if (_bird != null)
        {
            _isMouseDown = false;
            Shoot();
            _currentPosition = idlePosition.position;
            trajectoryLineRenderer.positionCount = 0;
        }
    }

    // Shoot the bird
    private void Shoot()
    {
        // Shoot the bird
        _bird.rb.isKinematic = false;
        Vector3 birdForce = (_currentPosition - center.position) * _bird.launchForce * -1;
        _bird.Launch(birdForce);
        
        // Destroy the bird after 5 seconds
        Destroy(_bird.gameObject, 3f);
        
        // Create new bird on the slingshot
        _bird = null;
        _birdCollider = null;
        Invoke("CreateBird", 3);
        
        // Lose life after shoot
        lifeHandler.LoseLife(); // Decrease the number of lives
    }

    // Reset the strips to the idle position
    private void ResetStrips()
    {
        _currentPosition = idlePosition.position;
        SetStrips(_currentPosition);
    }

    // Set the strips to the given position
    private void SetStrips(Vector3 position)
    {
        foreach (var lineRenderer in lineRenderers)
            lineRenderer.SetPosition(1, position);

        if (_bird)
        {
            Vector3 dir = position - center.position;
            _bird.transform.position = position + dir.normalized * _birdPositionOffset;
            _bird.transform.right = -dir.normalized;
        }
    }

    // Clamp the boundary of the slingshot
    private Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, _bottomBoundary, 1000);
        return vector;
    }

    // Display the trajectory of the bird
    private void DisplayTrajectory()
    {
        // Calculate the trajectory
        Vector3 velocity = (_currentPosition - center.position) * _bird.launchForce * -1;
        int segmentCount = 50; // Lenght of trajectory line
        Vector3[] segments = new Vector3[segmentCount]; // Array of trajectory points
        segments[0] = _bird.transform.position; // Start from the bird's current position

        // Calculate the trajectory points
        for (int i = 1; i < segmentCount; i++)
        {
            float time = i * Time.fixedDeltaTime * 5;
            segments[i] = segments[0] + velocity * time + 0.5f * (Vector3)(Physics2D.gravity) * time * time;
        }

        // Set the trajectory line renderer
        trajectoryLineRenderer.positionCount = segmentCount;
        trajectoryLineRenderer.SetPositions(segments);
    }
}