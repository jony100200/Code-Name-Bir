using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float _hp=100;
    [SerializeField] protected float _shotCounter;

    [SerializeField] protected float _minTimeBetweenShots=0.2f;

    [SerializeField] protected float _maxTimeBetweenShots = 3f;

    [SerializeField] protected GameObject _projectile;

    [SerializeField] protected float _projectileSpeed=-10f;
    // Start is called before the first frame update
    void Start()
    {
        _shotCounter = Random.Range(_minTimeBetweenShots, _maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    protected void CountDownAndShoot()
    {
        _shotCounter -= Time.deltaTime;
        if (_shotCounter<=0f)
        {
            Fire();
            _shotCounter = Random.Range(_minTimeBetweenShots, _maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(
            _projectile, transform.position, Quaternion.identity
        )as GameObject;
        _projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -_projectileSpeed);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        DoingDamage doingDamage= other.gameObject.GetComponent<DoingDamage>();
        DestroyEnemy(doingDamage);
        
    }

    protected void DestroyEnemy(DoingDamage doingDamage)
    {
        _hp -= doingDamage.GetDamaged();
        if (_hp <=0)
        {
            Destroy(gameObject);
        }
    }
}
