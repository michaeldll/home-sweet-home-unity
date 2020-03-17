using UnityEngine;
using FrostweepGames.Plugins.GoogleCloud.TextToSpeech;

public class TextToSpeech : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;
	[SerializeField] string text = GameManager.textOne;
	[SerializeField] string voiceName = "en-US-Wavenet-C";
	[SerializeField] string languageName = "en_US";
	[SerializeField] Enumerators.SsmlVoiceGender genderName = Enumerators.SsmlVoiceGender.FEMALE;

	private GCTextToSpeech _gcTextToSpeechInstance;
	private Voice[] _voices;
	private Voice _currentVoice;

	void Start()
	{
		//get text
		text = GameManager.textOne;

		//get singleton instance
		_gcTextToSpeechInstance = GCTextToSpeech.Instance;

		//subscribe to events
		_gcTextToSpeechInstance.SynthesizeSuccessEvent += _gcTextToSpeech_SynthesizeSuccessEvent;

		_gcTextToSpeechInstance.SynthesizeFailedEvent += _gcTextToSpeech_SynthesizeFailedEvent;

		initSpeak();
	}

	#region failed handlers

	private void _gcTextToSpeech_SynthesizeFailedEvent(string error)
	{
		Debug.Log(error);
	}

	#endregion failed handlers

	#region sucess handlers

	private void _gcTextToSpeech_SynthesizeSuccessEvent(PostSynthesizeResponse response)
	{
		audioSource.clip = _gcTextToSpeechInstance.GetAudioClipFromBase64(response.audioContent, Constants.DEFAULT_AUDIO_ENCODING);
		audioSource.Play();
	}

	private void initSpeak()
	{
		if (string.IsNullOrEmpty(text))
			return;

		_gcTextToSpeechInstance.Synthesize(text, new VoiceConfig()
		{
			gender = genderName,
			languageCode = languageName,
			name = voiceName
		});
	}


	#endregion sucess handlers
}
