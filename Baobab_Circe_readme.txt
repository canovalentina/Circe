Assignment: Final
Team name: Baobab
Game name: Circe
Usernames: azuyeu3, nkhatri30, varcay3
Full names: Arseni Zuyeu, Nischal Khatri, Valentina Cano Arcay
Emails: azuyeu3@gatech.edu, nkhatri30@gatech.edu, valentinacano@gatech.edu
Canvas account names: Arseni Zuyeu, Nischal Khatri, Valentina Cano

I) Scenes:
  - Start scene: Main Menu
  - Level 0: Helios Palace Tutorial
  - Level 1: Island Level 1 Extended
  - Final level: Athena

II) How to play and what parts of the level to observe technology requirements 

Basic Gameplay
--------------
  - Resolution: 1366x768
  - Character: Circe
  - Controls: 
    - W,A,S,D: Circe Movement
    - Q, E: Camera Movement
    - Space: Jump
    - G: God mode
    - R: Collect Plants
    - C: Craft Potions 
    - esc: Pause menu
    - 1: Increase Health (red potion)
    - 2: Enable Stoneskin (grey potion)
    - 3: Spill Enemy Trap (black potion)
    - 4: Spill to Open Door (yellow potion)
    - Left Ctrl: Throw Explosive Attack (orange potion)
    - Left Shift: Throw Transformation (purple potion)


Levels
-------
  - Main Menu: Start the game and get to know about Circe's backstory
  - Tutorial: Follow the tutorial level for a detailed **step-to-step interactive explanation** on the different gameplay mechanics (potion crafting and powers)! 
  - Island: On the island, continue using the skills you learned from the tutorial (such as how to collect plants and craft potions), with these additional elements:
    - More AI enemies to defeat
    - Interactive Elements
      - Hop on the cloud and land on the building for some collectables
      - Spill potion on the cauldron to open the gate and find the key to the final level
      - NPCs with story-line dialogue
      - Other elements on the island: Passive animals walking around, statues, breakable pottery
  - Athena: Go back to the palace and defeat the final enemy

God Mode
--------
  - Press the "G" key to get unlimited potions and to not be impacted by attacks. This can make it easier to test out the different powers that Circe has

Potion Crafting
---------------
  - Collect single plants or multiple plants from bushes to have all the herbs necessary to create your potions
  - Press the "C" key to toggle the crafting potions panel, where you Circe creates different types of potions, using the Craft buttons, based on the plants she has collected
  - The potion types are:
    - Red: Health (drink)
    - Orange: Explosive (throw)
    - Black: Trap (spill)
    - Gray: Stoneskin (drink)
    - Purple: Transformation (enemies to animals) (throw). Note: Only transforms if health is at 1, if not, it decreases health like the orange potion
    - Yellow: Quest (spill to open doors)

Additional Notes:
-----------------
- It is intentional that Brute enemy's sword sometimes doesn't always injure Circe
- The background music manager is intentionally a single instance that persists across scenes and is set in the first scene (Main Menu). If opening scenes within Unity instead of the build, without starting at Main Menu, then the music will not play.


III) Known Problem Areas:
- The cloud can bump against Athena and send her underneath the island
- The pigs sometimes pass through the fences
- In the Island scene, stairs in the environment houses are not available to walk up/down on
- Brute sometimes disappears when using purple potion at a very close range 

IV) Manifest of which files authored by each teammate:

Arseni Zuyeu (azuyeu3):

Collectables:
-------------
- Configurable animated single collectible ingredient (with particle system)
- Multiple collectible (one for harvesting multiple ingredients) with particle system (if not harvested yet), configurable ingredients yielded and reharvest timer
 
