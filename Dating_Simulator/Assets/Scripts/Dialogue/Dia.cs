public class Dia {
	public (string, int, Query.CompType)[] writeBacks;

	public string   speaker;
	public string   text;
	public string[] ChoiceText;
	public Rule     rule;
	public Dia(Rule rule, string speaker, string text, string[] choiceText,
				params (string, int, Query.CompType)[] writeBacks) {
		this.rule       = rule;
		this.speaker    = speaker;
		this.text       = text;
		ChoiceText      = choiceText;
		this.writeBacks = writeBacks;
	}
}