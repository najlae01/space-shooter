# Galactic Defender

## Overview
**Galactic Defender** is a space shooter mobile game where players control a spaceship to navigate and survive against waves of enemies. The game features auto-firing bullets, allowing players to focus on dodging enemy attacks and maintaining strategic positioning within the game boundaries. The ultimate goal is to achieve high scores and progress through increasingly challenging levels.

## Game Scenes

### 1. Start Scene
The Start Scene is the initial entry point for the game. Here, players can tap on screen to start the game.
![startScene](https://github.com/najlae01/space-shooter/assets/88176530/147fc0c9-5e53-487d-8e84-639a81185eb9)


### 2. Levels for Testing
![scenes](https://github.com/najlae01/space-shooter/assets/88176530/8f75f8d4-d570-4711-935b-398f543bf84c)

These levels are designed to test and challenge players with varying conditions:
- **Win Condition Time**: The duration within which players must complete the level's objectives and enemies' type.
![winCondition](https://github.com/najlae01/space-shooter/assets/88176530/a7a8fa1a-6627-4acc-8731-110bd1d6d3ec)

- **Different Enemies**: Players face a variety of enemies, each with unique behaviors and characteristics to enhance the gameplay experience. The enemy types include:
    - **Meteors**: Meteors vary in size and speed, adding to the difficulty as players progress.
    - **Green Enemies**: These enemies move steadily towards the player's ship and cause significant damage upon collision. They introduce a direct threat that players must navigate around to survive.
    - **Purple Enemies**: Equipped with canons that fire bullets at intervals. They require players to constantly stay alert and maneuver strategically to dodge incoming fire while returning attacks.
![enemies](https://github.com/najlae01/space-shooter/assets/88176530/f5ed479e-5d58-4a9a-8e55-5075fae0a433)

- **Enemy Spawning Time**: The intervals at which enemies appear.
![enemiesSpawnTime](https://github.com/najlae01/space-shooter/assets/88176530/84f8cef9-fac5-4772-ad2a-32ef4eaaae22)

Progression mechanics:
- Players unlock new levels upon successfully completing the current level.

![winScreen](https://github.com/najlae01/space-shooter/assets/88176530/b1fd91e5-a76b-47ce-9891-6dfdae67c031)

- If a player fails, they can replay the level or choose another unlocked level.

![lostScreen](https://github.com/najlae01/space-shooter/assets/88176530/73563262-1a16-46e6-bd10-3452f296d513)


### 3. End Game Scene
This scene appears after the player wins or loses a level. It displays the player's score, the high score, and provides options to either quit the game or play the next level (Win case), replay the level (Lose case). Please refer to the figures above displaying the end game state.


## Game HUD
The game HUD includes a health bar located at the top left side of the screen, allowing players to monitor their ship's health. The score is displayed at the top right corner, keeping players informed of their current points.


## Game Controls

### Manual Control
Players can manually control their spaceship's movement across the x and y axes using touch input:
- Dragging on the screen moves the spaceship.
- Movement is constrained within set boundaries to keep the gameplay focused and challenging.

### Auto-Control Mode (Upcoming Feature)
In this mode, the spaceship is controlled by reinforcement learning algorithms. Players can switch between auto-control and manual control modes.

![autoControlMode](https://github.com/najlae01/space-shooter/assets/88176530/6b77ec78-430a-4ee2-8025-e4b9e90091f3)


## Extra and Upcoming Features
- **Auto-Control Mode**: Using Unity's ML-Agents plugin, this feature will allow the spaceship to be controlled by AI.

### Training Scene
The Training Scene is used to train the model using the ML Agents plugin and by setting multiple scenarios.
![trainingScene](https://github.com/najlae01/space-shooter/assets/88176530/3f8a1fd8-60d9-41e9-a142-bc628be03ae3)
![Capture d’écran (4947)](https://github.com/najlae01/space-shooter/assets/88176530/d7a70868-8268-48e4-a37b-dd81e9f93c17)

### Rewards and Penalties
- Agent receives rewards for maintaining a safe distance from enemies and successfully navigating levels.
- Penalties are applied for colliding with enemies or projectiles, moving out of bounds.


## Installation
1. Clone the repository.
2. Open the project in Unity.
3. Ensure you have the Unity ML-Agents package installed for the auto-control mode.
4. Build and run the game on your preferred mobile device emulator or physical device.

## How to Play
1. Launch the game to enter the Start Scene.
2. Tap to start a new game.
3. Navigate through levels, avoiding enemies and projectiles.
4. Progress through the game by completing levels and achieving high scores.

## Quit Game
Players can quit the game from the menu displayed after winning or losing a level by tapping the quit button.
![lostScreen](https://github.com/najlae01/space-shooter/assets/88176530/8ad3b2b5-1f46-4dfb-a1ca-ee3c9841abf4)


## License
This project is the final project of the CS50 game dev course.

## Development and Future Work
Further training and optimization of the reinforcement learning agent are needed. Continuous testing and parameter adjustments will improve the auto-control feature for a better gaming experience.

## License
This project is the final project of the CS50 game dev course.
