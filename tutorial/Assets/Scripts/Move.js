var target: Transform;
var moveSpeed: float = 1.0;
var rotationSpeed: float = 30;

function FixedUpdate () {
	var z=Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
	var y=Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
	transform.Translate(0,0,z);
	if (Input.GetAxis("Vertical")) print("moving forward");
transform.Rotate(0, y, 0);
}