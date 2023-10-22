# Donkey Kong Pursuit (2D)

> Donkey Kong is a 1981 arcade platform video game released by Nintendo. Its gameplay maneuvers Mario across platforms to ascend a construction site and rescue Pauline from the giant gorilla named Donkey Kong, all while avoiding or jumping over obstacles. It is the first game in both the Donkey Kong and Mario franchises.

- **Topics**: Character Physics and Sprites, Game Management
- **Version**: Unity 2022.3 (LTS)


![Mario Run](https://github.com/osrbck/DonkeyKong-Pursuit/tree/main/Assets/Sprites/Mario_Run3.png?raw=true)


# Mario Controls

>The character control code provided here is designed for 2D platformer games and offers a solid foundation for creating a character that can run, jump, and interact with its environment realistically.

**Responsive Movement:** The control system provides responsive horizontal movement, allowing the character to run in both directions with appropriate animations and physics-based handling.
**Jumping and Falling Mechanics:** The code includes gravity and jump control to enable smooth and predictable jumps with variable heights, replicating the familiar Mario-style jumping experience.
**Ladder Climbing:** Implement ladder-climbing functionality, enabling the character to seamlessly ascend and descend ladders in the game world.
**Dynamic Ground Detection and Jump Cooldown:** The character can detect whether it's grounded, ensuring that movements change appropriately when the character is on the ground or in the air. This crucial feature JumpCD helps prevent unrealistic behaviors, such as climbing stairs while airborne.
**Physics Integration:** The code integrates Unity's built-in 2D physics system, facilitating a consistent and reliable character control experience.

# Sprite Rendering to Animate, Not Animator

Animations were created with an array of a few sprites using the InvokeRepeating() method On Enable.


# Game Manager on Preload Scene

When the game starts, we go to **Preload Scene** and Start the Game Manager. It is designed to be called again at every level with the DontDestroyOnLoad() method.
> Scenes: MainMenu, Preload, Level-1, Level-2, Level-3 

> enum ManuName: MainMenu, PauseMenu, GameOver