Potions and crafting mechanics:
-------------------------------
- Ingredients and potions types
- Sprite configs for 2d/3d ingredients
- Sprite configs for 2d potions
- Recipes
- Crafting logic (what potions can be crafted with current state of ingredients, updating inventory after craft)
- Inventory state, which can be carried over across scenes
- 2d and 3d prefabs for all potions, except purple
- Crafting UI modal
- Potions HUD (old vertical and new horizontal)
- Highlighting required ingredients and showing short tooltip when mouse over potion in crafting window
- Resetting inventory 

Enemy AI:
---------
- Nav meshes and "naive" following of player if within certain range (was used for demo pitch, and then reused it later scripts and fsm)
- FSM for Brute enemy
- Patrol state - goes from waypoint to waypoint, stops at waypoint for some time to look around
- Chase state - triggers when player is in certain radius + enemy is able to see them (i.e. if forward vector is within certain range + no obstacles between). If player runs away - gets back to patrolling
- Attack state - triggers in close proximity to player
- Stuck state - when collides with black potion trap puddle
- Dead state - when gets hit by player
- Combat - refined auto aim, so it doesn't require to preassign particular enemy per stage, supports multiple enemies, targets closest alive one

Potion actions:
---------------
- Logic of refetching potions when thrown if inventory not empty, switching potions for drinking and spilling upon clicking corresponding buttons
- Explosion for collision with orange potion, also unbound it from particular targets so it can hit any target
- Spilling black potion - spilling with particle system, animated puddle which grows when spill happens, makes enemy stuck on collision
- Drinking red potion - increasing health
- Drinking grey potion - switching to a stone texture for a player + 5s immortability
- Spilling yellow potion - spilling liquid with particle system

Tutorial level:
---------------
- Assembling the whole level logic in tutorial controller
- Tutorial hints - showing tips to user while pausing the game
- Animated gates. show state objective when approached. open with animation once objective is fulfilled
- Hint colliders - show one-time tutorial hint in key areas
- Falling bricks from the ceiling, which decreases HP and triggers hint
- Lava that instantly kills on collision
- Animated fence for enemy combat stage
- Potion puzzle to open gates
- Exit portal (load next level)
- Multiple small scripts to check objectives completion to open the next one

Final level:
----------------
- Assembled the scene for final level

Misc:
-----
- Navigation arrow - points to the current game objective (can be any object on the scene) from the ordered list, switches to next one either if approached or if next one is closer than previous one
- Breakable pottery, with ability to hide ingredient or quest item inside
- Improved forward jumping (using applying force instead of toggling gravity, for more natural jump trajectory and feel)
- God mode (immortability and infinite ammo)
- Abstracted yellow potion puzzle, to make other types of doors work with it as long as inherited from AbstractDoor.
- Game logger(now catching all game events and shows in top left corner)
- Playtesting logger
- Setting game resolution script
- Fixed AI (Athena) rotating to follow player
- Finding assets for things above (potions, ingredients, pottery, ui images, etc.)
- Set up initial repo with imported island as a main scene

--------------------------------------
--------------------------------------

Nischal Khatri (nkhatri30):

Circe (Main Character)
----------------------
- Imported a "Peasant Girl" character from Mixamo to align with the game’s story, representing a Medieval Greek Witch.

Animators and Animations used in Circe:
---------------------------------------
1. **Base Layer**
  - Modified the Base Layer animator from SomeDude_RootMotion from individual milestones.
  - Applied basic idle and walking movements using Blend Trees.

2. **Throwing Layer**
  - Added a Throwing Layer state to Circe.
  - Integrated a Throw animation applied to Circe's avatar.
  - Animation is triggered via the "Throw" parameter transition.

