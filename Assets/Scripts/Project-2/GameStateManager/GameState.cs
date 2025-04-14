
public enum GameState {

    None = 0,
    Start = 1 << 0,
    Game = 1 << 1,
    LevelCompleted = 1 << 2,
    LevelFailed = 1 << 3,

}