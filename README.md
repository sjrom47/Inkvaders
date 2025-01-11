# Inkzanity Unleashed 🎮

A multiplayer shooter game inspired by Splatoon, developed as a final project. Paint the arena, transform into squids, and compete in team-based matches!
<!-- TABLE OF CONTENTS -->
- [Inkzanity Unleashed 🎮](#inkzanity-unleashed-)
  - [Features ✨](#features-)
  - [System Requirements 🖥️](#system-requirements-️)
  - [Installation 🚀](#installation-)
  - [Controls 🎮](#controls-)
  - [Architecture 🏗️](#architecture-️)
  - [Future Development 🔮](#future-development-)
  - [Development Team 👨‍💻](#development-team-)

## Features ✨

- **Team-based Gameplay**: Configurable team sizes (default: 3v3)
- **Paint Mechanics**: 
  - Paint surfaces with team colors
  - Real-time surface painting and detection
  - Territory control through paint coverage
- **Character Abilities**:
  - Transform into a squid
  - Reload ammunition in squid form
  - Multiple weapon types
- **Combat System**:
  - Team-based combat
  - Health system
  - Damage from enemy paint
- **Game Modes**:
  - Time-limited matches (default: 90 seconds)
  - Territory control scoring
- **AI Implementation**:
  - NavMesh-based movement
  - State machine behavior system
  - Team-based AI coordination

## System Requirements 🖥️

- Unity (version specified in project settings)
- Sufficient GPU for shader processing
- Recommended: Dedicated graphics card for optimal performance

## Installation 🚀

1. Clone the repository
2. Open the project in Unity
3. Ensure all required assets are imported
4. Build the project or run directly in the Unity editor

## Controls 🎮

- WASD: Movement
- Mouse: Camera control and aiming
- Left Click: Shoot
- [Key]: Transform into squid (configurable)

## Architecture 🏗️

The project uses several design patterns:
- **Singleton**: Used for GameManager, PaintManager, and CollisionHandler
- **Bridge**: Implemented for weapons, colors, and particles systems
- **Command**: Controls player actions and AI behavior
- **State**: Manages player logic and AI behavior
- **Builder**: Used for player instantiation and configuration
- **Observer**: Handles game events and UI updates



## Future Development 🔮

Planned features:
- Additional weapon types with unique projectiles
- Wave-based enemy mode
- Shop system with weapon unlocks
- Wall-climbing squid mechanics
- Advanced power-up system
- Complete options menu with color selection
- Sound system implementation
- Save/load system completion

## Development Team 👨‍💻

- [Sergio Jiménez Romero](https://github.com/sjrom47)
- [Carlos Martínez Cuenca](https://github.com/carlosIMAT)
- [Alberto Velasco Rodríguez](https://github.com/Alberto-cd)

