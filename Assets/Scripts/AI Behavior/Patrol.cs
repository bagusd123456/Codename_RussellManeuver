using UnityEngine;

public class Patrol : MonoBehaviour
{
    public bool active;

    public Transform[] waypoints;
    private int _currentWaypointIndex = 0;
    public float _speed = 2f;

    private float _waitTime = 1f; // in seconds
    private float _waitCounter = 0f;
    private bool _waiting = false;

    public int currentSpeed;
    private void Update()
    {
        if (!active) return;
        if (_waiting)
        {
            currentSpeed = 0;
            _waitCounter += Time.deltaTime;
            if (_waitCounter < _waitTime)
                return;
            _waiting = false;
        }

        Transform wp = waypoints[_currentWaypointIndex];
        if (Vector3.Distance(transform.position, wp.position) < 0.01f)
        {
            transform.position = wp.position;
            _waitCounter = 0f;
            _waiting = true;

            _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
        }
        else
        {
            currentSpeed = 1;
            transform.position = Vector3.MoveTowards(
                transform.position,
                wp.position,
                _speed * Time.deltaTime);
            transform.LookAt(wp.position);
        }
    }

}