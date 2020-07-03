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
    [SerializeField] protected GameObject _explosion;

    [SerializeField] protected float _projectileSpeed=-10f;

    [Header("SFX ")]
    [SerializeField] protected AudioClip _deathSFX;
    [SerializeField] protected AudioClip _shootSFX;
    [SerializeField] [Range(0, 1)] protected float _deathSFXVolume = 0.6f;
    [SerializeField] [Range(0, 1)] protected float _shootSFXVolume = 0.6f;
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
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -_projectileSpeed);
        AudioSource.PlayClipAtPoint(_shootSFX, Camera.main.transform.position, _shootSFXVolume);
    }

    protected void Explosion()
    {
        GameObject explode = Instantiate(_explosion,transform.position,Quaternion.identity
        ) as GameObject;
        Destroy(explode,1);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        DoingDamage doingDamage= other.gameObject.GetComponent<DoingDamage>();
        if (!doingDamage)
        {
            return;
        }
        DestroyEnemy(doingDamage);
    }

    protected void DestroyEnemy(DoingDamage doingDamage)
    {
        _hp -= doingDamage.GetDamaged();
        doingDamage.GetHit();
        if (_hp <=0)
        {
            Explosion();
            AudioSource.PlayClipAtPoint(_deathSFX,Camera.main.transform.position,_deathSFXVolume);
            Destroy(gameObject);
        }
    }
}