3. **Picking, Drinking, Spilling & Jumping Layer**
  - **Picking Animation**
    - Implemented a Picking state using root motion.
    - Combined it with a coroutine rotation for accurate animation execution.
    - Triggered via the "Pick" parameter transition.

  - **Drinking Animation**
    - Added a Drinking state to support health and transformation potions.
    - Triggered via the "Drink" parameter transition.

  - **Spilling Animation**
    - Integrated a Spilling state using a Mixamo watering animation.
    - Used for spilling trap potions.
    - Triggered via the "Spill" parameter transition.

  - **Spilling on Target Animation**
    - Implemented a Spilling on Target state.
    - Uses a Button Pushing animation from Mixamo.
    - Designed for spilling potion onto an in-game target (e.g., a vase).
    - Combined it with a coroutine rotation for accurate animation execution.
    - Triggered via the "SpillOnTarget" parameter transition.

  - **Jumping Animation**
    - Implemented a Jumping state.
    - Uses a Idle Jumping animation from Mixamo.
    - Triggered via the "StandingJump" parameter transition.

  - **Running Jump Animation**
    - Implemented a Running Jump state.
    - Uses a Running Jump animation from Mixamo.
    - Triggered via the "RunningJump" parameter transition.

4. **Hurt & Dead Layer**
  - **Dead Animation**
    - Implemented a Falling Back Death state using root motion.
    - Triggered via the "isDead" parameter transition.

  - **Hurt Animation**
    - Implemented a Pain Gesture state using root motion.
    - Triggered via the "isHit" parameter transition.

Scripts Contributed for Circe:
------------------------------
1. **Root Motion Control Script** - Handles Circe's movement using root motion, ensuring smooth animation transitions and precise positioning for interactions. Dervied from SomeDude_RooMotion of indvidual milestones.
2. **Potions Actions Controller** - Manages potion actions like throwing, drinking, spilling, jumping triggering effects on impact.
3. **Character Input Controller** - Handles player inputs for movement.
4. **Plant Collector** - Detects nearby plants and allows Circe to pick them up with an animation when the player presses 'R'.
5. **Brute's Sword Hit Detection & Health System** - Contributed to the first draft of the script to detect Brute's sword hit on Circe, which was later modified into a full health bar system using the **CharacterHealthBarCollisionHandler** script.


Camera and Camera Scripts for Circe:
------------------------------------
- Added a third person view camera as a child object of Circe, which follows Circe throughout the game play
- **Scene Adaptive Third Person Camera** - A third-person camera that adjusts its position and rotation dynamically based on whether Circe is in an indoor or outdoor scene. It detects the scene type and sets appropriate camera offsets.


Collectable Ingredients for Circe:
----------------------------------
- **Collectable Ingredients System** - Imported and added the first draft of collectable ingredients, including models and interaction logic. Initially implemented with a basic collection system using the **PlantCollector** script, which was later modified to fit gameplay mechanics.

Brute (Enemy AI)
----------------
- Imported a "Brute Warrior" character from Mixamo to align with the game’s story, representing an enemy character in a medieval setting.

Animators and Animations used in Brute:
---------------------------------------
1. **Base Layer**
  - **Crouch Idle Animation**
    - Default state where Brute is waiting in a crouched position
  - **Run Forward Animation**
    - Implemented a Standing Run Forward animation.
    - Triggered via the "isWalking" parameter transition.
  - **Attacking Animation**
    - Implemented a Standing Attack animation.
    - Triggered via the "isAttack" parameter transition.

2. **Confused & Dead Layer**
  - **Confused Animation**
    - Implemented a Confused state using drunk animation.
    - Triggered via the "isConfused" parameter transition.

  - **Dead Animation**
    - Implemented a Dead state using enemy dead animation.
    - Triggered via the "isDead" parameter transition.

Scripts Contributed for Brute:
------------------------------
1. **Brute Movement** - Implemented the initial movement logic for Brute, allowing him to follow Circe within a certain range, detect obstacles, and attack when in proximity.
2. **Brute Collision Handler** - Contributed to the initial implementation of detecting Brute’s sword hits, which was later integrated into the AI scripts.
3. **Brute Sword Script** - Handles Brute’s sword collisions with Circe, triggering hit reactions, health reduction, and sword impact sound effects.

