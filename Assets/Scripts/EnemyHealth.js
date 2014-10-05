#pragma strict

var Health = 100;

function ApplyDammage (TheDammage : int)
{
	Health -= TheDammage;
	Debug.Log("Enemy Health" + Health);
	if(Health <= 0)
	{
		Dead();
	}
}

function Dead()
{
	Destroy (gameObject);
}