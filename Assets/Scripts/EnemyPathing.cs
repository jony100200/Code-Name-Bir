using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    protected WaveConfiguration _waveConfiguration;
    protected List<Transform> _wayPoints;

    protected int _wayPtIndex;

    // Start is called before the first frame update
    private void Start()
    {
        _wayPoints = _waveConfiguration.GetWaypoints();
        transform.position = _wayPoints[_wayPtIndex].transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
    }

    public void SetWaveConfiguration(WaveConfiguration waveConfiguration)
    {
        this._waveConfiguration = waveConfiguration;
    }

    protected void Move()
    {
        if (_wayPtIndex <= _wayPoints.Count - 1)
        {
            var targetPos = _wayPoints[_wayPtIndex].transform.position;
            var moveThisFrame =  _waveConfiguration.GetMoveSpeed()* Time.deltaTime;
            transform.position = Vector2.MoveTowards
                (transform.position, targetPos, moveThisFrame);
            if (transform.position == targetPos)
                _wayPtIndex++;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}