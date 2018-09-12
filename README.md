Platform Hopper
==============
by Darren Lim

Thank you for checking out my game!

Introduction
--------------

This is my first solo-made game that I finished.

Platform Hopper is a 3D infinite jumping game. The goal of the game is to get as high as you can by jumping on platforms, avoiding enemies, and getting power ups to jump higher. You start on a floor that, when you reach a certain height, starts to climb upwards. The platforms you jump on start to fall the higher you get. That's it! Good Jump Have Fun!

How to Play
--------------

This game has been uploaded to itch.io (will update link and directions to play)

To move, use the W A S D keys. To look around, use the mouse. To jump, use spacebar. To use multiplayer, you either make a room or join one. However, there can only be up to **six** players in one room. Multiplayer requires at least 2 players to start.

You can jump through platforms from underneath it! This is vital in being able to jump high.

Design Decisions
--------------

Platform Hopper is a very simple game that uses one core mechanic: jumping. I wanted to make a game that my friends and I could have fun with.

Firstly, jumping through platforms from under it is a crucial part of the game. This allows players to jump on to platforms that are right above without maneuvering, making it easier to traverse the game.

Secondly, I didn't want the game to be just jumping. There are difficulties added to make the game harder as the player jumps higher. Inspiration for some mechanics were taken from Doodle Jump (mobile game). As the player progresses higher, the floor climbs faster and platforms have a higher chance of falling. Power ups are also somewhat difficult to attain because they are usually in the empty spaces between platforms.

Thirdly, I wanted to use Object Pooling to pool each object in game (platforms, enemies, and power ups) and reuse each initialized object by disabling and reenabling them. However, when I was thinking of optimazations, I realized I could have just repositioned each initialized object instead of pooling them and going through the list to reenable them.

Lastly, multiplayer has also been implemented because I wanted this game, in the end, to be a game where people could play with friends. However, there still might be bugs. I am still trying to grasp implementation of UNET using Unity's HLAPI in games.

End Notes
--------------

This game was made in Unity and coded in C# using Microsoft Visual Studio. Github was used as the main source control. Most of the objects used are simple polygons (cubes, capsule, spheres). Unity's UNET was used for multiplayer, and only the multiplayer uses a burger model for the character. As for sounds/music, most of them are royalty-free and from websites that post them for free use. Only the main menu scene is a clip I created using Bosca Ceoil. Other assets are taken from the Unity Asset Store.
