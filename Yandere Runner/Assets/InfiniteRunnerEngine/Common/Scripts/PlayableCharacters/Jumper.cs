﻿using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.InfiniteRunnerEngine
{	
	public class Jumper : PlayableCharacter 
	{
		/// the vertical force applied to the character when jumping
		public float JumpForce = 20f;
		/// the number of jumps allowed
		public int NumberOfJumpsAllowed=2;
	    /// the minimum time (in seconds) allowed between two consecutive jumps
	    public float CooldownBetweenJumps = 0f;
		/// can the character jump only when grounded ?
		public bool JumpsAllowedWhenGroundedOnly;
		/// the speed at which the character falls back down again when the jump button is released
		public float JumpReleaseSpeed = 50f; 
			
		public int _numberOfJumpsLeft;
		protected bool _jumping=false;
		protected float _lastJumpTime;

		/// <summary>
		/// On Start() we initialize the last jump time
		/// </summary>
		protected override void Start()
		{
			_lastJumpTime=Time.time;
		}

		/// <summary>
		/// On update, we update the animator and try to reset the jumper's position
		/// </summary>
		protected override void Update ()
		{
			_jumping = false;

			// we determine the distance between the ground and the Jumper
			ComputeDistanceToTheGround();
			// we send our various states to the animator.      
			UpdateAnimator ();		
			// if we're supposed to reset the player's position, we lerp its position to its initial position
			ResetPosition();
			// we check if the player is out of the death bounds or not
	        CheckDeathConditions ();

			// we reset our jump variables if needed
			if (_grounded)
			{
				_jumping = false;
				if (Time.time - _lastJumpTime>0.02f)
				{
					_numberOfJumpsLeft = NumberOfJumpsAllowed;
				}
			}
		}

		/// <summary>
		/// Updates all mecanim animators.
		/// </summary>
		protected override void UpdateAllMecanimAnimators()
		{		
			MMAnimator.UpdateAnimatorBool(_animator,"Grounded",_grounded);
			MMAnimator.UpdateAnimatorBool(_animator, "Jumping", _jumping);
			MMAnimator.UpdateAnimatorFloat(_animator, "VerticalSpeed", _rigidbodyInterface.Velocity.y);
		}
		
		/// <summary>
		/// What happens when the main action button button is pressed
		/// </summary>
		public override void MainActionStart()
		{		
 			Jump();
		}

		public virtual void Jump()
		{
			// if the character is not grounded and is only allowed to jump when grounded, we do not jump
			if (JumpsAllowedWhenGroundedOnly && !_grounded)
			{
				return;
			}
			
			// if the character doesn't have any jump left, we do not jump
			if (_numberOfJumpsLeft==0)
			{
				return;
			}

	        // if we're still in cooldown from the last jump AND this is not the first jump, we do not jump
	        if ((Time.time - _lastJumpTime < CooldownBetweenJumps) && (_numberOfJumpsLeft!=NumberOfJumpsAllowed))
	        {
	            return;
			}
			_lastJumpTime=Time.time;
			// we jump and decrease the number of jumps left
			_numberOfJumpsLeft--;
			
			// if the character is falling down, we reset its velocity
			if (_rigidbodyInterface.Velocity.y < 0)
			{
				_rigidbodyInterface.Velocity = Vector3.zero;
			}
			
			// we make our character jump
			_rigidbodyInterface.AddForce(Vector3.up * JumpForce);
			MMEventManager.TriggerEvent(new MMGameEvent("Jump"));

	        _lastJumpTime = Time.time;
	        _jumping =true;
		}
		
		/// <summary>
		/// What happens when the main action button button is released
		/// </summary>
		public override void MainActionEnd()
		{
			// we initiate the descent
			StartCoroutine(JumpSlow());
		}
		
		/// <summary>
		/// Slows the player's jump
		/// </summary>
		/// <returns>The slow.</returns>
		public virtual IEnumerator JumpSlow()
		{
			while (_rigidbodyInterface.Velocity.y > 0)
			{			
				Vector3 newGravity = Vector3.up * (_rigidbodyInterface.Velocity.y - JumpReleaseSpeed * Time.deltaTime);
				_rigidbodyInterface.Velocity = new Vector3(_rigidbodyInterface.Velocity.x,newGravity.y,_rigidbodyInterface.Velocity.z);
				yield return 0;
			}
		}
		
				
	}
}