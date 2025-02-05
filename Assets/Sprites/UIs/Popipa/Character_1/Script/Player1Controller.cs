using UnityEngine;
using System.Collections;

public class Player1Controller : MonoBehaviour
{

	public Animator anim;

	// Controls facing direction
	public bool facingRight;

	// Use this for initialization
	void Start()
	{


	}
	 
	public void Jump()
    {
		anim.SetTrigger("JumpTrigger") ;
	}

	//public void JumpOff()
 //   {
	//	anim.SetBool("Jump", false);
	//}

	public void Dead()
	{
		anim.SetTrigger("DeadTrigger");
	}

	//public void DeadOff()
	//{
	//	anim.SetBool("Dead", false);
	//}
	public void Walk()
	{
		anim.SetBool("isWalk" , true);
	}

	public void WalkOff()
	{
		anim.SetBool("isWalk", false);
	}
	public void Run()
	{
		anim.SetBool("isRun" , true);
	}
	public void RunOff()
	{
		anim.SetBool("isRun", false);
	}
	public void Attack()
	{
		anim.SetTrigger("AttackTrigger");
	}
	//public void AttackOff()
	//{
	//	anim.SetBool("Attack", false);
	//}





	void Update()
	{


		



	} 


}



