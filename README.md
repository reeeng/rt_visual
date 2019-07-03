# rt_ue4
Realtime viz in UE4 (talking to a webapp)

## What? Why?
PoC for part of a design project.

Idea is:

1. Web application where user drafts floor plans, dimensions / materials
2. Data conveyed to UE4 via backend
3. UE4 generates static meshes based on the dimensions. The meshes are generated in realtime (polling), so an update on the Web UI will be reflected within the app.


Extra:

- Use ARCore to display generated models in physical space. Possibly set to physical location based on markers.

- Ability to rotate, view model using hand gestures. 


Web application is: https://github.com/boarnoah/rt_app
