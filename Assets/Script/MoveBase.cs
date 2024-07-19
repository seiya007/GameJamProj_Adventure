using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveBase : MonoBehaviour
{
    [SerializeField]
    private Collider collider;

    public virtual void Start()
    {
        collider = GetComponent<Collider>();
    }
}
