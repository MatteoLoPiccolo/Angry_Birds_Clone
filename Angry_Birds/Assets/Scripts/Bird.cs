using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float maxDragDistance = 4f;
    [SerializeField] float launchPower = 150f;

    LineRenderer lineRenderer;
    Vector3 startPosition;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        startPosition = transform.position;
    }

    void OnMouseDrag() 
    {
        var destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        destination.z = 0;
        if(Vector2.Distance(destination, startPosition) > maxDragDistance)
            destination = Vector3.MoveTowards(startPosition, destination, maxDragDistance);
        transform.position = destination;
        lineRenderer.SetPosition(1, transform.position);
    }

    void OnMouseUp()
    {
        Vector3 directionAndMagnitude = startPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionAndMagnitude * launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
            GetComponent<Rigidbody2D>().gravityScale = 1;

    }
}