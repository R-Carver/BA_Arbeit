using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MyAI : MonoBehaviour {

	public Transform targetPosition;
	Seeker seeker;
	private CharacterController controller;

	//The calculated path
	public Path path;

	//The AI's speed in meters per second
	public float speed = 2;

	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 3;

	//The waypoint we are currently moving towards
	private int currentWaypoint = 0;

	//How often to recalculate the path (in seconds)
	public float repeathRate = 0.5f;
	private float lastRepath = -9999;

	// Use this for initialization
	void Start () {
		seeker = GetComponent<Seeker>();
		controller = GetComponent<CharacterController>();

	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - lastRepath > repeathRate && seeker.IsDone()){
			lastRepath = Time.time + Random.value*repeathRate*0.5f;

			seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
		}

		if(path == null){
			//We have no path to follow so don't do anything
			return;
		}

		if(currentWaypoint > path.vectorPath.Count) return;
		if(currentWaypoint == path.vectorPath.Count){
			Debug.Log("End of Path Reached");
			currentWaypoint++;
			return;
		}

		//Direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
		dir *= speed;
		//Note that SimpleMove takes a velocity in meters/second, so we should not multiply by Time.deltaTime
		controller.SimpleMove(dir);

		if((transform.position - path.vectorPath[currentWaypoint]).sqrMagnitude < nextWaypointDistance * nextWaypointDistance){
			currentWaypoint++;
			return;
		}

	}

	void OnDisable(){
		seeker.pathCallback -= OnPathComplete;
	}


	public void OnPathComplete(Path p){
		Debug.Log("Yay, we got a path back. Did it have an error?" + p.error);
		if(!p.error){
			path = p;
			//Reset the waypoint counter so that we start to move towards the first point on the path
			currentWaypoint = 0;
		}
	}
}
