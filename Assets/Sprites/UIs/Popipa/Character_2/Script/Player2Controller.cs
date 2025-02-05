using UnityEngine;
using System.Collections;

public class Player2Controller : MonoBehaviour
{
	
	public Animator anim;           

	// Controls facing direction
	public bool facingRight;

	// Use this for initialization
	void Start()
	{
		

	}

	public void Escape()
	{
		anim.SetTrigger("EscapeTrigger");
	}

	//public void EscapeOff()
	//{
	//	anim.SetBool("Escape", false);
	//}

	
	public void Run()
	{
		anim.SetBool("isRun", true);
	}

	public void RunOff()
	{
		anim.SetBool("isRun", false);
	}
	public void Doll()
	{
		anim.SetTrigger("DollTrigger");
	}
	//public void DollOff()
	//{
	//	anim.SetBool("Doll", false);
	//}
	public void Attack()
	{
		anim.SetTrigger("AttackTrigger");
	}
	//public void AttackOff()
	//{
	//	anim.SetBool("Attack", false);
	//}





	// Update is called once per frame
	void Update()
	{



	} // Closes Update()

	// Function to flip Character in direction it's moving
	

}



