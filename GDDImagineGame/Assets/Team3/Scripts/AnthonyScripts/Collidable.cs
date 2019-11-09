﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collidable : MonoBehaviour
{

    public abstract void OnCollide(GameObject other);

    private void OnTriggerEnter(Collider other)
    {
        OnCollide(other.gameObject);
    }
}
