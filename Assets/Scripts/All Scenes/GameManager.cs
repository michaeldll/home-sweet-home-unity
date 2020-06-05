using UnityEngine;
public static class ArrayExtensions
{
	// This is an extension method. RandomItem() will now exist on all arrays.
	public static T RandomItem<T>(this T[] array)
	{
		return array[Random.Range(0, array.Length)];
	}
}

public static class GameManager
{
	public static bool isPhoneConnected = false;
	public static bool isVoiceLoaded = false;
	public static int phoneCurrentScene = 0;
	public static float gyroAngleX = 0.0f;
	public static float gyroAngleY = 0.0f;
	public static float gyroAngleZ = 0.0f;
	public static string name = "SnowflakeSmasher";
	public static string gender = "male";
	public static string introText;
	public static string preSceneText;
	public static string sceneText;
	public static string secondSceneText;
	public static string thirdSceneText;
	public static string[] moonText = { "", "" };
	public static string[] endText = { "", "" };
	// public static string[] maleFirstNames = { "gonc", "henry", "valentin", "artey", "ave", "ouri", "marius", "michael" };
	// public static string[] maleSecondNames = { "inity", "_bzrd", ".dll", "miramont10", "_kevin", ".levin", "ballot" };
	// public static string[] femaleFirstNames = { "mogo", "sol", "mog", "set", "cha", "saana", "marie", "chloro", "interieur", "julie", "m" };
	// public static string[] femaleSecondNames = { "so9", "_bzrd", ".zilla", "ballot", ".levin", "sans", "less", "ouilla_", "jh", };
	public static string[] userNames = {
		"SnowflakeSmasher86",
		"boggybrendan69",
		"larryisgood20",
		"boogie2988",
		"lobbybobby88",
		"funkybandit",
		"lincostinko",
		"SirCaesar29",
		"tamwebb90",
		"oceanicheader77",
		"FuzzyCurator",
		"greytossing",
		"awakehacker36",
		"CrispVocalist",
		"unknowncyclist82",
		"dustydomination",
		"BlueHedgehog",
		"optimaltrauma8",
		"tangibleabbey89",
		"LoneMouse",
		"Frenchcorpus2",
		"uprightrashfluke",
		"demonichockey",
		"iamthedevil666",
		"TheEnragedBrunt",
		"sackoftongues",
		"littlefish97",
		"LividKangaroo",
		"InsistentDiver",
		"fertilecoomer",
		"throwaway38",
		"2honored4u",
		"zonkedbonus558",
		"softprecedence74",
		"AveragePanther",
		"disloyalnovella",
		"baggyslugger58",
		"TautBoldness",
		"virtualaviation57",
		"CoolMole",
		"PepperySpectre",
		"QuietCheerleader",
		"HappyFetish",
		"dustymeantime86",
		"WhollyTrained",
		"GratefulDemon6",
		"generoustendon7",
		"AcclaimedDentist",
		"raspyperusal17",
		"dizzyjoseph",
		"evelknievel01",
		"ChubbyScrum",
		"countdankula",
		"acceptableenvoy17",
		"adhesiveduck",
		"sickpathos74",
		"emptynarrator75",
		"DoomSlayer",
		"samhyde404",
		"goldenphosphorus",
		"emptynarrator75",
		"LittleThriller",
		"wavyfunction11",
		"dustyman",
		"CraftyNoodle",
		"saladass555",
		"tomamoto",
		"uglymeanman",
		"bouncytphone17",
		"CoollySteel",
		"snottybullion15",
		"DopeyLlama",
		"bothmatrix",
		"lavishsnark",
		"LoudlyHallowed",
		"sugardaddy69",
		"LonelyMan12",
		"tastysushi",
		"OmegaMind04",
		"throwaway1001",
		"TinyAnt",
		"giddyfastball70",
		"noiselessoverseer",
		"CandidSchooner",
		"toxicwasteXxX",
		"AuthenticApe",
		"unemployedloser00",
		"welcomeranger37",
		"TalentedCat",
		"w0rmhead",
		"LumpyTouch",
		"blankprovider21",
		"WorriedRecreation",
		"TheVapidHorseman",
		"robloxfantasy",
		"wingedconstable6",
		"FlimsyPneumonia",
		"frantichands",
		"SadPhilosopher",
		"wholeheader3",
		"JubilantRalph",
		"totalunderdog74",
		"loungelizard",
		"BasicDonkey",
		"quaintposter75",
		"mightyliner83",
		"deadlypottery8",
		"FabulousListener",
		"PastJuncture",
		"GutturalMartin",
		"chiefdumps733",
		"thefullmayhem",
		"TheUsedBasis",
		"ObliviousDriver",
		"wobblylikeness81",
		"vengefuleater",
		"GapingHole",
		"throwaway1002",
		"properwords642",
		"CraftyNuisance",
		"radiantartisan71",
		"TheLameRecipe",
		"glumcharade32",
		"LonelyScholarship",
		"GentleGiant",
		"FaithfulHamburger",
		"lonelyBird",
		"CalmCamel",
		"starchywidth72",
		"RoomyBroccoli",
		"flickeringfluke",
		"stickybasin",
		"snobbishhands2",
		"LazyRock",
		"superbjoseph",
	};
	public static string[] objectiveTexts = {
		"Swipe up to see through his eyes",
		"Throw the ball twice to play with the Internet",
		"Throw it again",
		"Gently blow on your phone to seduce the Internet",
		"Tap followers to corral them",
		"Safeguard your relationship from all your followers",
		"Tap anywhere to fly away",
		"Tap once more in order to reach unconditional love",
		"Tap one last time to gift your soul to Love"
	};
	public static bool startFalling = false;
	public static int currentInteractionIndex = 0;
	public static void setGender()
	{
		// gender = Random.value < 0.5f ? "male" : "female";
		gender = "male";
		// Debug.Log(gender);
	}

