using System.Collections;
using UnityEngine;
using Cinemachine;
using TMPro;
using RedBlueGames.Tools.TextTyper;

public class InitSceneTwo : MonoBehaviour
{
	private WebSocketMessage _message;
	private AudioSource[] _musicSources = new AudioSource[5];
	[SerializeField] private CinemachineVirtualCamera[] cams; //Start cam, Scene cam
	[SerializeField] private GameObject[] assets; //appt, phone, phone beam, character,
	[SerializeField] private GameObject[] lights; //spotlight and beam, point light 1, directional light
	[SerializeField] private GameObject titleAnimation;
	[SerializeField] private TextToSpeech[] textToSpeechArr;
	[SerializeField] private TextTyperTalker textTyper = null;
	[SerializeField] private GameObject textCanvas = null;
	[SerializeField] private Camera mainCamera = null;
	[SerializeField] private GyroRotate gRotate = null;
	[SerializeField] private LookAtTarget lookAt = null;
	[SerializeField] private GameObject head = null;
	[SerializeField] private CrossfadeMixer crossfadeMixer = null;
	[SerializeField] private GameObject canvasUI = null;
	[SerializeField] private TextMeshProUGUI objectiveText = null;
	[SerializeField] private float delay = 0.032f;
	[SerializeField] private TextTyper textTyperComponent;
	[SerializeField] private Fade fade;
	[SerializeField] private GameObject whitePhoneUI = null;
	[SerializeField] private GameObject logoUI = null;
	[SerializeField] private Animator whitePhoneAnimator = null;

	void Start()
	{
		GetMusic();

		//black
		mainCamera.backgroundColor = new Color(0f, 0f, 0f, 1f);

		//hide everything
		toggleGameObjects(assets, false);
		toggleGameObjects(lights, false);
		titleAnimation.SetActive(false);

		//reset text
		textToSpeechArr[0].text = GameManager.introText;
		textToSpeechArr[1].text = GameManager.preSceneText;
		textToSpeechArr[2].text = GameManager.sceneText;
		textTyper.text = GameManager.introText;

		//disable scripts
		gRotate.enabled = false;
		lookAt.enabled = false;

		//start animation
		StartCoroutine(InitPartOne());
	}

	void Update()
	{
		if (GameManager.hasSlid || Input.GetKey(KeyCode.DownArrow))
		{
			StartCoroutine(InitPartTwo());
		}
	}

	void GetMusic()
	{
		_musicSources[0] = GameObject.FindWithTag("music_intro").GetComponent<AudioSource>();
		_musicSources[1] = GameObject.FindWithTag("music_avant_chute").GetComponent<AudioSource>();
		_musicSources[2] = GameObject.FindWithTag("music_chute_portable").GetComponent<AudioSource>();
		_musicSources[3] = GameObject.FindWithTag("music_apres_chute").GetComponent<AudioSource>();
		_musicSources[4] = GameObject.FindWithTag("music_fin").GetComponent<AudioSource>();
	}

	void toggleGameObjects(GameObject[] gObjects, bool toggle)
	{
		foreach (var gObject in gObjects)
		{
			gObject.SetActive(toggle ? true : false);
		}
	}

	void toggleTyper(bool toggle)
	{
		textTyper.enabled = toggle ? true : false;
		textCanvas.SetActive(toggle ? true : false);
	}

	IEnumerator InitPartOne()
	{
		yield return new WaitForSeconds(1f);
		textToSpeechArr[0].enabled = true;

		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		toggleTyper(true);

		yield return new WaitForSeconds(12.0f);
		toggleTyper(false);

		//show phone and character
		assets[1].SetActive(true);
		assets[3].SetActive(true);
		toggleGameObjects(lights, true);
		textToSpeechArr[1].enabled = true;
		canvasUI.SetActive(true);
		whitePhoneUI.SetActive(true);
		setObjective(0);
		SendMessage("readyToSwipe", "{\"from\":\"0\", \"to\":\"0\"}");
	}

	IEnumerator InitPartTwo()
	{
		GameManager.hasSlid = false;

		//start looking at phone
		lookAt.enabled = true;
		//turn on gyro
		gRotate.enabled = true;
		//turn on phone
		assets[2].SetActive(true);
		setObjective(-1);
		//fade music
		_musicSources[1].Play();
		crossfadeMixer.Crossfade("intro", 1.5f, "avant_chute");

		yield return new WaitForSeconds(0.6f);
		//show intro animation
		titleAnimation.SetActive(true);

		yield return new WaitForSeconds(8.5f);
		//switch cams
		cams[1].m_Priority = 3;
		//show appartment
		assets[0].SetActive(true);
		//bg yellow
		mainCamera.backgroundColor = new Color(0.82f, 0.6979567f, 0.51168f, 1f);
		//disable anim
		titleAnimation.SetActive(false);
		//show logo
		logoUI.SetActive(true);
		//send readyForNextScene
		SendMessage("readyForNextScene", "{\"from\":\"0\", \"to\":\"0\"}");
		//talk
		textToSpeechArr[2].enabled = true;
		//fade
		crossfadeMixer.Crossfade("avant_chute", 3f, null, 1, 0.65f);
		GameManager.isIntro = false;

		yield return new WaitForSeconds(8.5f);
		setObjective(1);
		_musicSources[0].Stop();
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
			_message.type = type; //"readyForNextScene"
			_message.message = message; //"{\"from\":\"0\", \"to\":\"0\"}"

			WebSocketClient.Instance.Send(_message);
		}
	}
}
