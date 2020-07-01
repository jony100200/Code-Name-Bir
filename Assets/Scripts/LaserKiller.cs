using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserKiller : MonoBehaviour
{
    // Start is called before the first frame update
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
