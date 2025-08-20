Crimson Tactics – Programming Test

This repository contains my implementation of the Crimson Tactics programming assignment. The goal was to create a simple isometric tactics-style grid system in Unity, complete with obstacle handling, pathfinding, and basic AI.

The project was built with Unity 2022.3.20f1 (LTS).


Features :

1. Grid System

- Generates a 10x10 grid of cube tiles at runtime.

- Each tile stores its own grid coordinates and obstacle state.

- Hovering over a tile with the mouse shows its (x, y) position on the UI.

2. Obstacle Editor Tool

- Includes a custom Unity editor window with a toggle grid.

- Developers can mark tiles as blocked by toggling buttons.

- Obstacle data is stored in a ScriptableObject, making it easy to edit and save.

- At runtime, blocked tiles are visualized with red spheres placed on top of them.

3. Player Movement & Pathfinding

- Player can click on any tile to move there.

- Pathfinding avoids obstacles and chooses the shortest route.

- Implemented using a grid-based BFS/A* search (no Unity NavMesh).

- Movement happens tile by tile, with input disabled until the unit stops moving.

4. Enemy AI

- Enemy unit tries to move to a tile adjacent to the player.

- Uses the same pathfinding algorithm as the player.

- Designed with an IUnitAI interface for clean OOP architecture.



NOTE: THIS REPOSITARY ONLY INCLUDES SCRIPTS OF THE GAME NOT AN ACTUAL GAME.









- Enemy waits until the player finishes moving before taking its turn
