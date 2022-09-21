using System;

public class DialogueContainer {

	private static readonly string _next   = "continue";
	private static readonly int    @false  = 0;
	private static readonly int    @true   = 1;
	private static readonly int    neutralInt = 0;
	private static readonly int    flirtyInt = 0;
	private static readonly int    angryInt = 0;
	private static readonly int    boredInt = 0;

	public static           string _commonInterest = "fish";
	private static          string _randomInterest = "random interest";
	private static          string _datesName      = "Amy";
	

	public const   string neutral      = "neutral";
	public const   string flirty       = "flirty";
	public const   string angry        = "angry";
	public const   string bored        = "bored";
	public const   string cost         = "cost";
	private static string happy        = "happy";
	private static string embarrassed  = "embarrassed";
	private static string interest     = "interest";
	private static string compliment   = "compliment";
	private static string joke         = "joke";
	private static string likesJokes   = "likesJokes";
	private static string selfInterest = "selfInterest";
	public static string drinkWine    = "drinkWine";
	private static string highestMood  = "highestMood";
	
	public static int    totalCost;
	
	public static Dia[] dia = new Dia[] {
		/* QUESTIONS */
		// *	interests = 0, 
		new Dia(rule: new Rule(new[] {
					(interest, new Criterion(0)),
				}),
				speaker: "You",
				text: $"What should I say?",
				choiceText: new string[] {
					"Talk about your date's interests",
					"Compliment your date",
					"Make a joke",
					"Start talking about yourself"
				},
				writeBacks: new[] {
					(interest, 1, Query.CompType.Increment),
					(compliment, 1, Query.CompType.Increment),
					(joke, 1, Query.CompType.Increment),
					(selfInterest, 1, Query.CompType.Increment),
				}
				),

		/* QUESTIONS TOLD JOKE */
		new Dia(rule: new Rule(new[] {
					(interest, new Criterion(0)),
					("toldJoke", new Criterion(1)),
				}),
				speaker: "You",
				text: $"What should I say?",
				choiceText: new string[] {
					"Talk about your date's interests",
					"Compliment your date",
					"Make a joke",
					"Start talking about yourself"
				},
				writeBacks: new[] {
					(interest, 1, Query.CompType.Increment),
					(compliment, 1, Query.CompType.Increment),
					("toldJoke", 1, Query.CompType.Increment),
					(selfInterest, 1, Query.CompType.Increment),
				}
				),

		/* JOKE */
		// *	interests = 0, joke = 1, 
		new Dia(rule: new Rule(new[] {
					(interest, new Criterion(0)),
					(joke, new Criterion(1)),
				}),
				speaker: "You",
				text: $"What did the fish say when he swam into a wall?",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(joke, 1, Query.CompType.Increment),
				}
				),

