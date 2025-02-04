using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IDamageable
{

	EnemyStats _state = EnemyStats.Chase;

	Transform targetTRansform;
	IDamageable _target;
	Animator _animator;
	CharacterController _characterController;

	[SerializeField] float _speed;
	[SerializeField] float _rotationSpeed;
	[SerializeField] float _maxHealth;
	[SerializeField] float _attackCooldown;

	[Header("Drop")]
	[SerializeField]
	GameObject ammoPrefab;
	[SerializeField]
	Transform spawnPosition;
	float chanceToDrop = 50f;

	[Header("Damage Text")]
	[SerializeField] GameObject damageTextPrefab;
	[SerializeField] Transform textSpawnPoint;

	[Header("Audio")]
	[SerializeField] private AudioClip damageSoundClip;

	public float Health { get; set; }


	float _attackCooldownTimer;

	float _verticalVelocity;
	float _gravity = 10f;


	private bool isDead = false;


	private void Start()
	{
		Health = _maxHealth;
		_target = GameObject.FindGameObjectWithTag("Player").GetComponent<IDamageable>();
		targetTRansform = (_target as MonoBehaviour).transform;
		_characterController = GetComponent<CharacterController>();
		_animator = GetComponent<Animator>();

	}

	private void Update()
	{

		if (isDead) return;

		if (_state == EnemyStats.Chase)
		{
			ChaseTarget();

		}

		if (_state == EnemyStats.Chase || _state == EnemyStats.Attack)
		{
			FaceTarget();
		}

		if (_state == EnemyStats.Attack)
		{
			AttackPlayer();
		}
	}

	private void AttackPlayer()
	{
		_animator.SetBool("IsAttack", true);
	}

	void ChaseTarget()
	{

		if (isDead)
		{
			return;
		}

		Vector3 direction = (targetTRansform.position - transform.position).normalized;

		if (!_characterController.isGrounded)
		{
			_verticalVelocity -= _gravity * Time.deltaTime;
		}

		direction.y = _verticalVelocity;
		if (_characterController.enabled)
		{
			_characterController.Move(direction * Time.deltaTime * _speed);
		}
	}

	void FaceTarget()
	{
		if (targetTRansform == null || isDead) return;

		Vector3 direction = (targetTRansform.position - transform.position).normalized;

		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));// ОШИБКА ТУТ 

		Quaternion correction = Quaternion.Euler(0, 180, 0);
		lookRotation *= correction;


		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);

	}
	

	public void TakeDamage(float damage)
	{
		Health -= damage;
		ShowDamageText(damage);
		SoundFXManager.instance.PlaySoundFXClip(damageSoundClip, transform,0.25f);

		if (Health <= 0)
		{

			Die();
		}
	}



	void ShowDamageText(float damage)
	{

		if (damageTextPrefab != null && textSpawnPoint != null)
		{
			GameObject damageText = Instantiate(damageTextPrefab, textSpawnPoint.transform.position, Quaternion.identity);

			damageText.GetComponent<DamageText>().SetDamage(damage);

			damageText.transform.SetParent(GameObject.Find("Canvas").transform, false);
		}
	}
	void SpawnAmmo()
	{
		if (ammoPrefab == null || spawnPosition == null)
		{

			return;

		}


		float randomValue = Random.Range(0, 100f);

		if (randomValue <= chanceToDrop)
		{
			Instantiate(ammoPrefab, spawnPosition.position, Quaternion.identity);
		}
		else
		{
			Debug.Log("Random value:"+randomValue.ToString());
		}
	}
	public void Die()
	{
		_speed = 0;
		_animator.SetBool("IsDead", true);

		if (GameManager.instance != null)
		{
			GameManager.instance.InccreadeDeadEnimies();
		}

		isDead = true;


		if (_characterController != null)
		{
			_characterController.enabled = false;
		}
		Destroy(gameObject, 1.5f);
		SpawnAmmo();
	}

	public void SetState(EnemyStats newState)
	{
		if (_state == newState)
			return;

		ExitState(_state);
		_state = newState;
		EnterState(newState);
	}

	private void ExitState(EnemyStats state)
	{
		switch (state)
		{
			case EnemyStats.Chase:
				_animator.SetBool("isWalking", false);
				break;

			case EnemyStats.Attack:
				_animator.SetBool("IsAttack", false);
				break;
		}
	}

	private void EnterState(EnemyStats state)
	{
		switch (state)
		{
			case EnemyStats.Chase:
				_animator.SetBool("isWalking", true);
				break;

			case EnemyStats.Attack:
				_attackCooldownTimer = _attackCooldown;
				_animator.SetBool("IsAttack", true);
				break;
		}
	}
}
