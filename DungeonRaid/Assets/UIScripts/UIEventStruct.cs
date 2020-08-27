public struct UIEvent { 

    public UIEvent (string type, int currentLevel, int EXPToNextLevel) {
        this.type = type;
        this.currentLevel = currentLevel;
        this.EXPToNextLevel = EXPToNextLevel;
	}

    public string type { get; internal set; }
    public int currentLevel { get; internal set; }
    public int EXPToNextLevel { get; internal set; }
}
