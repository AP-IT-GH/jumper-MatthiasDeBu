using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour
{

    public float Movespeed = 3.5f;
    public float Movespeed2 = 50.5f;
    public GameObject agent;

    private void Update()
    {
        
       
        this.transform.Translate(Vector3.back * Movespeed * Time.deltaTime);
       
        

        if(this.transform.localPosition.y < 0)
        {
            agent.GetComponent<CubeAgent>().EndEpisode();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ObstacleDestination") == true)
        {
            agent.GetComponent<CubeAgent>().AddReward(1f);           
        }
    }

    


}
