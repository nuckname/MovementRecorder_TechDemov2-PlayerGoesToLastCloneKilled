Ideas:
 - when moving crosshair changes.

Bugs:
 - Grenade Launcher + player collision isnt working. 
 - Ghost bullets kill ghosts
 - Pause Menu doesnt freeze time replay. current commeneted out
 - Pause Menu buttons arent working.
 - gun script firers two bullets on first time using. 
 
 
- Box collision is buggy when collision happens with anything but a tag ghost bullet.
- Big Bug: when Ghost is killed will still fire bullets, GhostID, Time value, assign bullet ID?.
ToDos:
- Grenade Launcher doesnt trigger has moved with the explosion as it doesnt hit th box. So no collision is called.


There are build errors. Color doesnt work. - maybe max red a complex colour. 
needs user feedback. 

weapon boxes need world timer and replay thats all so that we can see them being destoried. 

small camera movements.

gun ammo should reload in AboutToStart. It saves ammo.

    Levels:
    - 3 lanes
    
    Map Theme is a port on the sea side with cargo cranes that are carrying boxes that are horving walls. 
    
    Performance:
    - Replay data from ghost Manager script not from each transform -> removes a lot of updates. 
    - Multi threading + dots. 
    - Boxes despawn at wrong time. whenever im deletein ghost I need to stop.
    Play Test:
    - Cant make spawn location different. Maybe put spawn point further back. 
    - Play always ran back wards when they spawn the first clone. 
    - They also when the same path -> bad map?
    
    -> add box collision, all play testers really liked the boxes. 
    
 A replay feature that can replay each 'run' one frame at a time would be so cool.
 
 tron gun improvements:
  - when fired still shows the trajectory (leaves no trail) 
  - isnt using gun script, so doesnt have reload and stuff like that.
  - cant hit isrecord boxes, its a feature. 