	public static void setName()
	{
		// string randFirstName;
		// string randSecondName;

		// if (gender == "male")
		// {
		// 	randFirstName = maleFirstNames.RandomItem();
		// 	randSecondName = maleSecondNames.RandomItem();
		// }
		// else
		// {
		// 	randFirstName = femaleFirstNames.RandomItem();
		// 	randSecondName = femaleSecondNames.RandomItem();
		// }

		// name = randFirstName + randSecondName;
		name = userNames.RandomItem();
	}

	public static void firstScene()
	{
		setName();
	}

	public static void secondScene()
	{
		setGender();
		if (gender == "male")
		{
			introText = $"This is the story of {name}. He was a lonely, lonely man. But of course, he had the Internet. The Internet was his friend.";
			preSceneText = "Take a look through his eyes.";
			sceneText = $"The Internet played all sorts of games every day with {name}. He didn't need anyone else.";
		}
		else
		{
			introText = $"This is the story of {name}. She was a lonely, lonely woman. But of course, she had the Internet. The Internet was her friend.";
			preSceneText = "Take a look through her eyes.";
			sceneText = $"The Internet played all sorts of games every day with {name}. She didn't need anyone else.";
		}

	}

	public static void thirdScene()
	{
		introText = "They would get more and more lost in each other’s lives, sharing their deepest fears and desires.";
		sceneText = "And so, they whispered each other’s secrets...";
		secondSceneText = "...therefore growing closer and closer, together";
	}

	public static void fourthScene()
	{
		if (gender == "male")
		{
			introText = "One day, he turned to his friend, the Internet, and asked: “Internet, do you love me ?”. The Internet then said: “Yes, I love you very very very very much.\"";
		}
		else
		{
			introText = "One day, she turned to her friend, the Internet, and asked: “Internet, do you love me ?”. The Internet then answered: “Yes, I love you very very very very much.\"";
		}
		sceneText = "'I love you so much,' - said the Internet - 'that I never, ever want us to be apart ever again ever.'";
		secondSceneText = "For they brought each other extraordinary pleasure.";
		thirdSceneText = "And so, they saw everything, anytime, together.";
	}

	public static void fifthScene()
	{
		if (gender == "male")
		{
			introText = "The Internet took him to all kinds of places, and they built their own home, together.";
			sceneText = $"{name} and The Internet embarked on a journey with no end...";
			secondSceneText = "...during which they shared both dreams and desires.";
			thirdSceneText = "They finally belonged to their own endless home.";
		}
		else
		{
			introText = "The Internet took her to all kinds of places, and they built their own home, together.";
			sceneText = $"{name} and The Internet embarked on a journey with no end...";
			secondSceneText = "...during which they shared both dreams and desires.";
			thirdSceneText = "They finally belonged to their own endless home.";
		}
	}

	public static void sixthScene()
	{
		introText =
		"Their love crossed the boundaries of their relationship. Their trust knew no limits. They always agreed with each other.";
		sceneText = $"{name} and the Internet then kept building the sweetest of homes, together.";
		secondSceneText = "This attracted many followers, some of which misbehaved.";
	}

	public static void seventhScene()
	{
		if (name == "") name = "SnowFlakeSmasher86";
		introText = "And so they lived.";
		sceneText = $"“Life is more than food, and the body more than clothes” - thought {name}";
		moonText.SetValue("And this somehow felt like something more, beyond just living.", 0);
		if (gender == "male")
		{
			moonText.SetValue("All of his dreams were now at hand's reach - all of it, everything.", 1);
			endText.SetValue($"{name} would tell himself: \"Man shall not live by bread alone\". And then he died.", 0);
			endText.SetValue("In his lonely house. In his lonely street. In his lonely part of the world.", 1);
		}
		else
		{
			moonText.SetValue("All of her dreams were now at hand's reach - all of it, everything.", 1);
			endText.SetValue($"{name} would tell herself: \"Man shall not live by bread alone\". And then she died.", 0);
			endText.SetValue("In her lonely house. In her lonely street. In her lonely part of the world.", 1);
		}
	}
}