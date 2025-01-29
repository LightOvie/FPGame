using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerMoveement : MonoBehaviour
{
	const float gravity = -9.81f;

	public float speed = 5f;
	public float sprintSpeed = 10f;
	public float jumpHeight = 2f;

	private CharacterController controller;
	private Vector3 velocity;

	private float stepDelay = 0.5f;
	private float stepTimer = 0f;

	[SerializeField] private AudioClip[] walkClips;



	bool isSprinting = false;
	private void Start()
	{
		controller = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update()
	{


		float moveX = Input.GetAxis("Horizontal");
		float moveZ = Input.GetAxis("Vertical");

		isSprinting = Input.GetKey(KeyCode.LeftShift);
		float currentSpeed = isSprinting ? sprintSpeed : speed;
		Vector3 move = transform.right * moveX + transform.forward * moveZ;
		controller.Move(move * speed * Time.deltaTime);



		if (controller.isGrounded && velocity.y < 0)
		{
			velocity.y = -2f;
		}

		if (Input.GetButtonDown("Jump") && controller.isGrounded)  // Fixe it so Player can jump when he want it 
		{
			velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
		}
		velocity.y += gravity * Time.deltaTime;


		controller.Move(velocity * Time.deltaTime);


		HandleFootstepSounds(move);
	}


	private void HandleFootstepSounds(Vector3 move)
	{
		if (controller.isGrounded && move.magnitude > 0.1f)
		{
			stepDelay = isSprinting ? 0.35f : 0.5f;
			stepTimer -= Time.deltaTime;
			if (stepTimer <= 0f)
			{
				PlayFootstepSound();
				stepTimer = stepDelay;
			}
		}
		else
		{
			stepTimer = 0f;
		}
	}

	private void PlayFootstepSound()
	{
		
		SoundFXManager.instance.PlayRandomSoundFXClip(walkClips, transform, 1f);

	}
}
