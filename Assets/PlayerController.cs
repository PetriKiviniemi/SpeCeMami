using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementDir {
    LEFT, RIGHT, UP, DOWN
}

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    public float groundDeceleration = 150.0f;
    public float walkAcceleration = 150.0f;
    float airAcceleration = 30.0f;
    public float speed = 13.0f;
    public float jumpHeight = 5.0f;
    public Vector2 velocity = new Vector3(0, 0);
    public float mass = 3.0f;
    private BoxCollider2D boxCollider;
    public bool grounded;
    private bool moveLockedLeft = false;
    private bool moveLockedRight = false;
    public MovementDir curDir;


    public void OnStart()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    public void OnUpdate()
    {
        float moveInputHor = Input.GetAxisRaw("Horizontal");
        float moveInputVer = Input.GetAxisRaw("Vertical");


        if (grounded)
        {
            velocity.y = 0;

            if (Input.GetButtonDown("Jump"))
            {
                // Calculate the velocity required to achieve the target jump height.
                velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
                curDir = MovementDir.UP;
            }
        }
        float acceleration = grounded ? walkAcceleration : airAcceleration;
        float deceleration = grounded ? groundDeceleration : 0;

        if (moveInputHor != 0)
        {
            if (moveInputHor == -1 && !moveLockedLeft || moveInputHor == 1 && !moveLockedRight)
                velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInputHor, acceleration * Time.deltaTime);
            if(moveInputHor == -1)
                curDir = MovementDir.LEFT;
            if(moveInputHor == 1)
                curDir = MovementDir.RIGHT;
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
        }

        if (moveInputVer != 0)
        {
            if (moveInputVer == 1)
            {
                curDir = MovementDir.UP;
            }
        }

        velocity.y += Physics2D.gravity.y * Time.deltaTime * mass;
        gameObject.transform.Translate(velocity * Time.deltaTime);
        grounded = false;

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);
        foreach (Collider2D hit in hits)
        {
            if (hit == boxCollider)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
                {
                    grounded = true;
                    moveLockedLeft = false;
                    moveLockedRight = false;
                }
                if (Vector2.Angle(colliderDistance.normal, Vector2.up) == 180 && velocity.y > 0)
                {
                    velocity.y = -0.1f;
                }
                //Freeze movement to left or right if wall is hit
                if (Vector2.Angle(colliderDistance.normal, Vector2.left) == 180)
                {
                    moveLockedLeft = true;
                    moveLockedRight = false;
                    velocity.x = 0.01f;
                }
                if (Vector2.Angle(colliderDistance.normal, Vector2.right) == 180)
                {
                    moveLockedRight = true;
                    moveLockedLeft = false;
                    velocity.x = -0.01f;
                }
            }
        }

    }

    //TODO:: FIX
    private Vector3 AdjustedVelToSlope(Vector2 vel)
    {
        RaycastHit2D hit = Physics2D.Raycast( transform.position - new Vector3(0, 0.55f, 0), Vector2.down, 0.25f);
        Debug.DrawRay(transform.position - new Vector3(0, 0.55f, 0), Vector2.down * 0.25f, Color.green);

        if(hit.collider && hit.collider != boxCollider)
        {
            var slopeRotation = Quaternion.FromToRotation(Vector2.up, hit.normal);
            var adjustedVel = slopeRotation * vel;
            Debug.Log("Adjusteved vel: " + adjustedVel);

            if (adjustedVel.y < 0.3f)
                return adjustedVel;
        }

        return vel;
    }
}
