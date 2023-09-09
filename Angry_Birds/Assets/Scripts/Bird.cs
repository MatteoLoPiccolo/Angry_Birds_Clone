using UnityEngine;
using UnityEngine.SceneManagement;

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
        lineRenderer.enabled = false;
    }

    void OnMouseDrag()
    {
        var destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        destination.z = 0;
        if (Vector2.Distance(destination, startPosition) > maxDragDistance)
            destination = Vector3.MoveTowards(startPosition, destination, maxDragDistance);
        transform.position = destination;
        lineRenderer.SetPosition(1, transform.position);
        lineRenderer.enabled = true;
    }

    void OnMouseUp()
    {
        Vector3 directionAndMagnitude = startPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionAndMagnitude * launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            GetComponent<Rigidbody2D>().gravityScale = 1;

        if (FindAnyObjectByType<Enemy>(FindObjectsInactive.Exclude) == null)
        {
            var levelToLoad = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(levelToLoad);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke(nameof(ReloadLevel), 5);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}