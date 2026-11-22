# Four in a Row — Windows Forms Game (C# / .NET)

A desktop version of the classic *Connect Four* game, written in C# with Windows Forms. Two players take turns dropping tokens into a vertical grid, and the first to line up four in a row — horizontally, vertically or diagonally — wins the round. You can play against a friend on the same computer or against a computer opponent.

## Features

- **Settings screen** before the game starts: player names, two-player or vs-computer mode, and board size.
- **Adjustable board size** from 4×4 up to 10×10 (default 4 rows × 7 columns).
- **Two-player mode** (local, same computer) or **single-player mode** against the computer.
- **Computer opponent** that:
  1. plays a winning move if one is available,
  2. otherwise blocks the human player's winning move,
  3. otherwise prefers the center columns (75% of the time) with some randomness, so games don't repeat.
- **Win and tie detection** across rows, columns and both diagonal directions.
- **Running score** across rounds, with an "Another round?" prompt after each win or tie.
- Column buttons are disabled automatically when a column fills up.

## How to play

1. On the **Game Settings** screen, enter Player 1's name.
2. To play against another person, tick **Player 2** and enter their name. Leave it unticked to play against the computer.
3. Choose the number of rows and columns and click **Start!**
4. Click a numbered button above a column to drop your token into it. Player 1 is `X`, Player 2 is `O`.
5. Get four of your tokens in a line to win the round. After each round, choose whether to play another one; scores are kept until you close the game.

## Architecture

The game logic is kept separate from the user interface, so the board and rules have no dependency on Windows Forms.

```
Program              entry point: shows the settings form, then the game form

UI (Windows Forms)
├── FormGameSettings  player names, game mode, board size
└── FormGame          board grid, column buttons, scores, end-of-round dialogs

Game logic
├── GameManager       turn order, moves, win/tie checks, scoring, restarts
├── Board             grid state, token dropping, win detection, undo, reset
├── Player            name, score, and the computer opponent's move selection
├── ePlayer           None / Player1 / Player2
└── eDropResult       Success / FullColumn / OutOfBounds
```

The computer opponent looks ahead one move by trying each column on the real board, checking for a winner, and undoing the move (`Board.UndoMove`), so no copy of the board is needed.

## Building and running

**Requirements:** Windows and Visual Studio with the ".NET desktop development" workload.

The repository contains the source files only (no project file). To build it:

1. In Visual Studio, create a new **Windows Forms App (.NET Framework)** project.
2. Delete the generated `Form1.cs` and `Program.cs`.
3. Add all the `.cs` files from this repository to the project (**Project → Add Existing Item…**).
4. Build and run (`F5`).

The code is all in the `Ex_05` namespace and builds the forms in code, so no designer files are needed.

## Author

**Ben Jaitin** — [github.com/Salaryb](https://github.com/Salaryb)