		/* PUNCHLINE */
		// *	interests = 0, joke = 2, 
		new Dia(rule: new Rule(new[] {
					(interest, new Criterion(0)),
					(joke, new Criterion(2)),
				}),
				speaker: "You",
				text: $"Dam.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(joke, 1, Query.CompType.Increment),
				}
				),

		/* LIKES JOKE */
		// *	interests = 0, joke = 3, likesJokes, 
		new Dia(rule: new Rule(new[] {
					(interest, new Criterion(0)),
					(joke, new Criterion(3)),
					(likesJokes, new Criterion(1)),
				}),
				speaker: $"{_datesName}",
				text: $"Hahaha! That was a good one.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(happy, 1, Query.CompType.Increment),
					(bored, -1, Query.CompType.Increment),
					(joke, 1, Query.CompType.Increment),
					("toldJoke", 1, Query.CompType.Increment),
				}
				),

		/* DISLIKES JOKE */
		// *	interests = 0, joke = 3, !likesJokes
		new Dia(rule: new Rule(new[] {
					(interest, new Criterion(0)),
					(joke, new Criterion(3)),
					(likesJokes, new Criterion(0)),
				}),
				speaker: $"{_datesName}",
				text: $"Ha... funny...",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(bored, 1, Query.CompType.Increment),
					(joke, 1, Query.CompType.Increment),
					("toldJoke", 1, Query.CompType.Increment),
				}
				),

		/* JOKE EXPLANATION */
		// *	interests = 0, joke = 4, likesJokes
		new Dia(rule: new Rule(new[] {
					(interest, new Criterion(0)),
					(joke, new Criterion(4)),
					(likesJokes, new Criterion(1)),
					("toldJoke", new Criterion(2)),
				}),
				speaker: "You",
				text: $"You see, it's funny because the word dam and the exclamation \"Damn!\" sound quite similar...",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(embarrassed, 1, Query.CompType.Increment),
					(joke, 1, Query.CompType.Increment),
					("toldJoke", 1, Query.CompType.Increment),
				}
				),

		/* JOKE EXPLANATION REACTION */
		// *	interests = 0, likesJokes, joke = 5,
		new Dia(rule: new Rule(new[] {
					(interest, new Criterion(0)),
					(joke, new Criterion(5)),
					(likesJokes, new Criterion(1)),
					("toldJoke", new Criterion(3)),
				}),
				speaker: $"{_datesName}",
				text: $"Yeah... Thanks.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(bored, 1, Query.CompType.Increment),
					(joke, 1, Query.CompType.Increment),
					("toldJoke", 1, Query.CompType.Increment),
				}
				),

		/* MAYBE THEY'RE DEAF? */
		// *	interests = 0, !likesJokes, joke = 4, 
		new Dia(rule: new Rule(new[] {
					(interest, new Criterion(0)),
					(joke, new Criterion(4)),
					(likesJokes, new Criterion(0)),
					("toldJoke", new Criterion(2)),
				}),
				speaker: "You",
				text: $"WHAT DID THE FISH SAY WHEN HE SWAM INTO A WALL?!",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(joke, 1, Query.CompType.Increment),
					(embarrassed, 1, Query.CompType.Increment),
					(bored, -1, Query.CompType.Increment),
					("toldJoke", 1, Query.CompType.Increment),
				}
				),


		/* YELLED JOKE REACTION */
		new Dia(rule: new Rule(new[] {
					(interest, new Criterion(0)),
					(joke, new Criterion(5)),
					(likesJokes, new Criterion(0)),
					("toldJoke", new Criterion(2)),
				}),
				speaker: $"{_datesName}",
				text: $"Keep it down please, you're embarrassing me!",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(embarrassed, 1, Query.CompType.Increment),
					(angry, 1, Query.CompType.Increment),
					(joke, 1, Query.CompType.Increment),
					("toldJoke", 1, Query.CompType.Increment),
				}
				),

		/* GIVE COMPLIMENT */
		new Dia(rule: new Rule(new[] {
					(interest, new Criterion(0)),
					(compliment, new Criterion(1)),
				}),
				speaker: $"You",
				text: $"Wow! You look absolutely stunning!",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(compliment, 1, Query.CompType.Increment),
				}
				),

		/* RECEIVE COMPLIMENT */
		new Dia(rule: new Rule(new[] {
					(interest, new Criterion(0)),
					(compliment, new Criterion(2)),
				}),
				speaker: $"{_datesName}",
				text: $"Oh really... that's so sweet of you!",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(compliment, 1, Query.CompType.Increment),
					(flirty, 1, Query.CompType.Increment),
				}
				),


		/* SELF CENTERED */
		new Dia(rule: new Rule(new[] {
					(interest, new Criterion(0)),
					(selfInterest, new Criterion(1)),
				}),
				speaker: $"You",
				text:
				$"I'm really not surprised you wanted to go on a date with such an amazing person as myself. I have so many good qualities.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(selfInterest, 1, Query.CompType.Increment),
				}
				),

		/* SELF CENTERED RESPONSE */
		new Dia(rule: new Rule(new[] {
					(interest, new Criterion(0)),
					(selfInterest, new Criterion(2)),
				}),
				speaker: $"{_datesName}",
				text: $"Good for you I suppose.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(selfInterest, 1, Query.CompType.Increment),
					(angry, 1, Query.CompType.Increment),
					(bored, 1, Query.CompType.Increment),
				}
				),

		/* INTERESTS */
		new Dia(rule: new Rule(new[] {
					(interest, new Criterion(1)),
				}),
				speaker: $"You",
				text: $"What do I ask about?",
				choiceText: new string[] {
					"Talk about common interests",
					"Talk about your dates interests",
					"Anyways... Let's talk more about me.",
				},
				writeBacks: new[] {
					(interest, 2, Query.CompType.Set),
					(interest, 3, Query.CompType.Set),
					(interest, 4, Query.CompType.Set),
				}
				),

		/* COMMON INTERESTS */
		new Dia(rule: new Rule(new[] {
					(interest, new Criterion(2)),
				}),
				speaker: $"You",
				text: $"So you like {_commonInterest}, that's one of my biggest interests too!",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(interest, -1, Query.CompType.Set),
					(happy, 1, Query.CompType.Increment),
					("waiter", 1, Query.CompType.Set),
					// initializing keys here for later
					("timeTravel", 0, Query.CompType.Set),
					("flying", 0, Query.CompType.Set),
					("xRay", 0, Query.CompType.Set),
					("noPowers", 0, Query.CompType.Set),
					("answeredPhone", -2, Query.CompType.Set),
				}
				),

		/* DATES INTERESTS */
		new Dia(rule: new Rule(new[] {
					(interest, new Criterion(3)),
				}),
				speaker: $"You",
				text: $"So you like {_randomInterest}, I don't know much about it, maybe you could teach me?",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(interest, 5, Query.CompType.Set), // TODO Debug this! Should value be other than 5 and CompType.Set
					(flirty, 1, Query.CompType.Increment),
					(happy, 1, Query.CompType.Increment),
					("waiter", 1, Query.CompType.Set),
					// initializing keys here for later
					("timeTravel", 0, Query.CompType.Set),
					("flying", 0, Query.CompType.Set),
					("xRay", 0, Query.CompType.Set),
					("noPowers", 0, Query.CompType.Set),
					("answeredPhone", -2, Query.CompType.Set),
				}
				),

		/* YOUR INTERESTS */
		new Dia(rule: new Rule(new[] {
					(interest, new Criterion(4)),
				}),
				speaker: $"You",
				text: $"Okay... Well my interests are far more complex, you wouldn't understand.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(angry, 1, Query.CompType.Increment),
					(interest, -1, Query.CompType.Set), // TODO Debug this! Should value be other than -1 and CompType.Set
					("waiter", 1, Query.CompType.Set),
					// initializing keys here for later
					("timeTravel", 0, Query.CompType.Set),
					("flying", 0, Query.CompType.Set),
					("xRay", 0, Query.CompType.Set),
					("noPowers", 0, Query.CompType.Set),
					("answeredPhone", -2, Query.CompType.Set),
				}
				),

		/* ORDER FOOD PLAYER */
		new Dia(rule: new Rule(new[] {
					("waiter", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"The waiter's here. What do I want to order?",
				choiceText: new string[] {
					"Salad 5$",
					"Burger 10$",
					"Steak 30$",
					"Lobster 100$",
				},
				writeBacks: new[] {
					("playerSalad", 1, Query.CompType.Set),
					("playerBurger", 1, Query.CompType.Set),
					("playerSteak", 1, Query.CompType.Set),
					("playerLobster", 1, Query.CompType.Set),
				}
				),

		/* WAITER ORDER SALAD */
		new Dia(rule: new Rule(new[] {
					("waiter", new Criterion(1)),
					("playerSalad", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"I'll have the salad please.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("waiter", 1, Query.CompType.Increment),
					(cost, 5, Query.CompType.Increment),
				}
				),

		/* WAITER ORDER BURGER */
		new Dia(rule: new Rule(new[] {
					("waiter", new Criterion(1)),
					("playerBurger", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"I'll have the burger please.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("waiter", 1, Query.CompType.Increment),
					(cost, 10, Query.CompType.Increment),
				}
				),

		/* WAITER ORDER STEAK */
		new Dia(rule: new Rule(new[] {
					("waiter", new Criterion(1)),
					("playerSteak", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"I'll have the steak please.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("waiter", 1, Query.CompType.Increment),
					(cost, 30, Query.CompType.Increment),
				}
				),

		/* WAITER ORDER LOBSTER */
		new Dia(rule: new Rule(new[] {
					("waiter", new Criterion(1)),
					("playerLobster", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"I'll have the lobster please.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("waiter", 1, Query.CompType.Increment),
					(cost, 100, Query.CompType.Increment),
				}
				),

		/* ORDER FOOD DATE */
		new Dia(rule: new Rule(new[] {
					("waiter", new Criterion(2)),
				}),
				speaker: $"The Waiter",
				text: $"And what would your lovely date have?",
				choiceText: new string[] {
					"Salad 5$",
					"Burger 10$",
					"Steak 30$",
					"Lobster 100$",
				},
				writeBacks: new[] {
					("dateSalad", 1, Query.CompType.Set),
					("dateBurger", 1, Query.CompType.Set),
					("dateSteak", 1, Query.CompType.Set),
					("dateLobster", 1, Query.CompType.Set),
				}
				),

		/* WAITER DATE SALAD */
		// *	interest = 5, waiter = 2
		new Dia(rule: new Rule(new[] {
					("waiter", new Criterion(2)),
					("dateSalad", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"My date will have the salad.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("waiter", 1, Query.CompType.Increment),
					(cost, 5, Query.CompType.Increment),
				}
				),

		/* WAITER DATE BURGER */
		// *	interest = 5, 
		new Dia(rule: new Rule(new[] {
					("waiter", new Criterion(2)),
					("dateBurger", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"The burger please.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("waiter", 1, Query.CompType.Increment),
					(cost, 10, Query.CompType.Increment),
				}
				),

		/* WAITER DATE STEAK */
		// *	interest = 5, 
		new Dia(rule: new Rule(new[] {
					("waiter", new Criterion(2)),
					("dateSteak", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"A steak for the lovely one.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("waiter", 1, Query.CompType.Increment),
					(cost, 30, Query.CompType.Increment),
				}
				),

		/* WAITER DATE LOBSTER */
		// *	interest = 5, 
		new Dia(rule: new Rule(new[] {
					("waiter", new Criterion(2)),
					("dateLobster", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"I think lobster is in season right? One lobster please.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("waiter", 1, Query.CompType.Increment),
					(cost, 100, Query.CompType.Increment),
				}
				),

		/* EATING ACTIVITY DRUNK */
		// *	interest = 5, waiter = 4
		new Dia(rule: new Rule(new[] {
					("waiter", new Criterion(3)),
					(drinkWine, new Criterion(0, int.MaxValue)),
				}),
				speaker: $"You",
				text: $"What do I do while we're eating?",
				choiceText: new string[] {
					"Eat in silence",
					"Ask about the food",
					"Drink wine",
					"Serenade my date",
				},
				writeBacks: new[] {
					("waiter", 4, Query.CompType.Set),
					("waiter", 5, Query.CompType.Set),
					("waiter", 6, Query.CompType.Set),
					("waiter", 7, Query.CompType.Set),
				}
				),
		
		/* EATING ACTIVITY */
		// *	interest = 5, waiter = 4
		new Dia(rule: new Rule(new[] {
					("waiter", new Criterion(3)),
				}),
				speaker: $"You",
				text: $"What do I do while we're eating?",
				choiceText: new string[] {
					"Eat in silence",
					"Ask about the food",
					"Drink wine",
				},
				writeBacks: new[] {
					("waiter", 4, Query.CompType.Set),
					("waiter", 5, Query.CompType.Set),
					("waiter", 6, Query.CompType.Set),
				}
				),
		
		/* SERENADE */
		// *	interest = 5, waiter = 4
		new Dia(rule: new Rule(new[] {
					("waiter", new Criterion(7)),
				}),
				speaker: $"You",
				text: $"You sing, with great confidence, very badly.",
				choiceText: new string[] {
					$"Onlyyy yooooooou, can maaaakeee, my eyyyyyyyyyeeeesss....",
				},
				writeBacks: new[] {
					("waiter", -1, Query.CompType.Set),
					(embarrassed, 1, Query.CompType.Increment),
					("toneDeaf", 1, Query.CompType.Set),
					("dateOver", 1, Query.CompType.Set),
				}
				),

		/* EATING SILENCE */
		new Dia(rule: new Rule(new[] {
					("waiter", new Criterion(4)),	// eatSilence
				}),
				speaker: $"",
				text: $"...",
				choiceText: new string[] {
					$"...",
				},
				writeBacks: new[] {
					(bored, 1, Query.CompType.Increment),
					(embarrassed, 1, Query.CompType.Increment),
					("eatSilence", 1, Query.CompType.Set),
					("waiter", 3, Query.CompType.Set),
				}
				),

		/* FOOD DELICIOUS PLAYER_SALAD */
		new Dia(rule: new Rule(new[] {
					("waiter", new Criterion(5)),	// askFood
					("playerSalad", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"This salad was really delicious!",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("neutral", 1, Query.CompType.Increment),
					("askFood", 1, Query.CompType.Set),
					("waiter", -1, Query.CompType.Set),
				}
				),
		
		/* FOOD DELICIOUS PLAYER_BURGER */
		// *	interest = 5, waiter = 1, askFood = 1, salad = 1
		new Dia(rule: new Rule(new[] {
					("waiter", new Criterion(5)),	// askFood
					("playerBurger", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"This burger was delicious!",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("neutral", 1, Query.CompType.Increment),
					("askFood", 1, Query.CompType.Set),
					("waiter", -1, Query.CompType.Set),
				}
				),
		
		/* FOOD DELICIOUS PLAYER_STEAK */
		// *	interest = 5, waiter = 1, askFood = 1, salad = 1
		new Dia(rule: new Rule(new[] {
					("waiter", new Criterion(5)),	// askFood
					("playerSteak", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"This salad was really delicious!",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("neutral", 1, Query.CompType.Increment),
					("askFood", 1, Query.CompType.Set),
					("waiter", -1, Query.CompType.Set),
				}
				),
		
		/* FOOD DELICIOUS PLAYER_LOBSTER */
		// *	interest = 5, waiter = 1, askFood = 1, salad = 1
		new Dia(rule: new Rule(new[] {
					("waiter", new Criterion(5)),	// askFood
					("playerLobster", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"This lobster was so delicious!",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("neutral", 1, Query.CompType.Increment),
					("askFood", 1, Query.CompType.Set),
					("waiter", -1, Query.CompType.Set),
				}
				),

		/* ASK FOOD FOLLOWUP DATE_SALAD */
		// *	interest = 5, waiter = 1, askFood = 1, salad = 1
		new Dia(rule: new Rule(new[] {
					("askFood", new Criterion(1)),
					("dateSalad", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"How was your salad?",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(angry, 1, Query.CompType.Increment),
					("askFood", 1, Query.CompType.Increment),
				}
				),

		/* ASK FOOD FOLLOWUP DATE_BURGER */
		// *	interest = 5, waiter = 1, askFood = 1, salad = 1
		new Dia(rule: new Rule(new[] {
					("askFood", new Criterion(1)),
					("dateBurger", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"How was your burger?",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("neutral", 1, Query.CompType.Increment),
					("askFood", 1, Query.CompType.Increment),
				}
				),

		/* ASK FOOD FOLLOWUP DATE_STEAK */
		// *	interest = 5, waiter = 1, askFood = 1, salad = 1
		new Dia(rule: new Rule(new[] {
					("askFood", new Criterion(1)),
					("dateSteak", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"How was your steak?",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(happy, 1, Query.CompType.Increment),
					("askFood", 1, Query.CompType.Increment),
				}
				),

		/* ASK FOOD FOLLOWUP DATE_LOBSTER */
		// *	interest = 5, waiter = 1, askFood = 1, salad = 1
		new Dia(rule: new Rule(new[] {
					("askFood", new Criterion(1)),
					("dateLobster", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"How was your lobster?",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(happy, 1, Query.CompType.Increment),
					("askFood", 1, Query.CompType.Increment),
				}
				),


		/* ANSWER LOBSTER */
		// *	interest = 5, waiter = 1, askFood = 2, lobster = 1
		new Dia(rule: new Rule(new[] {
					// (interest, new Criterion(1)),
					// ("waiter", new Criterion(1)),
					("askFood", new Criterion(2)),
					("dateLobster", new Criterion(1)),
				}),
				speaker: $"{_datesName}",
				text: $"The lobster tastes very good",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(happy, 1, Query.CompType.Increment),
					("askFood", 1, Query.CompType.Increment),
					("dateQuestion", 1, Query.CompType.Set),
				}
				),
		
		/*  ANSWER SALAD */
		new Dia(rule: new Rule(new[] {
					("askFood", new Criterion(2)),
					("dateSalad", new Criterion(1)),
				}),
				speaker: $"{_datesName}",
				text: $"I did not want a salad! Do I look like a rabbit to you?",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(angry, 1, Query.CompType.Increment),
					("askFood", 1, Query.CompType.Increment),
					("dateQuestion", 1, Query.CompType.Set),
				}
				),

		/* ANSWER STEAK */
		new Dia(rule: new Rule(new[] {
					("askFood", new Criterion(2)),
					("dateSteak", new Criterion(1)),
				}),
				speaker: $"{_datesName}",
				text: $"The steak tastes very good",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(happy, 1, Query.CompType.Increment),
					("askFood", 1, Query.CompType.Increment),
					("dateQuestion", 1, Query.CompType.Set),
				}
				),

		/* ANSWER BURGER */
		new Dia(rule: new Rule(new[] {
					("askFood", new Criterion(2)),
					("dateBurger", new Criterion(1)),
				}),
				speaker: $"{_datesName}",
				text: $"It was alright. Nothing special.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(happy, 1, Query.CompType.Increment),
					("askFood", 1, Query.CompType.Increment),
					("dateQuestion", 1, Query.CompType.Set),
				}
				),
		
		/* ANSWER BURGER PLAYER_LOBSTER */
		new Dia(rule: new Rule(new[] {
					("askFood", new Criterion(2)),
					("dateBurger", new Criterion(1)),
					("playerLobster", new Criterion(1)),
				}),
				speaker: $"{_datesName}",
				text: $"It was fine...",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(angry, 1, Query.CompType.Increment),
					("askFood", 1, Query.CompType.Increment),
					("dateQuestion", 1, Query.CompType.Set),
				}
				),
		
		
		/* DATE_QUESTION */
		new Dia(rule: new Rule(new[] {
					("dateQuestion", new Criterion(1)),
					("timeTravel", new Criterion(0)),
					("flying", new Criterion(0)),
					("xRay", new Criterion(0)),
					("noPowers", new Criterion(0)),
				}),
				speaker: $"{_datesName}",
				text: $"If you could have any superpower, what would it be?",
				choiceText: new string[] {
					"Time travel",
					"Flying",
					"X-Ray vision",
					"I don't want any superpowers",
				},
				writeBacks: new[] {
					("timeTravel", 1, Query.CompType.Set),
					("flying", 1, Query.CompType.Set),
					("xRay", 1, Query.CompType.Set),
					("noPowers", 1, Query.CompType.Set),
				}
				),
		
		/* POWER_ANSWER_TIME_TRAVEL */
		new Dia(rule: new Rule(new[] {
					("timeTravel", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"I think I would choose time traveling so I could have met you earlier!",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("timeTravel", 1, Query.CompType.Increment),
					(flirty, 1, Query.CompType.Increment),
					("phone", 1, Query.CompType.Set),
				}
				),

		/* POWER_ANSWER_FLYING */
		new Dia(rule: new Rule(new[] {
					("flying", new Criterion(1)),
				}),
				speaker: $"You",
				text: $" I think flying would be very fun and useful.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("flying", 1, Query.CompType.Increment),
					(happy, 1, Query.CompType.Increment),
					("phone", 1, Query.CompType.Set),
				}
				),
		
		/* POWER_ANSWER_XRAY */
		new Dia(rule: new Rule(new[] {
					("xRay", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"X-ray vision of course so I could see through your clothes.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("xRay", 1, Query.CompType.Increment),
					(angry, 1, Query.CompType.Increment),
					("phone", 1, Query.CompType.Set),
				}
				),
		
		/* POWER_ANSWER_NONE */
		new Dia(rule: new Rule(new[] {
					("noPowers", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"Superpowers are for children, I don’t want any.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("noPowers", 1, Query.CompType.Increment),
					(bored, 1, Query.CompType.Increment),
					("phone", 1, Query.CompType.Set),
				}
				),
		
		/* PHONE_CALL */
		new Dia(rule: new Rule(new[] {
					("phone", new Criterion(1)),
					("answeredPhone", new Criterion(-2)),
				}),
				speaker: $"Narrator",
				text: $"Suddenly your phone starts ringing. \n\"Incoming call from BOSS\"\nWhat should I do?",
				choiceText: new string[] {
					"This is important",
					"It can wait",
				},
				writeBacks: new[] {
					("answeredPhone", 1, Query.CompType.Set),
					("answeredPhone", 0, Query.CompType.Set),
				}
				),
		
		/* ANSWER_PHONE_CALL */
		new Dia(rule: new Rule(new[] {
					("answeredPhone", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"It's my boss, sorry but I have to take this call.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("answeredPhone", -1, Query.CompType.Set),
					(neutral, 1, Query.CompType.Increment),
					(bored, 1, Query.CompType.Increment),
					("bathroom", 1, Query.CompType.Set),
				}
				),
		
		/* DIDN'T_ANSWER_PHONE_CALL */
		new Dia(rule: new Rule(new[] {
					("answeredPhone", new Criterion(0)),
				}),
				speaker: $"You",
				text: $"Oh sorry about that, now where were we...",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("answeredPhone", -1, Query.CompType.Set),
					(flirty, 1, Query.CompType.Increment),
					(happy, 1, Query.CompType.Increment),
					(neutral, 1, Query.CompType.Increment),
					("bathroom", 1, Query.CompType.Set),
				}
				),
		
		/* BATHROOM */
		new Dia(rule: new Rule(new[] {
					("bathroom", new Criterion(1)),
				}),
				speaker: $"{_datesName}",
				text: $"I had to go to the bathroom anyway, be right back.",
				choiceText: new string[] {
					$"Okay",
				},
				writeBacks: new[] {
					("bathroom", 1, Query.CompType.Increment),
				}
				),
		
		/* BATHROOM */
		new Dia(rule: new Rule(new[] {
					("bathroom", new Criterion(2)),
				}),
				speaker: $"You",
				text: $"What should I do while I wait for my date?",
				choiceText: new string[] {
					$"Drink wine",
					$"Prepare a poem",
					$"Play games on phone",
					$"Try to lick your own elbow",
				},
				writeBacks: new[] {
					(drinkWine, 1, Query.CompType.Increment),
					("poem", 1, Query.CompType.Set),
					("games", 1, Query.CompType.Set),
					("lick", 1, Query.CompType.Set),
				}
				),
		
		/* POEM */
		new Dia(rule: new Rule(new[] {
					("poem", new Criterion(1)),
					("poem", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"You felt creative and decided to write a poem for your date to surprise them with when they come back.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("poem", 1, Query.CompType.Increment),
					("bathroom", -1, Query.CompType.Set),
					("return", 1, Query.CompType.Set),
				}
				),
		
		/* GAMES */
		new Dia(rule: new Rule(new[] {
					("games", new Criterion(1)),
					("games", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"You destroyed some noobs on your favorite game while you waited for your date to come back.",
				choiceText: new string[] {
					$"GG",
				},
				writeBacks: new[] {
					("games", 1, Query.CompType.Increment),
					("bathroom", -1, Query.CompType.Set),
					("return", 1, Query.CompType.Set),
				}
				),
		
		/* LICK */
		new Dia(rule: new Rule(new[] {
					("lick", new Criterion(1)),
					("lick", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"You tried to lick your own elbow... In a restaurant full of people. It failed.",
				choiceText: new string[] {
					$"It was a good effort though",
				},
				writeBacks: new[] {
					("lick", 1, Query.CompType.Increment),
					("bathroom", -1, Query.CompType.Set),
					("return", 1, Query.CompType.Set),
				}
				),
		
		/* DATE_RETURN */
		new Dia(rule: new Rule(new[] {
					("return", new Criterion(1)),
				}),
				speaker: $"Narrator",
				text: $"Date comes back",
				choiceText: new string[] {
					$"Ah, you're back.",
				},
				writeBacks: new[] {
					("return", 1, Query.CompType.Increment),
				}
				),
		
		/* DATE_RETURN POEM */
		new Dia(rule: new Rule(new[] {
					("return", new Criterion(2)),
					("poem", new Criterion(2)),
				}),
				speaker: $"You",
				text: $"While you were gone I wrote this poem for you...",
				choiceText: new string[] {
					$"ahem...",
				},
				writeBacks: new[] {
					("poem", 1, Query.CompType.Increment),
					("return", -1, Query.CompType.Set),
				}
				),
		
		/* DATE_RETURN_POEM_READING */
		new Dia(rule: new Rule(new[] {
					("poem", new Criterion(3)),
				}),
				speaker: $"You",
				text: $"Roses are red, violets are blue. There is no other star, as sparkling as you!",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("poem", 1, Query.CompType.Increment),
				}
				),
		
		/* DATE_RETURN_POEM_RESPONSE */
		new Dia(rule: new Rule(new[] {
					("poem", new Criterion(4)),
				}),
				speaker: $"{_datesName}",
				text: $"Awww! You're so sweet.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("poem", 1, Query.CompType.Increment),
					(flirty, 1, Query.CompType.Increment),
					("paying", 1, Query.CompType.Set),
				}
				),
		
		/* DATE_RETURN */
		new Dia(rule: new Rule(new[] {
					("return", new Criterion(2)),
				}),
				speaker: $"{_datesName}",
				text: $"I’m back!\nAnyways, I was wondering what you’re looking for in a relationship.",
				choiceText: new string[] {
					"Someone to grow old with",
					"Someone good looking",
					"A best friend ",
					"I don’t want a relationship",
				},
				writeBacks: new[] {
					("growOld", 1, Query.CompType.Set),
					("goodLooking", 1, Query.CompType.Set),
					("bestFriend", 1, Query.CompType.Set),
					("noRelationship", 1, Query.CompType.Set),
				}
				),
		
		/* GROW_OLD */
		new Dia(rule: new Rule(new[] {
					("return", new Criterion(2)),
					("growOld", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"I want to find someone to spend the rest of my life with.",
				choiceText: new string[] {
					$"{_next}"
				},
				writeBacks: new[] {
					("growOld", 1, Query.CompType.Increment),
					("return", -1, Query.CompType.Set),
				}
				),
		
		/* GROW_OLD_RESPONSE */
		new Dia(rule: new Rule(new[] {
					("growOld", new Criterion(2)),
				}),
				speaker: $"{_datesName}",
				text: $"That sounds lovely",
				choiceText: new string[] {
					$"{_next}"
				},
				writeBacks: new[] {
					("growOld", 1, Query.CompType.Increment),
					("return", -1, Query.CompType.Set),
					(happy, 1, Query.CompType.Increment),
					("paying", 1, Query.CompType.Set),
				}
				),

		/* GOOD_LOOKING */
		new Dia(rule: new Rule(new[] {
					("return", new Criterion(2)),
					("goodLooking", new Criterion(1)),
				}),
				speaker: $"You",
				text: $" I’m looking for someone as beautiful as you!",
				choiceText: new string[] {
					$"{_next}"
				},
				writeBacks: new[] {
					("goodLooking", 1, Query.CompType.Increment),
					("return", -1, Query.CompType.Set),
				}
				),
		
		/* GOOD_LOOKING_RESPONSE_ANGRY */
		new Dia(rule: new Rule(new[] {
					("goodLooking", new Criterion(2)),
					(angry, new Criterion(2, int.MaxValue)),
					(angry, new Criterion(2, int.MaxValue)),
					(angry, new Criterion(2, int.MaxValue)),
				}),
				speaker: $"{_datesName}",
				text: $"Is appearance really all you care about?",
				choiceText: new string[] {
					$"{_next}"
				},
				writeBacks: new[] {
					("goodLooking", 1, Query.CompType.Increment),
					("return", -1, Query.CompType.Set),
					(angry, 1, Query.CompType.Increment),
					("paying", 1, Query.CompType.Set),
				}
				),

		/* GOOD_LOOKING_RESPONSE_FLIRTY */
		new Dia(rule: new Rule(new[] {
					("goodLooking", new Criterion(2)),
					(flirty, new Criterion(0, 100)),
					(flirty, new Criterion(0, 100)),
				}),
				speaker: $"{_datesName}",
				text: $"Well, here I am.",
				choiceText: new string[] {
					$"{_next}"
				},
				writeBacks: new[] {
					("goodLooking", 1, Query.CompType.Increment),
					("return", -1, Query.CompType.Set),
					(flirty, 1, Query.CompType.Increment),
					("paying", 1, Query.CompType.Set),
				}
				),

		/* GOOD_LOOKING_RESPONSE_FLIRTY */
		new Dia(rule: new Rule(new[] {
					("goodLooking", new Criterion(2)),
				}),
				speaker: $"{_datesName}",
				text: $"Gotcha.",
				choiceText: new string[] {
					$"{_next}"
				},
				writeBacks: new[] {
					("goodLooking", 1, Query.CompType.Increment),
					("return", -1, Query.CompType.Set),
					(embarrassed, 1, Query.CompType.Increment),
					("paying", 1, Query.CompType.Set),
				}
				),
		
		/* BEST_FRIEND */
		new Dia(rule: new Rule(new[] {
					("return", new Criterion(2)),
					("bestFriend", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"Someone that I can have fun with.",
				choiceText: new string[] {
					$"{_next}"
				},
				writeBacks: new[] {
					("bestFriend", 1, Query.CompType.Increment),
					("return", -1, Query.CompType.Set),
					(neutral, 1, Query.CompType.Increment),
					(happy, 1, Query.CompType.Increment),
				}
				),
		
		/* BEST_FRIEND_RESPONSE */
		new Dia(rule: new Rule(new[] {
					("bestFriend", new Criterion(2)),
				}),
				speaker: $"{_datesName}",
				text: $"Having fun is important",
				choiceText: new string[] {
					$"{_next}"
				},
				writeBacks: new[] {
					("bestFriend", -1, Query.CompType.Set),
					(neutral, 1, Query.CompType.Increment),
					(happy, 1, Query.CompType.Increment),
					("paying", 1, Query.CompType.Set),
				}
				),
		
		/* NO_RELATIONSHIP */
		new Dia(rule: new Rule(new[] {
					("return", new Criterion(2)),
					("noRelationship", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"Who said I was looking for a relationship?",
				choiceText: new string[] {
					$"{_next}"
				},
				writeBacks: new[] {
					("noRelationship", 1, Query.CompType.Increment),
					("return", -1, Query.CompType.Set),
					(neutral, 1, Query.CompType.Increment),
					(happy, 1, Query.CompType.Increment),
					("dontCallMe", 1, Query.CompType.Set),
					("dateOver", 1, Query.CompType.Set),
				}
				),
		
		/* NO_RELATIONSHIP_RESPONSE */
		new Dia(rule: new Rule(new[] {
					("noRelationship", new Criterion(2)),
				}),
				speaker: $"{_datesName}",
				text: $"You’re just wasting my time.",
				choiceText: new string[] {
					$"{_next}"
				},
				writeBacks: new[] {
					("noRelationship", -1, Query.CompType.Set),
					("dontCallMe", 1, Query.CompType.Set),
					("dateOver", 1, Query.CompType.Set),
				}
				),

		/* PAY_DAY */
		new Dia(rule: new Rule(new[] {
					("paying", new Criterion(1)),
				}),
				speaker: $"Narrator",
				text: $"It’s gotten late and it’s time for you and your date to leave the restaurant.",
				choiceText: new string[] {
					$"{_next}"
				},
				writeBacks: new[] {
					("paying", 1, Query.CompType.Increment),
				}
				),
		
		/* PAY_DAY_2_SUB_105$ */
		new Dia(rule: new Rule(new[] {
					("paying", new Criterion(2)),
					(cost ,new Criterion(0, 105)),
				}),
				speaker: $"Waiter",
				text: $"The total bill is at {totalCost}",
				choiceText: new string[] {
					"Offer to pay for everything",
					"Ask date to split the bill"
				},
				writeBacks: new[] {
					("paying", 3, Query.CompType.Set),
					("paying", 4, Query.CompType.Set),
				}
				),
		
		/* <105_LET_ME_PAY */
		new Dia(rule: new Rule(new[] {
					("paying", new Criterion(3)),
				}),
				speaker: $"You",
				text: $"Let me pay, it was a pleasure meeting you tonight.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("payingAllOfIt", 1, Query.CompType.Increment),
					("paying", -1, Query.CompType.Set),
					(happy, 1, Query.CompType.Increment),
					(flirty, 1, Query.CompType.Increment),
				}
				),
		
		/* <105_LET_ME_PAY_RESPONSE */
		new Dia(rule: new Rule(new[] {
					("payingAllOfIt", new Criterion(2)),
				}),
				speaker: $"{_datesName}",
				text: $"Thank you so much, it was nice to meet you.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("payingAllOfIt", -1, Query.CompType.Set),
					// ("paying", -1, Query.CompType.Set),
					("dateOver", 1, Query.CompType.Set),
				}
				),
		
		/* <105_SPLIT_BILL */
		new Dia(rule: new Rule(new[] {
					("paying", new Criterion(4)),
				}),
				speaker: $"You",
				text: $"Is it okay if we split the bill tonight?",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("paying", -1, Query.CompType.Set),
					("splitTheBill", 1, Query.CompType.Set),
					(neutral, 1, Query.CompType.Increment),
					(angry, 1, Query.CompType.Increment),
				}
				),
		
		/* <105_SPLIT_BILL_ANGRY_RESPONSE */
		new Dia(rule: new Rule(new[] {
					("splitTheBill", new Criterion(1)),
					(highestMood, new Criterion(angryInt)),
				}),
				speaker: $"{_datesName}",
				text: $"Seriously...",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("splitTheBill", -1, Query.CompType.Set),
					(neutral, 1, Query.CompType.Increment),
					(angry, 1, Query.CompType.Increment),
					("dateOver", 1, Query.CompType.Set),
				}
				),
		
		/* <105_SPLIT_BILL_NEUTRAL_RESPONSE */
		new Dia(rule: new Rule(new[] {
					("splitTheBill", new Criterion(2)),
				}),
				speaker: $"{_datesName}",
				text: $"Okay let's split the bill.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("splitTheBill", -1, Query.CompType.Set),
					(neutral, 1, Query.CompType.Increment),
					("dateOver", 1, Query.CompType.Set),
				}
				),

		/* PAY_DAY_2_ABOVE_105 */
		new Dia(rule: new Rule(new[] {
					("paying", new Criterion(2)),
					(cost ,new Criterion(105, int.MaxValue)),
				}),
				speaker: $"Waiter",
				text: $"The total bill is at {totalCost}",
				choiceText: new string[] {
					$"{_next}"
				},
				writeBacks: new[] {
					("paying", -1, Query.CompType.Set),
					("cannotAfford", 1, Query.CompType.Set),
				}
				),
		
		/* PAY_DAY_2_ABOVE_105 */
		new Dia(rule: new Rule(new[] {
					("cannotAfford" ,new Criterion(1)),
				}),
				speaker: $"Narrator",
				text: $"It seems that the lobster you ordered earlier was a little over your budget. Now you don’t have enough money to pay for the food.",
				choiceText: new string[] {
					$"{_next}"
				},
				writeBacks: new[] {
					("cannotAfford", 1, Query.CompType.Increment),
				}
				),
		
		/* PAY_DAY_2_ABOVE_105_2 */
		new Dia(rule: new Rule(new[] {
					("cannotAfford" ,new Criterion(2)),
				}),
				speaker: $"You",
				text: $"What should I do?",
				choiceText: new string[] {
					"Ask your date to pay.",
					"I have to go to the bathroom real quick...",
				},
				writeBacks: new[] {
					("cannotAfford", 3, Query.CompType.Set),
					("cannotAfford", 4, Query.CompType.Set),
				}
				),
		
		/* ASK_DATE_TO_PAY */
		new Dia(rule: new Rule(new[] {
					("cannotAfford" ,new Criterion(3)),
				}),
				speaker: $"You",
				text: $"Uhh... it seems like I don’t have enough money to pay, maybe you could pay this time?",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("cannotAfford", -1, Query.CompType.Set),
					("askDateToPay", 1, Query.CompType.Set),
				}
				),
		
		/* ASKED_DATE_TO_PAY_ANGRY */
		new Dia(rule: new Rule(new[] {
					("askDateToPay" ,new Criterion(1)),
					(angry ,new Criterion(2, int.MaxValue)),
				}),
				speaker: $"{_datesName}",
				text: $"This has to be a joke. I’m going to have nightmares about this date for the rest of my life.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("cannotAfford", -1, Query.CompType.Set),
					("askDateToPay", -1, Query.CompType.Set),
					("dateOver", 1, Query.CompType.Set),
				}
				),
		
		/* ASKED_DATE_TO_PAY_NEUTRAL */
		new Dia(rule: new Rule(new[] {
					("askDateToPay" ,new Criterion(1)),
				}),
				speaker: $"{_datesName}",
				text: $"Sure I can pay this time.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("cannotAfford", -1, Query.CompType.Set),
					("askDateToPay", -1, Query.CompType.Set),
					("dateOver", 1, Query.CompType.Set),
				}
				),
		
		/* DINE_AND_DASH */
		new Dia(rule: new Rule(new[] {
					("cannotAfford" ,new Criterion(4)),
				}),
				speaker: $"You",
				text: $"Wait here, I just need to use the bathroom before I pay...",
				choiceText: new string[] {
					"Climb out through the bathroom window",
				},
				writeBacks: new[] {
					("cannotAfford", -1, Query.CompType.Set),
					("dateOver", 1, Query.CompType.Set),
					("smoothCriminal", 1, Query.CompType.Set),
				}
				),
		
		
		
		
		

		/* ENDING 1/7 I'LL CALL YOU LATER */
		new Dia(rule: new Rule(new[] {
					("dateOver" ,new Criterion(1)),
					("highestMood", new Criterion(0)),
				}),
				speaker: $"Narrator",
				text: $"Ending 1/7\n\"I'LL CALL YOU LATER\"\nGood job. You’re not a complete failure after all since you just scored yourself a second date.",
				choiceText: new string[] {
					"The End",
				},
				writeBacks: new[] {
					("callYouLater", -1, Query.CompType.Set),
				}
				),
		
		/* ENDING 2/7 SPEND_THE_NIGHT */
		new Dia(rule: new Rule(new[] {
					("dateOver" ,new Criterion(1)),
					("highestMood", new Criterion(1)),
				}),
				speaker: $"Narrator",
				text: $"Ending 2/7\n\"SPEND THE NIGHT\"\nWoah. How did you do that? Your date actually asked you to spend the night at their place?",
				choiceText: new string[] {
					"The End",
				},
				writeBacks: new[] {
					("spendTheNight", -1, Query.CompType.Set),
				}
				),
		
		/* ENDING 3/7 WORST_DATE_EVER */
		new Dia(rule: new Rule(new[] {
					("dateOver" ,new Criterion(1)),
					("highestMood", new Criterion(2)),
				}),
				speaker: $"Narrator",
				text: $"Ending 3/7\n\"Worst. Date. Ever.\"\nYeah... I kinda saw that one coming.\nIn fact I’m surprised your date stayed until the end.",
				choiceText: new string[] {
					"The End",
				},
				writeBacks: new[] {
					("worstDateEver", -1, Query.CompType.Set),
				}
				),
		
		/* ENDING 4/7 Zzzzz */
		new Dia(rule: new Rule(new[] {
					("dateOver" ,new Criterion(1)),
					("highestMood", new Criterion(3)),
				}),
				speaker: $"Narrator",
				text: $"Ending 4/7\n\"Zzzzz\"\nOh come on, you definitely were'nt that boring...",
				choiceText: new string[] {
					"The End",
				},
				writeBacks: new[] {
					("zzzzz", -1, Query.CompType.Set),
				}
				),
		
		/* ENDING 5/7 TONE_DEAF */
		new Dia(rule: new Rule(new[] {
					("toneDeaf" ,new Criterion(1)),
				}),
				speaker: $"Narrator",
				text: $"Ending 5/7\n\"TONE DEAF\"\nYour date hurried out of the restaurant with a bright red face. I mean... What did you expect?",
				choiceText: new string[] {
					"The End",
				},
				writeBacks: new[] {
					("toneDeaf", -1, Query.CompType.Set),
				}
				),
		
		/* ENDING 6/7 DON'T_CALL_ME */
		new Dia(rule: new Rule(new[] {
					("dateOver" ,new Criterion(1)),
					("dontCallMe" ,new Criterion(1)),
				}),
				speaker: $"Narrator",
				text: $"Ending 6/7\n\"DON'T CALL ME\"\nYou kinda deserved that one... Talk about wasting someone's time.",
				choiceText: new string[] {
					"The End",
				},
				writeBacks: new[] {
					("dontCallMe", -1, Query.CompType.Set),
				}
				),
		
		/* ENDING 7/7 SMOOTH_CRIMINAL */
		new Dia(rule: new Rule(new[] {
					("dateOver" ,new Criterion(1)),
					("smoothCriminal" ,new Criterion(1)),
				}),
				speaker: $"Narrator",
				text: $"Ending 7/7\n\"SMOOTH CRIMINAL\"\nHey I'm pretty sure that's illegal.\nCome back here!\nHello?",
				choiceText: new string[] {
					"The End",
				},
				writeBacks: new[] {
					("smoothCriminal", -1, Query.CompType.Set),
				}
				),
		
		
		
		
		

		// DRINK WINE
		new Dia(rule: new Rule(new[] {
					("waiter", new Criterion(6)),	// drinkWine
				}),
				speaker: $"You",
				text: $"...",
				choiceText: new string[] {
					"slurp",
				},
				writeBacks: new[] {
					(neutral, 1, Query.CompType.Increment),
					(drinkWine, 1, Query.CompType.Set),
					("waiter", 3, Query.CompType.Set),
				}
				),
		new Dia(rule: new Rule(new[] {
					(drinkWine, new Criterion(2)),
				}),
				speaker: $"You",
				text: $"This is a very good year, indeed",
				choiceText: new string[] {
					"slurp",
				},
				writeBacks: new[] {
					(neutral, 1, Query.CompType.Increment),
					(drinkWine, 1, Query.CompType.Increment),
				}
				),
		new Dia(rule: new Rule(new[] {
					(drinkWine, new Criterion(4)),
				}),
				speaker: $"You",
				text: $"I think red is the best color of wine",
				choiceText: new string[] {
					"slurp",
				},
				writeBacks: new[] {
					(neutral, 1, Query.CompType.Increment),
					(drinkWine, 1, Query.CompType.Increment),
				}
				),
		new Dia(rule: new Rule(new[] {
					(drinkWine, new Criterion(6)),
				}),
				speaker: $"You",
				text: $"Doctors say that ONE glass a day is good for your heart. How good won't it be after the whole bottle?",
				choiceText: new string[] {
					"slurp",
				},
				writeBacks: new[] {
					(neutral, 1, Query.CompType.Increment),
					(drinkWine, 1, Query.CompType.Increment),
				}
				),
		new Dia(rule: new Rule(new[] {
					(drinkWine, new Criterion(8)),
				}),
				speaker: $"You",
				text: $"Cheers!",
				choiceText: new string[] {
					"slurp",
				},
				writeBacks: new[] {
					(neutral, 1, Query.CompType.Increment),
					(drinkWine, 1, Query.CompType.Set),	// starts wine loop over
				}
				),
		
	};
}