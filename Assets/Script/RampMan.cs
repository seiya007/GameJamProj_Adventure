using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampMan : MonoBehaviour
{
    private Collider collider;    
    private Rigidbody rigidbody;
    
    [SerializeField]
    private SpriteAnimation spriteAnimation;

    private bool isMoving = false;
    
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = VirtualInputManager.Instance.GetStickDir();
        Move();
        AnimState();
    }

    private void Move()
    {
        rigidbody.velocity = dir * 2.0f;
    }

    private void AnimState()
    {
        if(rigidbody.velocity.magnitude > 0.0f)
        {
            if(isMoving) return;

            spriteAnimation.AnimChange(SpriteAnimation.AnimState.Walk);
            isMoving = true;
        }
        else
        {
            if(!isMoving) return;

            spriteAnimation.AnimChange(SpriteAnimation.AnimState.Idle);
            isMoving = false;
        }
    }
}
