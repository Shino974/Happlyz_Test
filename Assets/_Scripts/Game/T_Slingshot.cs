using UnityEngine;

public class T_Slingshot : MonoBehaviour
{
    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;
    public LineRenderer trajectoryLineRenderer;

    private Vector3 _currentPosition;

    private float _maxLength = 3;
    private float _bottomBoundary = -3.5f;

    private bool _isMouseDown;

    public GameObject birdPrefab;
    private float _birdPositionOffset = -0.4f;
    private T_Birds _bird;
    private Collider2D _birdCollider;

    void Start()
    {
        InitializeLineRenderers();
        CreateBird();
    }

    private void InitializeLineRenderers()
    {
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);
    }

    private void CreateBird()
    {
        _bird = Instantiate(birdPrefab).GetComponent<T_Birds>();
        _birdCollider = _bird.GetComponent<Collider2D>();
        _birdCollider.enabled = false;
        _bird.rb.isKinematic = true;
        ResetStrips();
    }

    void Update()
    {
        if (_isMouseDown)
            HandleMouseDrag();
        else
            ResetStrips();
    }

    private void HandleMouseDrag()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10;

        _currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        _currentPosition = center.position + Vector3.ClampMagnitude(_currentPosition - center.position, _maxLength);
        _currentPosition = ClampBoundary(_currentPosition);

        SetStrips(_currentPosition);
        DisplayTrajectory();

        if (_birdCollider)
            _birdCollider.enabled = true;
    }

    private void OnMouseDown()
    {
        _isMouseDown = true;
    }

    private void OnMouseUp()
    {
        _isMouseDown = false;
        Shoot();
        _currentPosition = idlePosition.position;
        trajectoryLineRenderer.positionCount = 0; // Clear the trajectory
    }

    private void Shoot()
    {
        _bird.rb.isKinematic = false;
        Vector3 birdForce = (_currentPosition - center.position) * _bird.launchForce * -1;
        _bird.rb.velocity = birdForce;

        Destroy(_bird.gameObject, 5f); // Destroy the bird after 5 seconds

        _bird = null;
        _birdCollider = null;
        Invoke("CreateBird", 1);
    }

    private void ResetStrips()
    {
        _currentPosition = idlePosition.position;
        SetStrips(_currentPosition);
    }

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

    private Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, _bottomBoundary, 1000);
        return vector;
    }

    private void DisplayTrajectory()
    {
        Vector3 velocity = (_currentPosition - center.position) * _bird.launchForce * -1;
        int segmentCount = 20;
        Vector3[] segments = new Vector3[segmentCount];
        segments[0] = _bird.transform.position; // Start from the bird's current position

        for (int i = 1; i < segmentCount; i++)
        {
            float time = i * Time.fixedDeltaTime * 5;
            segments[i] = segments[0] + velocity * time + 0.5f * (Vector3)(Physics2D.gravity) * time * time;
        }

        trajectoryLineRenderer.positionCount = segmentCount;
        trajectoryLineRenderer.SetPositions(segments);
    }
}