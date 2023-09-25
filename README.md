# Security Clone

This is a recreation of an old clone game I made back when Flash 5 was a thing that people used. It's a very simple game, but it's a usefull tool to demonstrate my abilities as a gamedev and expand my portfolio. 

## Knowledge applied in the making of this game

This are some of the concepts I highlight from this repository:

- **Object Oriented Programming**. From relatively simple concepts like abstraction, separation of interests and encapsulation, to more complex issues like interfaces and basic design patterns. For example, the ConeOfView class that allows multiple entities that have a cone of view object to have different behaviours when spotting the player.
- **Clean code**. I like to think my code is very clean 😅.
- **Unit testing**. Everything has unit tests, and there are some other tests to make sure the project works as intended.
- **Game design concepts**. Introduce the mechanics of the game slowly. No tutorial because it's a very known game concept, you can learn as you play. Difficulty progression. 

### Level design/debugging tools

I've added some tools in the project that allow for simple game debugging and level creation. 

- **Enemy route**. The enemy prefab has a custom inspector that allows to create the route it will follow and visualize it in real time.

### About the game & level design

The OG game had some particularly harsh game design choices. As this is a recreation of my very own first game and interpretation I wasn't aiming to the most clean and up to standards design, but rather something quick and easy to make. Levels and difficulty progression might not be the best. 

If you are interested in a better version of this same concept, I'm planning to make a more polished version.

There are 10 levels and the prision:

1. Intoduces the bases of the game. Movement, walls and enemies. Pretty trivial in itself but important nontheless. 
2. 
3. 
4. 
5. 
6. 
7. 
8. 
9. 
10. 

Lastly **the prision**. If you get captured, you get sent to the prision. I really liked this concept of the original game, but I understand it can get annoying and frustrating. I made it so that if you escape once in a level, you don't get send again to prision. 

I've also tried to explain more with the way the game looks. The original relied a lot in text and intermission screens. I decided to ditch that and apply a bit of "show don't tell".

## Original game

This is a clone from the game "Security 2" by shockArcade. Incredibly, you can still play it [here](https://www.newgrounds.com/portal/view/333169).

## ... why?

This is the very first game I made! There used to be an old tutorial page on how to remake this game on Flash that was very popular back in 2010. That was my very first experience with programming (if you can call 2 lines of Action Script 2 programming). It has a very special place in my heart. 

I've lost the original `.sfw` file so I decided to remake it as part of my portfolio. The reason for that being that although is a very simple game in concept it's not trivial to do in any modern engine.  