Brute Navigation and AI:
------------------------
- **NavMesh Agent Implementation** - Implemented a NavMesh Agent on Brute, enabling him to navigate the terrain dynamically.
- **Brute Following Mechanism** - Configured Brute to follow Circe when she enters a specific range, stopping when out of range or when attacking.
- The above was used later to create AI state machine for Brute.


Fenced Gated Area with Door Mechanism:
--------------------------------------
- **Constructed a Fenced Area** - Assembled a fenced perimeter with a functional gate to control Circe's movement in the game world.
- **Spill Stand Spot & Vase Interaction** - Designed a mechanism where Circe must spill a potion on a vase at the spill stand spot to open the door.
- **SpillTrigger Script** - Detects when Circe enters or exits the spill stand spot and ensures the action is only triggered when she is in position.
- **FenceDoorController Script** - Controls the door movement using a hinge joint, smoothly opening the door when the potion spill is detected.
- **Physics Handling** - Prevented the door from opening due to unintended collisions by freezing physics constraints until the spill event occurs.

Island Level 1 Extended Scene:
--------------------------------------
- Configured the entirety of the Island Level 1 Extend Scene for the final deliverable
- Utilized all resources and mechanisms to create a gameplay story
- Updated Statues that provide hint to match medieval greek language/theme
- Scattered breakable cauldron with ingredients throughout the island to make the environment more interactive
- Strategically placed Brutes in various spots to introduce difficultly/challenge in the game
- Bears are wolves are placed throughout the island to make the island more interactive
- Created and added piledup cauldron with fire and ingridients throughtout the island
- Introduced new vertical cloud to allow players option to cross the river
- Introduced a new Old Man character that screams at Circe to get out of the island
- Clear hints on various statues to make the gameplay easier based on playtesting feedbacks
- Placed a cauldron with key in the fenced area that unlocks door to next/final level
- Created exit portal which is activated once Circe finds the hidden key

			
--------------------------------------
--------------------------------------
			
Valentina Cano (varcay3):

Audio:
------
  - **AudioEventManager**: Following the AudioEventManager pattern from the milestones, implemented the audio events for the game.
  - **BackgroundMusicManager**: Following the same pattern, created a class for the background music which instantiates a BackgroundMusic prefab object to keep track of the music. It includes Play, Pause, Resume, Volume and Stop events
  - **Sound Effects**: Created various sound effects and called them where relevant:
    - Animals: BearGrowl, PigOink, WolfHowl
    - Plants: CollectsSinglePlant, CollectsPlant (multi-collectable)
    - UI: PressInventoryKey, PressPauseKey
    - Potions: Explosion, PlayerDrinksPotion
    - Attack: Sword
    - Game Over, Game won

Animals:
--------
  - Imported animals package to follow the theme of the story in which Circe is a goddess/witch who befriends animals and also transforms humans to animals
  - AnimalStateMachine:
    - Created an AnimalStateMachine to handle the animations and states of the different animals implemented. The type of animal is controlled by an Enum AnimalType 
    - Detects whether Circe is close or not. This is currently used to decide whether to play the animal noises
  - Animator Controllers:
    - Implemented BearAnimatorController, PigAnimatorController and WolfAnimatorController
    - Base layer includes:
      - **Idle**
        - Default state where animal is idle
      - **Walk Animation**
        - Triggered via the "Walk" and "Speed" parameter transition.
        - Animals move from Idle to Walk state at random intervals 
      - **Howl Animation**
        - Only available for the wolf
        - Can be reached from Walk or Idle
        - Triggered via the "Howl" parameter transition
      - Note: Animator Controllers include Attack, but we decided all animals are passive (all animals are set to CanAttack: false)
  - Added animals throughout the Island as interactive elements

