﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    public int Health = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Damage(int damage)
    {
        Health -= damage;
    }
}
