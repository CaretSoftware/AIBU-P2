using System;
using System.Collections;

/*
 *	TODO: Decide on JSON file format
 *	TODO: Json file reader
 *	TODO: Optimisation - Convert string and bool comparisons to numbers
 *	TODO:  
 *	TODO: Optimisation — #2 Different arrays of facts 
 *	TODO: Optimisation — #3 Sorting Algorithm for number of criteria, buckets of commonly grouped criteria etc. 
 *	TODO: Optimisation — #4 Partitioning rules by region
 */

public class Rules {
	public static Rule[] RuleList => _rules;

	private static readonly Rule[] _rules = {
		new Rule(new[] {
			("who", new Criterion(1,          1)),
			("context", new Criterion(5,      5)),
			("level", new Criterion(1,        1)),
			("hitBy", new Criterion(0,        0)),
			("nearbyAllies", new Criterion(2, 2)),
			("inDanger", new Criterion(1,     1)),
			("hitPoints", new Criterion(75,   100))
		}),
		new Rule(new[] {
			("who", new Criterion(1,          1)),
			("context", new Criterion(5,      5)),
			("level", new Criterion(1,        1)),
			("hitBy", new Criterion(1,        1)),
			("nearbyAllies", new Criterion(2, 2)),
			("inDanger", new Criterion(1,     1)),
			("hitPoints", new Criterion(75,   100))
		}),
		new Rule(new[] {
			("who", new Criterion(1,          1)),
			("context", new Criterion(5,      5)),
			("hitBy", new Criterion(1,        1)),
			("nearbyAllies", new Criterion(0, 0)),
			("inDanger", new Criterion(1,     1)),
			("hitPoints", new Criterion(75,   100))
		}),
		new Rule(new[] {
			("who", new Criterion(1,          1)),
			("nearbyAllies", new Criterion(1, int.MaxValue)),
			("hitPoints", new Criterion(0,    0))
		}),
		new Rule(new[] {
			("speaker", new Criterion(1,   1)),
			("listener", new Criterion(1,  1)),
			("hitPoints", new Criterion(0, 0))
		}),
	};
}


public struct Rule {
	public (string key, Criterion crit)[] Criteria { get; private set; }
	public int Length => Criteria.Length;

	public Rule((string key, Criterion crit)[] criteria) {
		Criteria = criteria;
	}
}

// public struct Rule {
// 	public (string key, Criterion crit)[] Criteria { get; private set; }
// 	public Dialog dialog { get; private set; }
// 	public int Length => Criteria.Length;
//
// 	public Rule((string key, Criterion crit)[] criteria, Dialog response) {
// 		Criteria = criteria;
// 		dialog	= response;
// 	}
// }

