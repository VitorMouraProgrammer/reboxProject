# reboxProject
My second android game with cool mechanics.

All the working game encounters in "classic" code, what I did to make the game work was:
  Make an invisible gameObject;
  Make the box gameObject;
  
  In the update mode the Box does a lerp (go from rotation to rotation) between itself rotation to the invisible gameObject rotation;
  When I need the box to rotate, I only rotate the invisible box, this creates a cool effect of the box lerping to the new rotation.
  The game is inspired by Genius game, you need to rotate back all the rotations the box does in the correct order.
  All the rotations are random and kept in a array, so the box know the following moves.
  
  The game is available in PlayStore as Rebox by PutzStudios.
  
  Thank you~~
