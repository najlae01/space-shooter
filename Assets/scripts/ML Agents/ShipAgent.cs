using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class ShipAgent : Agent
{
    public float movementSpeed = 5f;
    [SerializeField] public Vector2 scenarioOriginPosition;
    public Vector2 scenarioMaxSize = new Vector2(4f, 5f); // Define the maximum SIZE for scenarios

    [SerializeField] private float safeDistance = 0.5f;

    private Rigidbody2D rb;
    private float maxLeft;
    private float maxRight;
    private float maxTop;
    private float maxBottom;
    private Camera mainCamera;
    private Vector3 initialPosition;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;

        StartCoroutine(SetBoundaries());



        initialPosition = transform.localPosition; // Store the initial position
    }

    public override void OnEpisodeBegin()
    {
        ResetPlayer();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Agent's position (2 observations)
        sensor.AddObservation(transform.localPosition.x);
        sensor.AddObservation(transform.localPosition.y);

        // Agent's velocity (2 observations)
        sensor.AddObservation(rb.velocity.x);
        sensor.AddObservation(rb.velocity.y);

        // Find all enemies in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Initialize a fixed-size observation array to ensure size consistency
        float[] enemyObservations = new float[80]; // 10 enemies * 4 observations each (position x, position y, velocity x, velocity y)

        int currentObservationIndex = 0;
        for (int i = 0; i < enemies.Length && currentObservationIndex < enemyObservations.Length; i++)
        {
            if (enemies[i].TryGetComponent(out Rigidbody2D enemyRb))
            {
                enemyObservations[currentObservationIndex++] = enemies[i].transform.position.x;
                enemyObservations[currentObservationIndex++] = enemies[i].transform.position.y;
                enemyObservations[currentObservationIndex++] = enemyRb.velocity.x;
                enemyObservations[currentObservationIndex++] = enemyRb.velocity.y;
            }
        }

        // Add the fixed-size enemy observation array to the sensor
        sensor.AddObservation(enemyObservations);

        // Debug log to ensure correct number of observations
        Debug.Log("Total Observations: " + sensor.ObservationSize());
    }



    public override void OnActionReceived(ActionBuffers actions)
    {
        if (LevelManager.levelManager == null || LevelManager.levelManager.autoControlEnabled)
        {
            float moveX = Mathf.Clamp(actions.ContinuousActions[0], 0.151f, 0.845f);
            float moveY = Mathf.Clamp(actions.ContinuousActions[1], 0.081f, 0.695f);
            Vector2 movementInput = new Vector2(moveX, moveY).normalized;
            Vector2 movement = movementInput * movementSpeed;
            rb.velocity = movement;

            // Debug movement
            Debug.Log("Action Received: moveX = " + moveX + ", moveY = " + moveY);
            Debug.Log("Applied Movement: " + movement);

            // Check distance to enemies and reward for moving away
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < safeDistance)
                {
                    SetReward(-0.5f); // Penalize getting too close more significantly
                }
                else
                {
                    SetReward(0.1f); // Reward for maintaining safe distance
                }
            }

            // Check if out of bounds and apply a gradual penalty
            if (!IsWithinBounds(transform.localPosition))
            {
                SetReward(-0.1f); // Penalize slightly for being out of bounds

                Vector3 clampedPosition = new Vector3(
                    Mathf.Clamp(transform.localPosition.x, maxLeft, maxRight),
                    Mathf.Clamp(transform.localPosition.y, maxBottom, maxTop),
                    transform.localPosition.z
                );
                transform.localPosition = clampedPosition; // Move back within bounds
            }
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
        if (collision.CompareTag("Enemy") || collision.CompareTag("EnnemyProjectiles"))
        {
            AddReward(-1f);
            if (LevelManager.levelManager == null )
            {
                EndEpisode();
            }
        }

    }

    private bool IsWithinBounds(Vector3 position)
    {
        return position.x >= maxLeft && position.x <= maxRight &&
               position.y >= maxBottom && position.y <= maxTop;
    }

    private void ResetPlayer()
    {
        transform.localPosition = initialPosition; // Reset to the initial position
        rb.velocity = Vector2.zero;
    }

    private IEnumerator SetBoundaries()
    {
        yield return new WaitForSeconds(0.4f);

        maxLeft = mainCamera.ViewportToWorldPoint(new Vector2(0.15f, 0)).x;
        maxRight = mainCamera.ViewportToWorldPoint(new Vector2(0.85f, 0)).x;

        maxBottom = mainCamera.ViewportToWorldPoint(new Vector2(0, 0.08f)).y;
        maxTop = mainCamera.ViewportToWorldPoint(new Vector2(0, 0.7f)).y;
    }
}
