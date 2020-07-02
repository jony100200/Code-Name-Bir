using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoingDamage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected int _damage = 100;

    public int GetDamaged()
    {
        return _damage;
    }

    public void GetHit()
    {
        Destroy(gameObject);
    }
}
