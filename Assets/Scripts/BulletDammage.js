#pragma strict

var Dammage = 100;

function  OnCollisionEnter2D(coll: Collision2D) 
{
			Debug.Log("Test");
			coll.gameObject.SendMessage("ApplyDamage", 100,SendMessageOptions.DontRequireReceiver );
	
}