Helios and Odysseus NPCs:
-------------------------
- Imported the Uriel A Plotexia character from Mixamo 
- Animator Controllers:
    - Implemented HeliosAnimatorController and OdysseusAnimatorController
    - Base layer includes:
      - **Idle**
        - Default state where animal is idle
      - **Talking/Yelling Animation**
        - Triggered via the "Talk" parameter transition.
        - Helios uses the Yelling animation; Odysseus uses the Talking animation
      - **Disappointed Animation**
        - Only available for Helios
        - Triggered via the "Disappointed" parameter transition
  - Added Helios at the end of the Helios Palace scene with a message banishing Circe to the Island
  - Added Odysseus to the end of the Island Level with a message explaining that Athena is angry with Circe for messing with him and his men, so she must fight her
  - Implemented the dialogue logic with the NPCInteraction and DialogueController scripts

Cloud:
------
  - Object: Created a ramp-type object using 3 cube objects, set to invisible. Then, imported a package with different types of clouds and covered the ramp with clouds to give the illusion that Circe is hopping on a cloud
  - Animation: Created the PlatformArc animation which moves the object as an arc and back continuously
  - AnimatorController: Implemented the CloudPlatformController to animate the cloud 

Transformation Potion (Purple):
-------------------------------
  - In script TransformationPotionCollisionHandler, implemented potion functionality that converts a Brute into an animal prefab (currently set to a Pig)
  - Made relevant edits to PotionsActionsController to handle new functionality
  - Added purple potion to potions counter and crafting panel

UI
---
  - **UIManager**: Implemented UIManager class to controls the different panels
  - **Pause, Game Over, Game Won panels**: Created the panels with Restart Game, Restart Level, Quit Game, and Go to Main Menu buttons. Pause panel is toggled with the esc key
  - **Controls**: In pause panel, added an additional left-hand panel with a list of controls with corresponding potion colors where applicable

Helios Palace (Scene)
---------------------
- Imported the Palace scene as the introduction/tutorial level for them game
- Tested different positions and rotations for the outdoor and indoor scene and added SceneAdaptiveThirdPersonCamera to Circe
- Added ramps to all the stairs to make them walkable

Health Bars
-----------
  - Created Health Bar for Circe, which keeps track of her health levels with a red bar, and implemented its initial functionality in the CharacterHealthBarCollisionHandler script
  - Created Health Bar Canvas for Athena and Brute, which keeps track of their health levels with a over-head bars, and implemented their functionality so they are impacted by Circe's attacks with the orange and purple potions
      - Logic: The purple potion only transforms the enemy into an animal if their health is at 1. If not, it simply decreases their health like the orange potion. Brutes are transformed into pigs, while Athena is transformed into a wolf.

Greek Female Statue
--------------------
  - Imported a Greek Statue package to serve add interaction
  - Light: Implemented StatueController to make the statues light up when Circe approaches
  - Dialogue: 
    - Added an over-head Canvas to the Statue to display text
    - Implemented and added the DialogueTrigger script which allows:
      - Fade In/Fade Out: as Circe approaches
      - Typewriter effect: Makes the text appear letter by letter
  - Added statues to the scenes as interactive elements. For example, at the end of the Tutorial Scene a statue tells Circe that she is banished to the island.

Main Menu Scene
----------------
  - Added a Main Menu scene with options to start or quit the game
  - Created an animated third-person island view, featuring Circe and a pig (upper-right), a wolf (upper-left), and a bear (bottom-left) in the distance
  - Included a pottery image based on a historical depiction of the Circe myth
  - Added a "Who is Circe?" button that opens a panel with Circe’s backstory

