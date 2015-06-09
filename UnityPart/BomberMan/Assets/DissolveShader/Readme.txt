Dissolve Shader

Once imported you will find the Shader under "Dissolve/Dissolve_*"

Dissolve_TexturCoords:

The Shader Dissolve_TexturCoords uses the TextureCoordinates of the given Object. That gives you the Opportunity to set where the object should Dissolve first.

Dissolve_WorldCoords:

The Shader Dissolve_World Coords uses World/Object Coordinates to Dissolve the Object. This method gets rid of the Texture seams, but is not suitable for animated Objects.

Properties:
Amount - Animate this to Dissolve the Object
StartAmount - Sets the Value from witch the dissolve starts
Illuminate - Sets the Value of Illumination for the rims
Tile - Sets the tiling of DissolveSrc and DissolveSrcBump
DissColor - Sets the Color for the rimsColorAnimate - Sets witch RGB-Value should be animated X = Red, Y = Yellow, Z = Green (Ignore W)