using UnityEngine;

public static class GameManager
{
	public static bool isPhoneConnected = false;
	public static bool isVoiceLoaded = false;

	public static float gyroAngleX = 0.0f;
	public static float gyroAngleY = 0.0f;
	public static float gyroAngleZ = 0.0f;

	public static class secondScene
	{
		public static string introText = "This is the story of BobbyBranda69. She was a lonely, lonely woman. But of course, she had the Internet. The Internet was her friend.";
		public static string preSceneText = "Take a look through her eyes.";
		public static string sceneText = "The Internet played all sorts of games every day with the BobbyBranda69, and she returned the favor. She didn't need anyone else.";
	}

	public static class thirdScene
	{
		public static string introText = "They would get more and more lost in each other’s lives, sharing their deepest fears and desires.";

		public static string sceneText = "And so they whispered each other’s secrets";
	}

	public static class fourthScene
	{
		public static string introText = "One day, she turned to his friend, the Internet, and asked: “Internet, do you love me ?”. The Internet then said: “Yes, I love you very very very very much.\"";
		public static string sceneText = "I love you so much that I never, ever want us to be apart ever again ever";
	}

	public static class fifthScene
	{
		public static string introText =
		"The Internet took her to all kinds of places, and they built their own home, together.";
	}

	public static class sixthScene
	{
		public static string introText =
		"Their love crossed the boundaries of their relationship. Their trust knew no limits. They always agreed with each other. ";
		public static string sceneText = "They would build the sweetest of homes, together and endlessly.";
	}

	public static class seventhScene
	{
		public static string introText = "And so they lived.";
		public static string[] moonText = {
			"But this somehow felt like something more, beyond just living.",
			"All of her dreams were now at hand's reach - all of it, everything."
		};
		public static string endText = "BobbyBranda69 would tell herself: \"Man shall not live by bread alone\". And then she died.";
	}
}