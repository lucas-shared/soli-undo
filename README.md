# Solitaire Game with Undo System

A simplified Solitaire card game implemented in Unity with a robust undo system. The game features drag-and-drop card movement between multiple stacks and a configurable undo system.

## 🎮 Features

- **Drag-and-Drop Card Movement**: Cards can be dragged between different stacks using mouse input
- **Undo System**: Players can revert their last move with a configurable number of undo steps (default: 10)
- **Multiple Stack Types**: Support for Foundation, Tableau, and Waste stack types
- **Visual Card Management**: Cards show face-up/face-down states with proper visual feedback
- **Event-Driven Architecture**: Clean communication between game components
- **Data-Driven Design**: Card definitions and game configuration through ScriptableObjects

## 🏗️ Architecture

The implementation is built on top of several key components:

### Command Pattern for Undo System
- `ICommand` interface defines the contract for executable/undoable actions
- `MoveCardCommand` implements card movement with proper state tracking
- `UndoManager` manages the command history stack with configurable limits

### Card Management System
- `Card` class handles individual card behavior, visuals, and drag-and-drop interaction
- `CardStack` manages collections of cards with type-specific placement rules
- `CardStackType` enum defines different stack behaviors (Foundation, Tableau, Waste)

### Game State Management
- `SolitaireGameManager` orchestrates the overall game flow
- `SolitaireUndoConfig` ScriptableObject provides centralized configuration
- `CardData` ScriptableObjects define card properties and artwork

## 🚀 Getting Started

### Prerequisites
- Unity 6000.2.6f2

### Installation
1. Clone the repository
2. Open the project in Unity
3. (Optional) Configure the `SolitaireUndoConfig` ScriptableObject with your desired setup
4. Press Play to start the game

### Usage
- **Move Cards**: Click and drag cards between stacks
- **Undo Move**: Click the undo button to revert your last action
- **Stack Types**: 
  - **Foundation**: For completed suits (currently accepts any card for demo)
  - **Tableau**: Main playing area (currently accepts any card for demo)
  - **Waste**: Temporary card storage

## 🔧 Technical Details

### Key Components
- **Event-Driven Architecture**: Cards communicate moves through events
- **Collision Detection**: Raycast-based stack detection for card placement
- **Visual Feedback**: Smooth card positioning with spacing and layering
- **Memory Management**: Proper cleanup of card objects and event subscriptions

### Design Patterns Used
- **Command Pattern**: For undo/redo functionality
- **Observer Pattern**: For event-driven communication

## 🎯 Future Improvements

### 1. Strategy Pattern for Stack Logic
Currently, stack placement rules are hardcoded in `CardStack.CanPlaceCard()` and accept any card. A strategy pattern would improve modularity and allow for introducing rules representing complex logic:

```csharp
public interface IStackPlacementStrategy
{
    bool CanPlaceCard(Card card, CardStack stack);
}

public class FoundationStrategy : IStackPlacementStrategy { }
public class TableauStrategy : IStackPlacementStrategy { }
public class WasteStrategy : IStackPlacementStrategy { }
```

**Benefits:**
- Easier addition of new stack types
- Complex placement rules
- Better testability of individual strategies

### 2. Enhanced Modularity in CardStack
The `CardStack` class currently handles multiple responsibilities. Splitting these would improve Single Responsibility Principle adherence:

- `CardsContainer` for storage and positioning
- `StackValidator` for placement rules with mentioned `IStackPlacementStrategy`  
- `StackView` for visual management

### 3. Single Responsibility in Card Class
The `Card` class currently handles multiple concerns. Breaking this into smaller, focused classes:

- `CardView` for rendering and art management
- `CardInput` for mouse interaction
- `CardMovement` for visual behavior and movement between stacks
- `CardState` for face-up/face-down logic

### 4. Game State Management
- **GameState System**: Implement a proper state machine for game flow management
  - `StartGame` state for initialization and setup
  - `Gameplay` state for active gameplay
  - `PauseGame` state for temporary suspension
  - `GameOver` state for end-game scenarios
  - `MenuState` for in-game menu navigation
- **State Transitions**: Smooth transitions between different game states

### 5. Assets Loading Management
- **Async Asset Loading**: Implement asynchronous loading for card sprites and game assets
- **Asset Bundles**: Organize assets into bundles for better memory management

### 6. Scene Management with Loading Phases
- **Scene Loading System**: Implement proper scene transitions with loading screens
- **Progress Indicators**: Show loading progress for each phase

### 7. Additional Gameplay Enhancements
- **Animation System**: Smooth card movement animations
- **Sound Effects**: Audio feedback for card movements
- **Game Rules**: Implement proper Solitaire rules
- **Save/Load System**: Persistent game state across sessions
- **Input System**: Migration to Unity's new Input System
- **Performance**: Object pooling for cards

## 🤖 AI assistance

### LLM used during development
- **Github Copilot**: Used for code completion in Rider IDE
- **ChatGPT** and **Gemini**: used for code snippets and architecture design support
- **Prompts Examples:**
- `You are an experienced Unity C# developer who writes clear, modular code and short step-by-step instructions. I’m working on a 2D card game prototype in Unity with card movement between 3 stacks. I now need an undo system to revert the last cards moves. Design and implement an undo system using the Command pattern. Provide: An interface ICommand{ void Execute(); void Undo(); }, class MoveCardCommand(Card card, CardStack fromStack, CardStack toStack), class UndoManager {void ExecuteCommand(ICommand command); bool UndoLastCommand();}`
- `You are an experienced Unity C# developer who writes clear, modular code and short step-by-step instructions. I’m working on a 2D card game prototype in Unity with card movement between 3 stacks. Now, I want to implement card movement between stacks using drag-and-drop functionality where Card and CardStack classes are MonoBehaviours. Provide possible solutions prioritizing speed of implementation without compromising performance and code clarity.`
