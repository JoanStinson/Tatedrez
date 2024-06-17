# Tatedrez
A basic tatedrez mobile game implementation done in a few days.

<p align="center">
  <a>
    <img alt="Made With Unity" src="https://img.shields.io/badge/made%20with-Unity-57b9d3.svg?logo=Unity">
  </a>
  <a>
    <img alt="License" src="https://img.shields.io/github/license/JoanStinson/Tatedrez?logo=github">
  </a>
  <a>
    <img alt="Last Commit" src="https://img.shields.io/github/last-commit/JoanStinson/Tatedrez?logo=Mapbox&color=orange">
  </a>
  <a>
    <img alt="Repo Size" src="https://img.shields.io/github/repo-size/JoanStinson/Tatedrez?logo=VirtualBox">
  </a>
  <a>
    <img alt="Downloads" src="https://img.shields.io/github/downloads/JoanStinson/Tatedrez/total?color=brightgreen">
  </a>
  <a>
    <img alt="Last Release" src="https://img.shields.io/github/v/release/JoanStinson/Tatedrez?include_prereleases&logo=Dropbox&color=yellow">
  </a>
</p>

<p align="center">
  <img height="400" src="https://github.com/JoanStinson/Tatedrez/blob/main/UserImages/Preview.PNG">
</p>

## 💡 Implementation
A document explaining a bit the implementation process and project features can be found in the [Readme.pdf](https://github.com/JoanStinson/Tatedrez/blob/main/Readme.pdf) file.
<p align="center">
  <img src="https://github.com/JoanStinson/Tatedrez/blob/main/UserImages/Implementation01.PNG">
</p>
<p align="center">
  <img src="https://github.com/JoanStinson/Tatedrez/blob/main/UserImages/Implementation02.PNG">
</p>

## ⚙️ Installation

## 📜 Kata Rules
* **Pieces:**
    The game has only 3 pieces. Knight, Bishop and Rook:
    * Knight (Horse): The knight moves in an L-shape: two squares in one direction (either horizontally or vertically), followed by one square perpendicular to the previous direction. Knights can jump over other pieces on the board, making their movement unique. Knights can move to any square on the board that follows this L-shaped pattern, regardless of the color of the squares.
    * Rook: The rook moves in straight lines either horizontally or vertically. It can move any number of squares in the chosen direction, as long as there are no pieces blocking its path.
    * Bishop: The bishop moves diagonally on the board. It can move any number of squares diagonally in a single move, as long as there are no pieces obstructing its path.
      
* **Board Setup:**
    An empty board is placed, consisting of a 3x3 grid, similar to a Tic Tac Toe game.
    <p align="center">
      <img width="320" src="https://github.com/JoanStinson/Tatedrez/blob/main/UserImages/RulesImage01.png">
    </p>

* **Piece Placement:**
    Choose a random player to start.  
    Player 1 places one of their pieces in an empty square on the board.  
    Player 2 places one of their pieces in another empty square on the board.  
    They continue alternating until both players have placed their three pieces on the board.
    <p align="center">
      <img width="320" src="https://github.com/JoanStinson/Tatedrez/blob/main/UserImages/RulesImage02.png">
    </p>

* **Checking for TicTacToe:**
    After all players have placed their three pieces on the board, it's checked whether anyone has managed to create a line of three pieces in a row, column, or diagonal – a TicTacToe.

* **Dynamic Mode:**
    If neither player has achieved a TicTacToe with the placed pieces, the game enters the dynamic mode of Tateddrez.
    If X player can't move, the other player move twice.
    In this mode, players take turns to move one of their pieces following chess rules.
    **Capturing opponent's pieces is not allowed.**

* **Seeking TicTacToe:**
    In dynamic mode, players strategically move their pieces to form a TicTacToe.  
    They continue moving their pieces in turns until one of them achieves a TicTacToe with their three pieces.
    <p align="center">
      <img width="320" src="https://github.com/JoanStinson/Tatedrez/blob/main/UserImages/RulesImage03.png">
    </p>

* **Game Conclusion:**
    The game of Tateddrez concludes when one of the players manages to achieve a TicTacToe with their three pieces, either during the initial placement phase or during dynamic mode.  
    The player who achieves the TicTacToe is declared the winner.
    <p align="center">
      <img width="320" src="https://github.com/JoanStinson/Tatedrez/blob/main/UserImages/RulesImage04.png">
    </p>
