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
	public static string websocketID = "";
	public static bool isPhoneConnected = false;
	public static bool isVoiceLoaded = false;
	public static int phoneCurrentScene = 0;
	public static float gyroAngleX = 0.0f;
	public static float gyroAngleY = 0.0f;
	public static float gyroAngleZ = 0.0f;
	public static string name = "";
	public static string gender;
	public static string introText;
	public static string preSceneText;
	public static string sceneText;
	// public static string[] maleFirstNames = { "gonc", "henry", "valentin", "artey", "ave", "ouri", "marius", "michael" };
	// public static string[] maleSecondNames = { "inity", "_bzrd", ".dll", "miramont10", "_kevin", ".levin", "ballot" };
	// public static string[] femaleFirstNames = { "mogo", "sol", "mog", "set", "cha", "saana", "marie", "chloro", "interieur", "julie", "m" };
	// public static string[] femaleSecondNames = { "so9", "_bzrd", ".zilla", "ballot", ".levin", "sans", "less", "ouilla_", "jh", };
	public static string[] userNames = {
		"SnowflakeSmasher86",
		"boggybrendan69",
		"larryisgood207",
		"boogie2988",
		"lobbybobby88",
		"TheyCallMeDSP",
		"ChrisChanSonichu",
		"funkybandit",
		"lincostinko",
		"SirCaesar29",
		"tamwebb90",
		"canineduchess3848",
		"SuperficialParrot",
		"oceanicheader77",
		"FuzzyCurator",
		"throwaway3225",
		"greytossing",
		"awakehacker362",
		"CrispVocalist",
		"unknowncyclist82",
		"dustydomination",
		"BlueHedgehog",
		"optimaltrauma8",
		"tangibleabbey890",
		"LoneMouse",
		"acclaimedfetish",
		"toothsomeduchess31",
		"hothardship5",
		"Frenchcorpus2",
		"uprightrashfluke",
		"thegrievingduckling",
		"unfortunateflora518",
		"demonichockey",
		"iamthedevil666",
		"handsomebodyguard33",
		"TheEnragedBrunt",
		"sackoftongues",
		"littlefish97",
		"LividKangaroo",
		"InsistentDiver",
		"fertilecoomer",
		"throwaway38333",
		"2honored4u",
		"zonkedbonus558",
		"moralplurality0645",
		"softprecedence74",
		"AveragePanther",
		"growingopposition67",
		"disloyalnovella",
		"baggyslugger58",
		"TautBoldness",
		"virtualaviation57",
		"smarttycoon49",
		"CoolMole",
		"scornfuldialect86",
		"cravensubsidy888",
		"mercifulcombustion11",
		"PepperySpectre",
		"distressedseizure472",
		"QuietCheerleader",
		"HappyFetish",
		"insecurefoothold6235",
		"thirstysaucer",
		"pungentgrappling",
		"dustymeantime86",
		"WhollyTrained",
		"GratefulDemon6",
		"generoustendon7",
		"AcclaimedDentist",
		"raspyperusal176",
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
		"wavyfunction1101",
		"dustyman",
		"CraftyNoodle",
		"saladass555",
		"tomamoto",
		"uglymeanman",
		"bouncytphone187",
		"CoollySteel",
		"darkrecreation386",
		"snottybullion15",
		"DopeyLlama",
		"bothmatrix",
		"lavishsnark",
		"LoudlyHallowed",
		"moistdriver281",
		"silkyavarice2968",
		"sugardaddy69",
		"SymptomaticCapybara",
		"tastysushi",
		"OmegaMind04",
		"throwaway2104",
		"TinyAnt",
		"TheAmazingAtheist",
		"giddyfastball70",
		"noiselessoverseer",
		"nutritiousjuror5682",
		"cavernousphysique68",
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
		"thoughtfulmaintenance86",
		"robloxfantasy",
		"wingedconstable6",
		"perkyactress615",
		"FlimsyPneumonia",
		"DisgustingAlligator",
		"frantichands",
		"SadPhilosopher",
		"wholeheader3",
		"JubilantRalph",
		"totalunderdog74",
		"loungelizard",
		"pointlessliberation",
		"BasicDonkey",
		"quaintposter75",
		"mightyliner83",
		"deadlypottery8",
		"FabulousListener",
		"honoredtheology72",
		"PastJuncture",
		"GutturalMartin",
		"chiefdumps733",
		"unknownrefrigerator168",
		"alarmingdepression8",
		"thefullmayhem",
		"TheUsedBasis",
		"ObliviousDriver",
		"wobblylikeness81",
		"vengefuleater",
		"GapingHole",
		"throwaway9529",
		"properwords642",
		"CraftyNuisance",
		"radiantartisan71",
		"TheLameRecipe",
		"glumcharade32",
		"vacantmaintenance827",
		"LonelyScholarship",
		"GentleGiant",
		"FaithfulHamburger",
		"sociablememory",
		"CalmCamel",
		"gaudycivility28",
		"starchywidth72",
		"RoomyBroccoli",
		"MetallicBooty",
		"flickeringfluke",
		"CommentEtiquette",
		"stickybasin",
		"Cr1msonPr1nce",
		"snobbishhands2",
		"LazyRock",
		"muscularcheerleader023",
		"zestylookout5642",
		"superbjoseph",
	};

	public static void setGender()
	{
		gender = Random.value < 0.5f ? "male" : "female";
		Debug.Log(gender);
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

	public static void secondScene()
	{
		setGender();
		setName();
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