--------------------------------------
--------------------------------------
Script and Author(s):
----------------------
Assets/Scripts/Animals/AnimalMovement.cs, Valentina Cano
Assets/Scripts/Animals/AnimalStateMachine.cs, Valentina Cano
Assets/Scripts/Animals/States/AnimalAttackState.cs, Valentina Cano
Assets/Scripts/Animals/States/AnimalBaseState.cs, Valentina Cano
Assets/Scripts/Animals/States/AnimalIdleState.cs, Valentina Cano
Assets/Scripts/Animals/States/AnimalWalkState.cs, Valentina Cano
Assets/Scripts/AppEvents/AudioEventManager, Valentina Cano
Assets/Scripts/AppEvents/AudioEvents/BackgroundMusicEvents.cs, Valentina Cano
Assets/Scripts/AppEvents/AudioEvents/BearGrowlEvent.cs, Valentina Cano
Assets/Scripts/AppEvents/AudioEvents/DeathFemaleEvent.cs, Valentina Cano
Assets/Scripts/AppEvents/AudioEvents/DeathMaleEvent.cs, Valentina Cano
Assets/Scripts/AppEvents/AudioEvents/ExplosionEvent.cs, Arseni Zuyeu
Assets/Scripts/AppEvents/AudioEvents/GameOverEvent.cs, Valentina Cano
Assets/Scripts/AppEvents/AudioEvents/GameWonEvent.cs, Valentina Cano
Assets/Scripts/AppEvents/AudioEvents/PainFemaleEvent.cs, Valentina Cano
Assets/Scripts/AppEvents/AudioEvents/PainMaleEvent.cs, Valentina Cano
Assets/Scripts/AppEvents/AudioEvents/PigOinkEvent.cs, Valentina Cano
Assets/Scripts/AppEvents/AudioEvents/PlayerBreaksPotteryEvent.cs, Arseni Zuyeu
Assets/Scripts/AppEvents/AudioEvents/PlayerCollectsPlantEvent.cs, Valentina Cano
Assets/Scripts/AppEvents/AudioEvents/PlayerCollectsSinglePlantEvent.cs, Valentina Cano
Assets/Scripts/AppEvents/AudioEvents/PlayerDrinksPotionEvent.cs, Valentina Cano
Assets/Scripts/AppEvents/AudioEvents/PlayerLandsEvent.cs, Nischal Khatri
Assets/Scripts/AppEvents/AudioEvents/PlayerThrowsPotionEvent.cs, Valentina Cano
Assets/Scripts/AppEvents/AudioEvents/PressInventoryKeyEvent.cs, Valentina Cano
Assets/Scripts/AppEvents/AudioEvents/PressPauseKeyEvent.cs, Valentina Cano
Assets/Scripts/AppEvents/AudioEvents/SwordEvent.cs, Valentina Cano
Assets/Scripts/AppEvents/AudioEvents/WolfHowlEvent.cs, Valentina Cano
Assets/Scripts/AppEvents/BackgroundMusic.cs, Valentina Cano
Assets/Scripts/AppEvents/BackgroundMusicManager.cs, Valentina Cano
Assets/Scripts/AppEvents/CollectablePlant.cs, Arseni Zuyeu
Assets/Scripts/AppEvents/GameplayEvents/GodModeToggledEvent.cs, Arseni Zuyeu
Assets/Scripts/Camera/SceneAdaptiveThirdPersonCamera.cs, Valentina Cano
Assets/Scripts/CharacterControl/CharacterHealthBarCollisionHandler.cs, Valentina Cano, Arseni Zuyeu, Nischal Khatri
Assets/Scripts/CharacterControl/CharacterInputController.cs, Nischal Khatri
Assets/Scripts/CharacterControl/CirceHitReaction.cs, Nischal Khatri
Assets/Scripts/CharacterControl/DebugController.cs, CS6445
Assets/Scripts/CharacterControl/PlantCollector.cs, Nischal Khatri
Assets/Scripts/CharacterControl/PotionsActionsController.cs, Arseni Zuyeu, Nischal Khatri, Valentina Cano
Assets/Scripts/Clouds/CloudAttack.cs, Nischal Khatri
Assets/Scripts/Clouds/MovingPlatform.cs, Nischal Khatri
Assets/Scripts/Collectables/CollectableMulti.cs, Arseni Zuyeu
Assets/Scripts/Collectables/Collectible.cs, Arseni Zuyeu
Assets/Scripts/Collectables/KeyPickup.cs, Nischal Khatri
Assets/Scripts/Dialogue/AutoScrollDialogue.cs, Valentina Cano
Assets/Scripts/Dialogue/DialogueTrigger.cs, Valentina Cano
Assets/Scripts/Dialogue/DialogueController.cs, Valentina Cano
Assets/Scripts/Dialogue/NPCInteraction.cs, Valentina Cano
Assets/Scripts/Enemy/Athena/AthenaAttack.cs, Nischal Khatri
Assets/Scripts/Enemy/Athena/AthenaHealthBarHandler.cs, Valentina Cano
Assets/Scripts/Enemy/Brute/BruteCollision.cs, Arseni Zuyeu
Assets/Scripts/Enemy/Brute/BruteHealthBarHandler.cs, Valentina Cano
Assets/Scripts/Enemy/Brute/BruteMovement.cs, Nischal Khatri
Assets/Scripts/Enemy/Brute/BruteStateMachine.cs, Arseni Zuyeu
Assets/Scripts/Enemy/Brute/BruteSword.cs, Nischal Khatri
Assets/Scripts/Enemy/Brute/States/AttackState.cs, Arseni Zuyeu
Assets/Scripts/Enemy/Brute/States/BruteState.cs, Arseni Zuyeu
Assets/Scripts/Enemy/Brute/States/ChaseState.cs, Arseni Zuyeu
Assets/Scripts/Enemy/Brute/States/DeadState.cs, Arseni Zuyeu
Assets/Scripts/Enemy/Brute/States/PatrolState.cs, Arseni Zuyeu
Assets/Scripts/Enemy/Brute/States/StuckState.cs, Arseni Zuyeu
Assets/Scripts/Enemy/SailorCollisionHandler.cs, Arseni Zuyeu
Assets/Scripts/Enemy/SailorMovement.cs, Arseni Zuyeu
Assets/Scripts/EventSystem/EventManager.cs, Nischal Khatri
Assets/Scripts/Inventory/Config.cs, Arseni Zuyeu
Assets/Scripts/Inventory/IngredientSpriteConfig.cs, Arseni Zuyeu
Assets/Scripts/Inventory/IngredientUI.cs, Arseni Zuyeu
Assets/Scripts/Inventory/Inventory.cs, Arseni Zuyeu
Assets/Scripts/Inventory/PotionColorConfig.cs, Arseni Zuyeu
Assets/Scripts/Inventory/PotionCraftingManager.cs, Arseni Zuyeu
Assets/Scripts/Inventory/PotionSpriteConfig.cs, Arseni Zuyeu
Assets/Scripts/Inventory/PotionUI.cs, Arseni Zuyeu
Assets/Scripts/Inventory/ToggleInventory.cs, Arseni Zuyeu
Assets/Scripts/Levels/Athena/WinGameManager.cs, Valentina Cano
Assets/Scripts/Levels/Tutorial/BricksCollider.cs, Arseni Zuyeu
Assets/Scripts/Levels/Tutorial/ExitPortalController.cs, Arseni Zuyeu
Assets/Scripts/Levels/Tutorial/FallingColumnCollider.cs, Arseni Zuyeu
Assets/Scripts/Levels/Tutorial/GateScript.cs, Arseni Zuyeu
Assets/Scripts/Levels/Tutorial/HintCollider.cs, Arseni Zuyeu
Assets/Scripts/Levels/Tutorial/IngredientCollider.cs, Arseni Zuyeu
Assets/Scripts/Levels/Tutorial/KeyCollector.cs, Arseni Zuyeu
Assets/Scripts/Levels/Tutorial/LavaCollider.cs, Arseni Zuyeu
Assets/Scripts/Levels/Tutorial/PostLavaJumpCollider.cs, Arseni Zuyeu
Assets/Scripts/Levels/Tutorial/PotteryTutorialController.cs, Arseni Zuyeu
Assets/Scripts/Levels/Tutorial/ReleaseBrute.cs, Arseni Zuyeu
Assets/Scripts/Levels/Tutorial/StuckReportCollider.cs, Arseni Zuyeu
Assets/Scripts/Levels/Tutorial/TutorialController.cs, Arseni Zuyeu
Assets/Scripts/OldMan/OldManController.cs, Nischal Khatri
Assets/Scripts/Potions/ExplosivePotionCollisionHandler.cs, Arseni Zuyeu
Assets/Scripts/Potions/PuddleCollisionHandler.cs, Arseni Zuyeu
Assets/Scripts/Potions/TransformationPotionCollisionHandler.cs, Valentina Cano
Assets/Scripts/Props/AbstractDoor.cs, Arseni Zuyeu
Assets/Scripts/Props/ExitPortalController.cs, Arseni Zuyeu
Assets/Scripts/Props/FenceDoorController.cs, Nischal Khatri
Assets/Scripts/Props/PotteryController.cs, Arseni Zuyeu
Assets/Scripts/Props/SpillTrigger.cs, Khatri, Nischal
Assets/Scripts/Props/StatueController.cs, Valentina Cano
Assets/Scripts/Sea/WaterDeathZone.cs, Nischal Khatri 
Assets/Scripts/Sound/OldManScreaming.cs, Nischal Khatri
Assets/Scripts/UI/GameLog.cs, Arseni Zuyeu
Assets/Scripts/UI/LogMessage.cs, Arseni Zuyeu
Assets/Scripts/UI/UIManager.cs, Valentina Cano
Assets/Scripts/UI/UIManagerMainMenu.cs, Valentina Cano
Assets/Scripts/Utility/Constants.cs, Valentina Cano
Assets/Scripts/Utility/GameOverManager.cs, Arseni Zuyeu
Assets/Scripts/Utility/GameQuitter.cs, Nischal Khatri
Assets/Scripts/Utility/GameStarter.cs, Nischal Khatri
Assets/Scripts/Utility/GameRestarter.cs, Valentina Cano
Assets/Scripts/Utility/LevelRestarter.cs, Nischal Khatri
Assets/Scripts/Utility/GodMode.cs, Arseni Zuyeu
Assets/Scripts/Utility/PlaytestingLogger.cs, Arseni Zuyeu
Assets/Scripts/Utility/ResolutionSetter.cs, Arseni Zuyeu
Assets/Scripts/Utility/PlayerNavigation.cs, Arseni Zuyeu

