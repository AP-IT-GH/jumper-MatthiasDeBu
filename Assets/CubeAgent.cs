using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class CubeAgent : Agent
{

    public Transform Obstacle;
    public Transform ObstacleDestination;
    public Transform Obstacle2;
    public Transform ObstacleDestination2;


    public float Force = 7f;
    public bool targetTouched = false;
    public bool isJumping = false;
    private Rigidbody rb = null;

    // Start is called before the first frame update
    public override void OnEpisodeBegin()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        targetTouched = false;
        //reset dep ositie en orientatie als de agent gevallen is
        if (this.transform.localPosition.y < 0)
        {
            this.transform.localPosition = new Vector3(0, 0.5f, 0);
            this.transform.localRotation = Quaternion.identity;
        }
        // verplaats de target naar een nieuwe willekeurige locaite
        Obstacle.localPosition = new Vector3(0f, 0.62f, 16.48f);
        Obstacle2.localPosition = new Vector3(-16.92f, 0.62f, -0.16f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //Target en agent posities
        //sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        if (this.transform.localPosition.y < 0)
        {
            EndEpisode();
        }
    }

    public float speedMultiplier = 10f;
    public float rotationSpeed = 300f;
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {

        // acties, size = 2
        if (actionBuffers.DiscreteActions[0] == 1)
        {
            Jump();

        }

        if (actionBuffers.DiscreteActions[0] == 0)
        {
            AddReward(0.001f);

        }

        // van het platform gevallen?

    }


    // Beloningen
    public void OnCollisionEnter(Collision collision)
    {
        
        print("collided");
        if(collision.gameObject.CompareTag("Obstacle") == true)
        {        
            //Destroy(collision.gameObject);
            AddReward(-1f);
            print("Obstacle touched");
            EndEpisode();
        }

        if(isJumping == true)
        {
            if (collision.gameObject.CompareTag("Plane") == true)
            {
                isJumping = false;
                print("Plane touched");
            }
        }
        



    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;


        discreteActionsOut[0] = 0;
        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            discreteActionsOut[0] = 1;
        }

   

    }

    private void Jump()
    {
        if(isJumping == false)
        {
            rb.AddForce(Vector3.up * Force, ForceMode.Impulse);
            isJumping = true;
        }
        
    }


}
