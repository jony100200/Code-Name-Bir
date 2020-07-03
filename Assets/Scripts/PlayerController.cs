using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player ")]
    [SerializeField] protected float _moveSpeed;
    //doing offset so we player ship don't go off-screen
    [SerializeField] protected float _playerOffset;
    [SerializeField] protected int _hp=200;
    [Header("Projectile")]
    [SerializeField] protected float _projectileSpeed;
    [SerializeField] protected float _projectileFiringInterval;
    [SerializeField] protected GameObject _bulletPrefab;
    protected Coroutine _firingCoroutine;
    //[SerializeField] protected AudioSource _firing;
    protected float xMax;
    protected float xMin;
    protected float yMax;
    protected float yMin;

    // Start is called before the first frame update
    private void Start()
    {
        MovementBoundaries();
        //_shoot = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        Fire();
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {
        DoingDamage doingDamage = other.gameObject.GetComponent<DoingDamage>();
        if (!doingDamage)
        {
            return;
        }
        DestroyPlayer(doingDamage);
    }

    protected void DestroyPlayer(DoingDamage doingDamage)
    {
        _hp -= doingDamage.GetDamaged();
        doingDamage.GetHit();
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    //player movement code
    protected void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * _moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * _moveSpeed;
        var newPosX = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newPosY = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newPosX, newPosY);
    }

    //clamping to the game camera view boundary
    protected void MovementBoundaries()
    {
        var gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + _playerOffset;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - _playerOffset;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + _playerOffset;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - _playerOffset;
    }

    protected void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _firingCoroutine=StartCoroutine(RapidFire());
            // _shoot.Play();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(_firingCoroutine);
        }
    }
    //to fire bullets continuously
    IEnumerator RapidFire()
    {
        while (true)
        {
            var bullet = Instantiate(_bulletPrefab,
                transform.position,
                Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, _projectileSpeed);
            yield return new WaitForSeconds(_projectileFiringInterval);
        }
        
    }

   
}