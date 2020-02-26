using UnityEngine;

public class StartState : IState
{
	GameManager owner;

	public StartState(GameManager owner) { this.owner = owner; }

	public void Enter()
	{
		Debug.Log("entering start state");
	}

	public void Execute()
	{
		Debug.Log("updating start state");
	}

	public void Exit()
	{
		Debug.Log("exiting start state");
	}
}