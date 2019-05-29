using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    // Properties
    public Vector3 Velocity
    {
        protected set { velocity = value; }
        get { return velocity; }
    }

    // Variables
    public float maxVelocity = 5f, maxDistance = 5f;
    protected Vector3 velocity;
    protected NavMeshAgent agent;
    protected SteeringBehaviour[] behaviours;

    // Start is called before the first frame update
    void Awake()
    {
        // Get Nav Component
        agent = GetComponent<NavMeshAgent>();
        // Get all SteeringBehaviours on AI
        behaviours = GetComponents<SteeringBehaviour>();
    }

    private void Update()
    {
        CalculateForce();
    }

    // Calculates all forces from all behaviours
    public virtual Vector3 CalculateForce()
    {
        // Pseudocode
        // Step 1). Create a result Vector3
        // SET force to zero
        Vector3 force = Vector3.zero;

        // Step 2). Loop through all behaviours and get forces
        foreach (var behaviour in behaviours)
        {
            // Apply force to behaviour.GetForce x Weighting
            force += behaviour.GetForce() * behaviour.weighting;
            // Step 3). Limit the total force to max speed
            // If force magnitude > maxVelocity
            if (force.magnitude > maxVelocity)
            {
                // SET force to force normalized x maxSpeed
                force = force.normalized * maxVelocity;
                // BREAK - Exits the Loop
                break;
            }
        }

        // Step 4). Limit the total velocity to our max velocity if it exceeds
        velocity += force * Time.deltaTime;
        // IF velocity magnitude > max velocity THEN
        if(velocity.magnitude > maxVelocity)
        {
            //  SET velocity to velocity normalized x max velocity
            velocity = velocity.normalized * maxVelocity;
        }


        // Step 5). Sample destination for NavMeshAgent
        // IF velocity magnitude > 0 (velocity not zero)
        if(velocity.magnitude > 0)
        {
            //  SET pos to current (position) + velocity x delta
            Vector3 pos = transform.position + velocity * Time.deltaTime;
            NavMeshHit hit;
            //  IF NavMesh SamplePosition within NavMesh
            if(NavMesh.SamplePosition(pos, out hit, maxDistance, -1))
            {
                // SET agent destination to hit position 
                agent.SetDestination(hit.position);
            }
        }

        // Step 6). Return force
        return force;
    }
}
