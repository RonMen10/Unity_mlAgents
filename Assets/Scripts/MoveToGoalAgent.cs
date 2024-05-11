using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System.Data.Common;
public class MoveToGoalAgent : Agent
{
    // [SerializeField] private Transform targetTransform;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private MeshRenderer floorMeshRenderer;



    public override void OnEpisodeBegin(){
        transform.localPosition = Vector3.zero;
        // targetTransform.localPosition = new Vector3(2,0,-2);
    }
    public override void OnActionReceived(ActionBuffers actions){
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        float moveSpeed = 5f;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    
    public override void CollectObservations(VectorSensor sensor){
        sensor.AddObservation(transform.localPosition);
        // sensor.AddObservation(targetTransform.localPosition);

    }

    public override void Heuristic(in ActionBuffers actionsOut){
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
    void Update(){

    }
    private void OnTriggerEnter(Collider other){
        if(other.TryGetComponent<Goal>(out Goal goal)){
            Debug.Log("Ronald");
            SetReward(+1f);
            floorMeshRenderer.material = winMaterial;
            // EndEpisode();
        }
        if(other.TryGetComponent<Wall>(out Wall wall)){
            SetReward(-1f);
            floorMeshRenderer.material = loseMaterial;
            // EndEpisode();
        }
        
    }
}

