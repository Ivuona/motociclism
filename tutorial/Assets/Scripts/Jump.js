var vel: float =5.0;

function FixedUpdate () {
if (Input.GetButtonDown("Jump")&& (rigidbody.velocity.y<1))
rigidbody.velocity.y +=vel;
}