--------------------------------------
--------------------------------------
References, Imported Assets:
----------------------------
- Mixamo Characters (https://www.mixamo.com/#/?page=1&type=Character): Peasant Girl, Brute
- Mixamo Animations: 
- Survivor Island' Scene: https://assetstore.unity.com/packages/tools/terrain/survivor-island-139975 
- 'Palace with Treasures' Scene: https://assetstore.unity.com/packages/3d/environments/dungeons/palace-with-treasures-90225  
- Potions: https://assetstore.unity.com/packages/2d/gui/icons/magic-potion-1-69874 
- Bushes: https://assetstore.unity.com/packages/3d/vegetation/plants/yughues-free-bushes-13168 
- Clouds: https://assetstore.unity.com/packages/3d/3le-low-poly-cloud-pack-65911
- Animals: https://assetstore.unity.com/packages/3d/characters/animals/animal-pack-deluxe-99702
- Fences: https://assetstore.unity.com/packages/3d/environments/realistic-fences-pack-211850
- Fire explosion: https://assetstore.unity.com/packages/vfx/particles/fire-explosions/low-poly-fire-244190
- Breakable pottery: https://assetstore.unity.com/packages/3d/props/furniture/breakable-jars-vases-pots-280906
- Potions: https://assetstore.unity.com/packages/3d/props/potions-115115
- Circe vase image: Dresden 323, Attic Red-Figure Pelike (ca. 460 B.C., Ethiop Painter), Staatliche Kunstsammlungen Dresden. https://www.theoi.com/Gallery/T35.1.html