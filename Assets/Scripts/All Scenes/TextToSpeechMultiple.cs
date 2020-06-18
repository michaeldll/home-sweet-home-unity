using UnityEngine;
using FrostweepGames.Plugins.GoogleCloud.TextToSpeech;

public class TextToSpeechMultiple : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;
	public string text = "default text please change";
	[SerializeField] private string voiceName = "en-US-Wavenet-C";
	[SerializeField] private string languageName = "en_US";
	[SerializeField] Enumerators.SsmlVoiceGender genderName = Enumerators.SsmlVoiceGender.FEMALE;

	private GCTextToSpeech _gcTextToSpeechInstance;

	void Awake()
	{
		GameManager.isVoiceLoaded = false;

		//get singleton instance
		_gcTextToSpeechInstance = GCTextToSpeech.Instance;

		//subscribe to events
		_gcTextToSpeechInstance.SynthesizeSuccessEvent += _gcTextToSpeech_SynthesizeSuccessEvent;
		_gcTextToSpeechInstance.SynthesizeFailedEvent += _gcTextToSpeech_SynthesizeFailedEvent;

		// Debug.Log("debug");
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
		GameManager.isVoiceLoaded = true;
		audioSource.Play();
		Invoke("ResetVoiceLoaded", 0.2f);
	}
	#endregion sucess handlers

	public void TrySpeak()
	{
		if (string.IsNullOrEmpty(text))
			return;

		GameManager.isVoiceLoaded = false;

		//get singleton instance
		_gcTextToSpeechInstance = GCTextToSpeech.Instance;

		_gcTextToSpeechInstance.Synthesize(text, new VoiceConfig()
		{
			gender = genderName,
			languageCode = languageName,
			name = voiceName
		});
	}

	private void ResetVoiceLoaded()
	{
		GameManager.isVoiceLoaded = false;
	}
}
