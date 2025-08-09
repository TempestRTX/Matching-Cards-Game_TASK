# Matching Cards Game (Unity, C#)

A memory card matching game built in Unity using a **mix of State Machine and Singleton patterns** for clean architecture and maintainability.

---

## 🛠 Project Overview
This project demonstrates a structured approach to building a Unity game using:
- **Singleton Managers** for shared systems
- **State Machine** for handling game flow and transitions
- **Event-driven architecture** to decouple UI and logic
- **Save/Load system** for persisting gameplay progress

---

## 🎮 Core Features
- Multiple grid layouts (e.g., 2×2, 2×3, 5×6)
- Smooth card flipping animations
- Combo-based scoring with match/mismatch rules
- Parallel interaction — no hard blocking while checking matches
- Save/Load game state mid-session
- Auto-delete save data on level completion

---

## 🧩 Architecture & Design Patterns
### **1. State Machine**
- Game flow is controlled by a state machine to manage transitions (e.g., Main Menu → Game Play → Level Complete).
- Ensures predictable state changes and separation of responsibilities.

### **2. Singleton Managers**
All common managers exist as **singleton instances**:
- `GameManager` → Handles application-level transitions, layout setup, score updates
- `ScreenManager` → Manages active screens and UI transitions
- `AudioManager` → Centralized sound effects/music
- `EventSystemManager` → Broadcasts game events without direct references

### **3. Event System Manager**
- Cards and UI elements never reference each other directly.
- Events like `OnCardMatched`, `OnCardFlipped`, `OnGameOver` are broadcast from the `EventSystemManager`.
- Reduces coupling and improves maintainability.

---

## 💾 Save/Load System
- On **Save**: Stores matched card IDs, current score, and combo in `PlayerPrefs` (JSON serialized).
- On **Load**: Data is retrieved and applied to the current board, restoring flipped/matched states.
- On **Level Completion**: Saved data is cleared to start fresh.

---

## 📂 Project Structure
- **Assets/Scripts**
  - `Managers/` — Singleton-based managers (GameManager, ScreenManager, AudioManager, EventSystemManager, SaveSystem)
  - `Gameplay/` — Card controller scripts, match detection, score logic
  - `UI/` — Menus, HUD, animations
- **Assets/Prefabs** — Reusable card, layout, and UI prefabs
- **Assets/Resources** — Sprites, audio clips

---

## 🚀 How to Run
1. Open in **Unity 2021 LTS or later**
2. Open the `MainScene`
3. Press Play in the Editor

---

## 🔮 Future Enhancements
- Visual effects for matches/mismatches
- Configurable difficulty & timed mode
- Online multiplayer with Unity Netcode

---

## 📧 Contact
Developed by **Tempest (TempestRTX)**  
GitHub: [TempestRTX](https://github.com/TempestRTX)
