public struct Criterion {
	private readonly float _min, _max;
	
	public Criterion(int exact) {
		_min = exact;
		_max = exact;
	}

	public Criterion(int min, int max) {
		_min = min;
		_max = max;
	}

	public bool Compare(int x) {
		return x >= _min && _max >= x;
	}
}