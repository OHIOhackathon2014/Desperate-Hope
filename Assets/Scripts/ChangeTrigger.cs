using UnityEngine;
using System.Collections;

public class ChangeTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		
		// Is this a shot?
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
		if (shot != null)
		{
			shot.collider.isTrigger = false;
		}
	}
}
