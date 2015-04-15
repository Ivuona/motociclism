var bullet: GameObject;
var expRadius = 5;
var expForce = 2000;
var expTime = 1;

//function Start () {

//	var colliders: Collider[] = Physics.OverlapSphere(bullet.position, expRadius);
//	if (hit.rigidbody) {
//		hit.rigidbody.AddExplosionForce(ExplosionPower, bullet.position, expRadius);
//	}
	
//}

function Update () {

Physics.IgnoreCollision(bullet.collider, GameObject.Find("Player").collider);
}

function OnCollisionEnter () {
	Destroy(bullet,expTime);
}