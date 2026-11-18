# Sentinel Skies

**2D tower-defense game built with Unity and C#**

Sentinel Skies is a tower-defense game in which players place and upgrade defensive turrets to stop successive waves of enemies from reaching the end of a path. The project explores gameplay systems in Unity, including 2D physics, enemy spawning and movement, resource management, upgrades, menus, and level progression.

> **Repository contents:** This repository provides C# gameplay scripts and a packaged **macOS** application. It does **not** include a complete Unity Editor project (such as the original `Assets/`, scenes, and `ProjectSettings/` folders), so the source code alone is not sufficient to rebuild the game in Unity.

## Gameplay Features

- **Wave-based defense:** Spawn enemy groups over time, with wave size and spawning frequency scaling as rounds progress.
- **Waypoint navigation:** Enemies follow a predefined level path; enemies that reach the end reduce the player's remaining lives.
- **Tower placement and economy:** Select and purchase turrets, place them on available plots, and earn currency when enemies are defeated.
- **Projectile turrets:** Track nearby enemies, rotate toward targets, and fire projectiles that deal damage.
- **Freezing turrets:** Temporarily slow enemies within range.
- **Tower upgrades and selling:** Spend currency to improve towers' firing speed or effect frequency and range; sell towers to recover a configured amount.
- **Level and menu systems:** Display win/lose screens, pause gameplay, navigate menus, and track level unlocks with Unity `PlayerPrefs`.
- **Settings:** Includes C# controls for resolution, fullscreen, volume, graphics quality, and a grayscale post-processing option.

Features are described from the published scripts; some depend on Unity scenes, assets, and Inspector references that are not included as editable project files in this repository.

## Technologies and Concepts

| Technology / Concept | How it is used |
| --- | --- |
| **Unity** | Game engine, scene management, UI, prefabs, coroutines, and lifecycle methods |
| **C#** | Object-oriented game logic and component scripts |
| **Unity 2D Physics** | Rigidbody2D movement, collisions, and enemy detection |
| **Unity Events** | Notify the wave manager when enemies are destroyed or leave the path |
| **TextMeshPro / Unity UI** | Game menus, stats displays, and tower upgrade controls |
| **PlayerPrefs** | Persist which levels have been unlocked |

## How to Play

1. Choose a level using the game's menus.
2. Select a turret from the available tower choices.
3. Click a free plot to purchase and place the turret, provided you have enough currency.
4. Defend the path as enemies arrive in waves. Defeating enemies awards currency.
5. Click an existing turret to inspect upgrade or sell controls.
6. Preserve your remaining lives to progress.

**Controls visible in the code:**

- **Mouse:** Interact with plots and on-screen menus.
- **P:** Toggle the pause menu while in a level.
- **Other controls:** Depend on the packaged game's scene and UI configuration.

## Running the Game

### macOS build

The repository includes a packaged application at:

[`Executables/Sentinel Skies.app`](Executables/Sentinel%20Skies.app)

To try the included build:

1. Download the repository using **Code → Download ZIP** and extract it.
2. Open the `Executables` folder on a Mac.
3. Launch `Sentinel Skies.app` from Finder.

**Compatibility note:** The presence of a macOS app bundle does not guarantee compatibility with every current macOS version or Apple Silicon configuration. This documentation update did not execute the game, and you should follow your system's normal app-security checks.

The previous README mentioned Windows 10 or later. **No Windows executable is currently included in this repository**, so Windows availability cannot be verified from the published files.

### Working with the source code

The `Scripts/` directory contains C# scripts used in the game. These can be inspected individually to study the implementation, but **opening the repository as a complete Unity project or rebuilding it requires the original Unity scenes, assets, prefabs, and project settings**.

## Architecture and Code Tour

The main gameplay systems are divided into focused Unity components:

| Script | Responsibility |
| --- | --- |
| [`EnemySpawner.cs`](Scripts/EnemySpawner.cs) | Schedules waves, scales enemy counts and spawn rates, tracks remaining enemies |
| [`EnemyMovement.cs`](Scripts/EnemyMovement.cs) | Moves enemies through waypoints and deducts lives at the path endpoint |
| [`Health.cs`](Scripts/Health.cs) | Handles enemy damage, health-bar updates, destruction, and currency rewards |
| [`BuildManager.cs`](Scripts/BuildManager.cs) | Tracks which turret type is selected |
| [`Plot.cs`](Scripts/Plot.cs) | Handles placement, purchasing, plot highlighting, and selecting existing turrets |
| [`Turret.cs`](Scripts/Turret.cs) | Finds targets, rotates toward them, fires projectiles, and manages upgrades |
| [`TurretFreeze.cs`](Scripts/TurretFreeze.cs) | Slows nearby enemies temporarily and manages upgrades |
| [`Bullet.cs`](Scripts/Bullet.cs) | Moves projectiles toward targets and applies collision damage |
| [`LevelManager.cs`](Scripts/LevelManager.cs) | Manages currency, pause, completion, and game-over conditions |
| [`CompletedLevel.cs`](Scripts/CompletedLevel.cs) | Unlocks and loads available subsequent levels |
| [`MainMenu.cs`](Scripts/MainMenu.cs) | Provides menu navigation and resets/loads level progression |
| [`Settings.cs`](Scripts/Settings.cs) | Exposes graphics, audio, and display settings |

### Game-loop overview

~~~text
EnemySpawner creates a wave
             |
             v
EnemyMovement follows level waypoints
             |
             +----> Turret / TurretFreeze detects enemies
             |                 |
             |                 v
             |          Damage or slow effects
             |                 |
             |                 v
             |          Health tracks defeat
             |                 |
             |                 v
             |           Currency reward
             |
             +----> Enemy reaches end --> Lose a life

LevelManager checks loss and completion conditions
~~~

## Implementation Details

### Scaling waves

`EnemySpawner.cs` calculates the number of enemies and their spawn rate using the current wave number and tunable parameters such as `baseEnemies`, `difficultyScalingFactor`, and `enemiesPerSecondCap`. The game uses a coroutine to wait between waves.

### Tower upgrades

Projectile turrets can increase firing rate and targeting range, with upgrades capped at level 5 in the script. Freezing turrets can increase freeze frequency and range, with upgrades capped at level 3. Both use configurable costs and sell values.

### Event-driven enemy tracking

`EnemySpawner.onEnemyDestroy` is a Unity event used to update the count of active enemies when one is defeated or reaches the end of the path. This helps determine when the next wave can begin.

### Level persistence

The project uses `PlayerPrefs` to save a `levelReached` value. This **tracks unlocked levels**, not a complete save of tower placements or in-progress gameplay.

## Current Repository Limitations

- Only the **macOS application bundle** is published; no Windows executable is available here.
- The original Unity project, editable scene files, assets, and prefabs are not included.
- There is no automated test suite or continuous-integration workflow in this repository.
- Some UI actions, including portions of the tutorial and leaderboard, are placeholders in the published C# scripts.
- Compatibility and gameplay of the packaged application have not been independently tested for this documentation update.

## Portfolio Notes

This project demonstrates C# programming, object-oriented design, 2D gameplay logic, resource and state management, event-driven interactions, and user-interface integration in Unity.

**Suggested future addition:** A screenshot or short gameplay GIF would make the project easier to evaluate without downloading the macOS application.

---

*Sentinel Skies — Unity/C# tower-defense game project.*
