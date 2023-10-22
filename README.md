# Donkey Kong Pursuit (2D)

> Donkey Kong is a 1981 arcade platform video game released by Nintendo. Its gameplay maneuvers Mario across platforms to ascend a construction site and rescue Pauline from the giant gorilla named Donkey Kong, all while avoiding or jumping over obstacles. It is the first game in both the Donkey Kong and Mario franchises.

- **Topics**: Character Physics and Sprites, Game Management
- **Version**: Unity 2022.3 (LTS)

# Game Manager on Preload Scene

When the game starts, we go to **Preload Scene** and Start the Game Manager. It is designed to be called again at every level with the DontDestroyOnLoad() method.

> Scenes: MainMenu, Preload, Level-1, Level-2, Level-3

> enum ManuName: MainMenu, PauseMenu, GameOver

# Sprite Rendering to Animate

Animations were created with an array of a few sprites.

> Using the InvokeRepeating() method On Enable.

# Mario Controls

> The character control code provided here is designed for 2D platformer games and offers a solid foundation for creating a character that can run, jump, and interact with its environment realistically.

**Responsive Movement:** The control system provides responsive horizontal movement, allowing the character to run in both directions with appropriate animations and physics-based handling.

**Jumping and Falling Mechanics:** The code includes gravity and jump control to enable smooth and predictable jumps with variable heights, replicating the familiar Mario-style jumping experience.

**Ladder Climbing:** Implement ladder-climbing functionality, enabling the character to seamlessly ascend and descend ladders in the game world.

**Dynamic Ground Detection and Jump Cooldown:** The character can detect whether it's grounded, ensuring that movements change appropriately when the character is on the ground or in the air. This crucial feature JumpCD helps prevent unrealistic behaviors, such as climbing stairs while airborne.

**Physics Integration:** The code integrates Unity's built-in 2D physics system, facilitating a consistent and reliable character control experience.

# Patrols

Just like the iconic Mario Controls offer a comprehensive set of mechanics for a platformer, our game introduces dynamic and diverse patrols that add excitement and challenge to each level.

> In our game, we have two key elements: Donkey Kong and FireBalls, both of which are controlled and managed by the Game Manager.

**Donkey Kong:**
Equipped with a Barrel Spawner, which Kong uses during specific time periods to throw barrels. With the ability to change sprites and introduce new animations, Donkey Kong can become even more unpredictable and engaging.

**FireBalls:**
Move between specific points and pose a continuous threat to Mario. Their burning effect adds to the game, making it important for players to navigate skillfully to avoid taking damage.

# When we consider the Donkey Kong game, we can achieve diversity with more sprites and some development.
