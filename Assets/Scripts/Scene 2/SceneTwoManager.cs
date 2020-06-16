using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneTwoManager : MonoBehaviour
{
	private bool _isLoading = false;
	private Coroutine _loader = null;
	private WebSocketMessage _message;

	[SerializeField] private CrossfadeMixer crossfadeMixer;
	[SerializeField] private float crossfadeDuration = 4.0f;
	[SerializeField] private Fade fade;
	[SerializeField] private GameObject canvasUI = null;
	[SerializeField] private TextMeshProUGUI objectiveText = null;
	[SerializeField] private Animator whitePhoneAnimator = null;
	
	void Awake()
	{
		_message = new WebSocketMessage();
		GameManager.secondScene();
	}

	void Update()
	{
		if (GameManager.changedScene && !_isLoading) { 
			GameManager.changedScene = false;
			setObjective(-1);
			LoadScene("Third Scene");
		}

		if(Input.GetMouseButtonDown(1)){
			SendMessage("readyForNextScene", "{\"from\":\"0\", \"to\":\"0\"}");
		}
	}

	void setObjective(int objectiveIndex)
	{
		if (objectiveIndex > -1)
		{
			objectiveText.SetText(GameManager.objectiveTexts[objectiveIndex]);
			whitePhoneAnimator.SetBool("isInactive", false);
			whitePhoneAnimator.SetBool("isActive", true);
		}
		else
		{
			objectiveText.SetText("");
			whitePhoneAnimator.SetBool("isActive", false);
			whitePhoneAnimator.SetBool("isInactive", true);
		}
	}

	void SendMessage(string type, string message)
	{
		if (WebSocketClient.Instance != null)
		{
			_message = new WebSocketMessage();
			_message.id = GameManager.name;
			_message.type = type; //"readyForNextScene" , "sound";
			_message.message = message; //"{\"from\":\"0\", \"to\":\"0\"}", "{\"soundname\":\"notif\"}";

			WebSocketClient.Instance.Send(_message);
		}
	}

	void LoadScene(string name)
	{
		//fade music
		// crossfadeMixer.CrossfadeGroups("volPadLow", "volPadHigh", 2f);

		if (_loader != null)
		{
			//Debug.LogWarning("Scene load already in progress. Will not load.");
			return;
		}

		_isLoading = true;
		_loader = StartCoroutine(AsyncLoader(name));
	}

	public IEnumerator AsyncLoader(string name)
	{
		fade.FadeOut();
		yield return new WaitForSeconds(2f);

		// The Application loads the Scene in the background as the current Scene runs.
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
	}
}