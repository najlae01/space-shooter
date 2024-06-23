using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class ShipAgent : Agent
{
    public float movementSpeed = 5f;
    public Vector2 scenarioMaxBounds = new Vector2(5f, 5f); // Define the maximum bounds for each scenario

    private Rigidbody2D rb;
    private Vector2 maxLeftBottom;
    private Vector2 maxRightTop;
    private Vector3 initialPosition;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();

        // Calculate the maximum bounds based on the scenario's maximum bounds
        maxLeftBottom = -scenarioMaxBounds / 2f;
        maxRightTop = scenarioMaxBounds / 2f;

        initialPosition = transform.localPosition; // Store the initial position
    }

    public override void OnEpisodeBegin()
    {
        ResetPlayer();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(rb.velocity);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        // Normalize the movement input
        Vector2 movementInput = new Vector2(moveX, moveY).normalized;

        // Apply movement input with the movement speed
        Vector2 movement = movementInput * movementSpeed;
        rb.velocity = movement;
        transform.localPosition += (Vector3)movement;


        // Debug logs
        Debug.Log("Move X: " + moveX);
        Debug.Log("Move Y: " + moveY);
        Debug.Log("Movement: " + movement);

        // Check if the agent is out of bounds
        if (!IsWithinBounds(transform.localPosition))
        {
            // Apply a negative reward and end the episode
            SetReward(-1f);
            EndEpisode();
        }
    }


    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Horizontal");
        continuousActions[1] = Input.GetAxis("Vertical");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            AddReward(-1f);
        }
    }

    private bool IsWithinBounds(Vector3 position)
    {
        Vector2 relativePosition = position - initialPosition;
        return relativePosition.x >= maxLeftBottom.x && relativePosition.x <= maxRightTop.x &&
               relativePosition.y >= maxLeftBottom.y && relativePosition.y <= maxRightTop.y;
    }


    private void ResetPlayer()
    {
        transform.localPosition = initialPosition; // Reset to the initial position
        rb.velocity = Vector2.zero;
    }
}
