using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();    
    }

    private void Update()
    {
        if (ps && !ps.IsAlive()) // is there is a ps and if ps is not alive (ps will die after completing its loop)
        {
            DestorySelf();
        }
    }
    
    public void DestorySelf()
    {
        Destroy(gameObject);
    }

}
