using UnityEngine;
using System.Collections;

public class MovingPlatformController : MonoBehaviour
{

    [SerializeField]
    Transform platform;

    [SerializeField]
    Transform startTransform;

    [SerializeField]
    Transform endTransform;

    [SerializeField]
    float platformSpeed;


    Rigidbody2D platformRigidBody;
    Vector3 direction;
    Transform destination;

    void Start()
    {
        platformRigidBody = platform.GetComponent<Rigidbody2D>();

        SetDestination(startTransform);
    }

    void FixedUpdate()
    {
        platformRigidBody.MovePosition(platform.position + direction * platformSpeed * Time.fixedDeltaTime);

        if (Vector3.Distance(platform.position, destination.position) < platformSpeed * Time.fixedDeltaTime)
        {
            if (destination == startTransform)
            {
                SetDestination(endTransform);
            }
            else
            {
                SetDestination(startTransform);
            }
        }
    }


    void OnDrawGizmos()
    {
        if (startTransform && platform)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(startTransform.position, platform.localScale);
        }

        if (endTransform && platform)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(endTransform.position, platform.localScale);
        }

    }

    void SetDestination(Transform dest)
    {
        destination = dest;
        direction = (destination.position - platform.position).normalized;
    }
}