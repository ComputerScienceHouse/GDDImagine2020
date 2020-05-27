# Pacman but with extra steps
----- Shooting -----
Alliance players are given the ability to shoot bullets.

Rules:
1) An alliance player cannot shoot any bullets if their
   score is 0 (zero).
2) For every bullet shot, one point is deducted from the
   player who shoots the bullet and their team score.

----- Death -----
Rules:
1) When a player is killed, they are respawned in their
   home base, unable to move for 3 (three) seconds.
2) If a player died with at one point, they will keep
   it.

----- Points -----
General Rules:
1) Picking up dots gives you and your team
   1 (one) point.
2) Dying results in the smaller half of your
   points being stored in a Kill Confirmed tag
   which is placed in a random spot on the map.
3) The player/alliance that picks up the 
   Kill Confirmed tag receives its point value.

----- Boundaries -----
Rules:
1) Ally boundary = 'a' in room.txt
2) Enemy boundary = 'e' in room.txt
3) Members of the opposing team cannot
   enter their opponent's boundary.
4) Abilities, such as shooting bullets,
   cannot enter the opponent team's boundary.
   However, if the opponent stands too close
   to the edge of their boundary, they may end
   up getting shot (tldr don't get cocky)

----- Teleporters -----
When a teleporter is added to the map, simply use the
teleporters.txt file found inside of the map's directory
and add information in the following format:

pairID orientation x1 y1 x2 y2

pairID = Teleporter pair identification: Unifies two teleporters.
orientation = Determines the layout of teleporters.
x1 = x coordinate of first teleporter
y1 = y coordinate of first teleporter
x2 = x coordinate of second teleporter
y2 = y coordinate of second teleporter

Rules:
1) Teleporters must be defined in a pair.
2) If orientation == "H", coordinate pair 1 will be a
   left wall teleporter and coordinate pair 2 will be a
   right wall teleporter.
3) If orientation == "V", coordinate pair 1 will be a
   top wall teleporter and coordinate pair 2 will be a
   bottom wall teleporter.
4) If orientation == "P", coordinate pairs 1 and 2 will
   both be platform teleporters. Do not use platform
   teleporters as walls.
5) Wall teleporters are intended to be on the outtermost
   edges of the map, while platform teleporters can be
   used anywhere except the outtermost edges of the map
6) If 6 distinct elements are not defined in a line
   within the teleporters.txt file, then the teleporter
   pair WILL NOT BE GENERATED, leaving a hole at those
   coordinates.
7) Teleporter pair coordinates must be within the bounds
   of the map, otherwise an IndexOutOfRangeException
   will be thrown.
8) Inside the room.txt file, a teleporter is identified by
   't' (lowercase 'T'). If it is not included, the teleporter
   pair WILL NOT BE GENERATED, leaving a hole set those
   coordinates.