public class MiniGameQTE : MiniGame {
	/* PRIVATE VARIABLES */
	private int _sequenceLength;

	/* PROPERTIES */
	public int SequenceLength {
		get { return _sequenceLength; }
	}

	/* INITIALIZATION */
	public MiniGameQTE(int pPlayerIndex) : base(pPlayerIndex) {
		_instructions = "Complete the Sequence!";
		_sequenceLength = 0;

		InitTask();
	}

	private void InitTask() {
		for(int i = 0; i < _sequenceLength; i++) {
			_task.Enqueue(new ButtonAction(GetRandomControl()));
		}
	}
}