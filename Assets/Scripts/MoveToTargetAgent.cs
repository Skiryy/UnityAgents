using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System;

public class MoveToTargetAgent : Agent
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform target2;
    [SerializeField] private Transform target3;
    [SerializeField] private Transform target4;
    [SerializeField] private Transform target5; 


    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(UnityEngine.Random.Range(-45f, 45f), 2f, UnityEngine.Random.Range(45f, -45f));
        target.localPosition = new Vector3(UnityEngine.Random.Range(-45f, 45f), 3f, UnityEngine.Random.Range(45f, -45f));
        target2.localPosition = new Vector3(UnityEngine.Random.Range(-45f, 45f), 3f, UnityEngine.Random.Range(45f, -45f));
        target3.localPosition = new Vector3(UnityEngine.Random.Range(-45f, 45f), 3f, UnityEngine.Random.Range(45f, -45f));
        target4.localPosition = new Vector3(UnityEngine.Random.Range(-45f, 45f), 3f, UnityEngine.Random.Range(45f, -45f));
        target5.localPosition = new Vector3(UnityEngine.Random.Range(-45f, 45f), 3f, UnityEngine.Random.Range(45f, -45f));
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation((Vector3)transform.localPosition);
        sensor.AddObservation((Vector3)target.localPosition);
        //base.CollectObservations(sensor);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        //float moveY = actions.ContinuousActions[1];
        float moveY = 0f;
        float moveZ = actions.ContinuousActions[1];
        float movementSpeed = 5f;
        transform.localPosition += new Vector3(moveX, moveY, moveZ) * Time.deltaTime * movementSpeed;
        //base.OnActionReceived(actions);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AddReward(10f);
            Debug.Log("Made it!");
            EndEpisode();
        }
        else if (collision.gameObject.tag == "Walls") {
            AddReward(-2);
            Debug.Log("Failed!");
            EndEpisode();
        }
    }
}
