using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	[SerializeField] public TMP_Text beginScreenText = null;

	private StateMachine _stateMachine = new StateMachine();

	public void Start()
	{
		_stateMachine.ChangeState(new StartState(this));
	}

	// void Update()
	// {
	// 	stateMachine.Update();
	// }
}