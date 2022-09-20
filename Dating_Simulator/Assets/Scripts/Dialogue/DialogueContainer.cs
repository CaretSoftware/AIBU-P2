public class DialogueContainer {

	private static readonly string _next  = "continue";
	private static readonly int    @false = 0;
	private static readonly int    @true  = 1;

	private static string _commonInterest = "fish";
	private static string _randomInterest = "random interest";
	private static string _datesName      = "Amy";

	private static string happy        = "happy";
	private static string flirty       = "flirty";
	private static string embarrassed  = "embarrassed";
	private static string angry        = "angry";
	private static string bored        = "bored";
	private static string interest     = "interest";
	private static string compliment   = "compliment";
	private static string joke         = "joke";
	private static string likesJokes   = "likesJokes";
	private static string selfInterest = "selfInterest";


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
				}
				),

		/* EATING ACTIVITY */
		// *	interest = 5, waiter = 4
		new Dia(rule: new Rule(new[] {
					("waiter", new Criterion(3)),
				}),
				speaker: $"Your inner voice",
				text: $"What do I do while we're eating?",
				choiceText: new string[] {
					"Eat in silence",
					"Ask about the food",
					"Drink wine",
					"Serenade my date",
				},
				writeBacks: new[] {
					("eatSilence", 1, Query.CompType.Set),
					("askFood", 1, Query.CompType.Set),
					("drinkWine", 1, Query.CompType.Increment),
					("serenade", 1, Query.CompType.Set),
				}
				),

		/* EATING SILENCE */
		// *	eatSilence = 1 
		new Dia(rule: new Rule(new[] {
					("eatSilence", new Criterion(1)),
				}),
				speaker: $"...",
				text: $"...",
				choiceText: new string[] {
					$"...",
				},
				writeBacks: new[] {
					(bored, 1, Query.CompType.Increment),
					(embarrassed, 1, Query.CompType.Increment),
				}
				),

		/* FOOD DELICIOUS PLAYER_SALAD */
		// *	interest = 5, waiter = 1, askFood = 1, salad = 1
		new Dia(rule: new Rule(new[] {
					("askFood", new Criterion(1)),
					("playerSalad", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"This salad was really delicious!",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("neutral", 1, Query.CompType.Increment),
					("askFood", 1, Query.CompType.Increment),
				}
				),
		
		/* FOOD DELICIOUS PLAYER_BURGER */
		// *	interest = 5, waiter = 1, askFood = 1, salad = 1
		new Dia(rule: new Rule(new[] {
					("askFood", new Criterion(1)),
					("playerBurger", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"This burger was delicious!",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("neutral", 1, Query.CompType.Increment),
					("askFood", 1, Query.CompType.Increment),
				}
				),
		
		/* FOOD DELICIOUS PLAYER_STEAK */
		// *	interest = 5, waiter = 1, askFood = 1, salad = 1
		new Dia(rule: new Rule(new[] {
					("askFood", new Criterion(1)),
					("playerSteak", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"This salad was really delicious!",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("neutral", 1, Query.CompType.Increment),
					("askFood", 1, Query.CompType.Increment),
				}
				),
		
		/* FOOD DELICIOUS PLAYER_LOBSTER */
		// *	interest = 5, waiter = 1, askFood = 1, salad = 1
		new Dia(rule: new Rule(new[] {
					("askFood", new Criterion(1)),
					("playerLobster", new Criterion(1)),
				}),
				speaker: $"You",
				text: $"This lobster was so delicious!",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					("neutral", 1, Query.CompType.Increment),
					("askFood", 1, Query.CompType.Increment),
				}
				),

		/* ASK FOOD FOLLOWUP DATE_SALAD */
		// *	interest = 5, waiter = 1, askFood = 1, salad = 1
		new Dia(rule: new Rule(new[] {
					("askFood", new Criterion(2)),
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
					("askFood", new Criterion(2)),
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
					("askFood", new Criterion(2)),
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
					("askFood", new Criterion(2)),
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
					(interest, new Criterion(5)),
					("waiter", new Criterion(1)),
					("askFood", new Criterion(2)),
					("lobster", new Criterion(1)),
				}),
				speaker: $"{_datesName}",
				text: $"The steak tastes very good",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(happy, 1, Query.CompType.Increment),
					("askFood", 1, Query.CompType.Increment),
				}
				),

		/* ANSWER STEAK */
		// *	interest = 5, waiter = 1, askFood = 2, steak = 1
		new Dia(rule: new Rule(new[] {
					(interest, new Criterion(5)),
					("waiter", new Criterion(1)),
					("askFood", new Criterion(2)),
					("lobster", new Criterion(1)),
				}),
				speaker: $"{_datesName}",
				text: $"The steak tastes very good",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(happy, 1, Query.CompType.Increment),
					("askFood", 1, Query.CompType.Increment),
				}
				),

		// TODO make a choice dialogue for the dates food too

		/* ANSWER BURGER */
		// *	interest = 5, waiter = 1, askFood = 2, burger = 1
		new Dia(rule: new Rule(new[] {
					(interest, new Criterion(5)),
					("waiter", new Criterion(1)),
					("askFood", new Criterion(2)),
					("burger", new Criterion(1)),
				}),
				speaker: $"{_datesName}",
				text: $"It was alright. Nothing special.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(happy, 1, Query.CompType.Increment),
					("askFood", 1, Query.CompType.Increment),
				}
				),
		
		/* ANSWER BURGER PLAYER_LOBSTER */
		// *	interest = 5, waiter = 1, askFood = 2, burger = 1
		new Dia(rule: new Rule(new[] {
					(interest, new Criterion(5)),
					("waiter", new Criterion(1)),
					("askFood", new Criterion(2)),
					("burger", new Criterion(1)),
					("playerLobster", new Criterion(1)),
				}),
				speaker: $"{_datesName}",
				text: $"It was okay... Nothing special.",
				choiceText: new string[] {
					$"{_next}",
				},
				writeBacks: new[] {
					(angry, 1, Query.CompType.Increment),
					("askFood", 1, Query.CompType.Increment),
				}
				),
	};
}
