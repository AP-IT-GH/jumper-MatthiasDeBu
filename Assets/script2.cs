using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script2 : MonoBehaviour
{

    public float Movespeed = 1.8f;
    public GameObject agent;

    private void Update()
    {

        this.transform.Translate(Vector3.back * Movespeed * Time.deltaTime);


        if (this.transform.localPosition.y < 0)
        {
            agent.GetComponent<CubeAgent>().EndEpisode();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.CompareTag("ObstacleDestination2") == true)
        {
            agent.GetComponent<CubeAgent>().AddReward(1f);
            agent.GetComponent<CubeAgent>().EndEpisode();
        }
    }




}
