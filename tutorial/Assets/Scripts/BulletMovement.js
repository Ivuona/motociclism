var missile: Rigidbody;
var speed: float = 30.0;
var ammoCount: float = 20;
function Update () {
	if (Input.GetButtonDown("Fire1") && ammoCount > 0) {
		var instantiatedMissile : Rigidbody = Instantiate(missile, transform.position+Vector3(0,1.5,0), transform.rotation);
		Physics.IgnoreCollision(instantiatedMissile.collider, transform.root.collider);	
		instantiatedMissile.velocity = transform.TransformDirection(Vector3 (0,0,speed)); ammoCount--;
		if (ammoCount == 0) Debug.Log("out of ammo");
	}
}