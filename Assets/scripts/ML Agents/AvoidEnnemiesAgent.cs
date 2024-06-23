using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
public class AvoidEnnemiesAgent : Agent
{
    public float movementSpeed = 5f;

    private Rigidbody2D rb;
    private Camera mainCamera;
    private float maxLeft;
    private float maxRight;
    private float maxTop;
    private float maxBottom;


    public override void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        StartCoroutine(SetBoundaries());

    }

    public override void OnEpisodeBegin()
    {
        ResetPlayer();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(rb.velocity);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Check if auto control is enabled
        if (LevelManager.levelManager != null && LevelManager.levelManager.autoControlEnabled)
        {
            float moveX = actions.ContinuousActions[0];
            float moveY = actions.ContinuousActions[1];

            Vector2 movement = new Vector2(moveX, moveY) * movementSpeed;
            rb.velocity = movement;

            // Check if the agent is out of bounds
            if (!IsWithinBounds(transform.position))
            {
                // Apply a negative reward and end the episode
                SetReward(-1f);
                EndEpisode();
            }
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // Check if auto control is enabled
        if (LevelManager.levelManager != null && LevelManager.levelManager.autoControlEnabled)
        {
            ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
            continuousActions[0] = Input.GetAxis("Horizontal");
            continuousActions[1] = Input.GetAxis("Vertical");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if auto control is enabled
        if (LevelManager.levelManager != null && LevelManager.levelManager.autoControlEnabled)
        {
            if (collision.CompareTag("Enemy"))
            {
                AddReward(-1f);
            }
        }
    }

    private IEnumerator SetBoundaries()
    {
        yield return null;

        maxLeft = mainCamera.ViewportToWorldPoint(new Vector2(0.15f, 0)).x;
        maxRight = mainCamera.ViewportToWorldPoint(new Vector2(0.85f, 0)).x;

        maxBottom = mainCamera.ViewportToWorldPoint(new Vector2(0, 0.08f)).y;
        maxTop = mainCamera.ViewportToWorldPoint(new Vector2(0, 0.7f)).y;
    }

    private bool IsWithinBounds(Vector3 position)
    {
        return position.x >= maxLeft && position.x <= maxRight &&
               position.y >= maxBottom && position.y <= maxTop;
    }

    private void ResetPlayer()
    {
        transform.position = Vector3.zero;
        rb.velocity = Vector2.zero;
    }
}
