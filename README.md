# Donkey Kong Pursuit (2D)

> Donkey Kong is a 1981 arcade platform video game released by Nintendo. Its gameplay maneuvers Mario across platforms to ascend a construction site and rescue Pauline from the giant gorilla named Donkey Kong, all while avoiding or jumping over obstacles. It is the first game in both the Donkey Kong and Mario franchises.

- **Topics**: Character Physics and Sprites, Game Management
- **Version**: Unity 2022.3 (LTS)

Donkey Kong Pursuit offers a compelling and engaging gameplay experience that has stood the test of time. With meticulously designed character animations, a robust Game Manager system, and a host of challenging game elements, it captures the essence of classic arcade gaming. Step into the world of Donkey Kong Pursuit and relive the magic of this iconic title, where every jump and every move counts in the pursuit of saving Pauline from the clutches of Donkey Kong.

# Game Manager on Preload Scene

When the game starts, we go to **Preload Scene** and Start the Game Manager. It is designed to be called again at every level with the DontDestroyOnLoad() method.
> Scenes: MainMenu, Preload, Level-1, Level-2, Level-3 
> enum ManuName: MainMenu, PauseMenu, GameOver,


# Sprite Rendering to Animate Mario, Not Animator

Animations were created with an array of a few sprites using the InvokeRepeating() method.
> spriteRenderer.sprite = runSprites[spriteID];

# Patrols

**Kong**: Has BarrelSpawner and Danger collision area
**FlareBall**: Has Danger collusion area and Moving between two PatrolPoints
> The difficulty level of these objects increases with each level (Barrel spawn rate increases, and more...)


