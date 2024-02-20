using UnityEngine;

namespace player
{
	public class PlayerMovement : MonoBehaviour
	{
		[Header("Movement")]
		[SerializeField] [ReadOnly] private float moveSpeed;
		[SerializeField] private float walkSpeed;
		[SerializeField] private float runSpeed;
		public float RunSpeed{
			get
			{
				if(_rb.velocity.x != 0 && _rb.velocity.z != 0) UseStamina();
				return runSpeed;
			}
			set
			{
				runSpeed = value;
			}
		}
		[SerializeField] private float groundDrag;

		[Header("Stamina")]
		[SerializeField] [Min(0)] private float maxStamina;
		[SerializeField] [ReadOnly] private float stamina;
		private float _staminaTime;

		[Header("Jump")]
		[SerializeField] private float jumpForce;
		[SerializeField] private float jumpCooldown;
		[SerializeField] private float airMultiplier;
		private bool _readyToJump;

		[Header("Keybinds")]
		[SerializeField] private KeyCode jumpKey = KeyCode.Space;
		[SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;


		[Header("Ground Check")]
		[SerializeField] private float playerHeight;
		[SerializeField] private LayerMask whatisGround;
		private bool _isGrounded;

		[SerializeField] private Transform orientation;

		private float _horizontalInput;
		private float _verticalInput;

		private Vector3 _moveDirection;

		private Rigidbody _rb;

		private void Start() 
		{
			_rb = GetComponent<Rigidbody>();
			_rb.freezeRotation = true;
			_readyToJump = true;
			stamina = maxStamina;
		}

		private void Update() 
		{
			_isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatisGround);

			MyInput();
			SpeedControl();
			RegenStamina();

			if(_isGrounded)
				_rb.drag = groundDrag;
			else 
				_rb.drag = 0;
		} 

		private void FixedUpdate() 
		{
			MovePlayer();
		}

		private void MyInput()
		{
			_horizontalInput = Input.GetAxisRaw("Horizontal");
			_verticalInput = Input.GetAxisRaw("Vertical");

			if(Input.GetKey(jumpKey) && _readyToJump && _isGrounded)
			{
				_readyToJump = false;

				Jump();

				Invoke(nameof(ResetJump), jumpCooldown);
			}
		}

		private void MovePlayer()
		{
			_moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;
			moveSpeed = Input.GetKey(sprintKey) && stamina > 0 ? RunSpeed : walkSpeed;

			if(_isGrounded)
				_rb.AddForce(_moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
			else if(!_isGrounded)
				_rb.AddForce(_moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
		}

		private void SpeedControl(){
			Vector3 flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

			if(flatVel.magnitude > moveSpeed)
			{
				Vector3 limitedVel = flatVel.normalized * moveSpeed;
				_rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
			}
		}

		private void Jump(){
			_rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

			_rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

		}

		private void ResetJump(){
			_readyToJump = true;
		}

		private void RegenStamina(){
			if(_staminaTime < 2f)
			{
				_staminaTime += Time.deltaTime;
			}
			else if(stamina < maxStamina)
			{
				stamina += 10 * Time.deltaTime;
			}
		}

		private void UseStamina(){
			_staminaTime = 0;
			stamina -= 20 * Time.deltaTime;
		}
	}
}
