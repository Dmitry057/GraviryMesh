# GraviryMesh

# You can make gravity simulation with various shapes(like torus, monkey, box and other)



https://user-images.githubusercontent.com/47187489/207707974-f3f3177a-17c8-44ad-8cc7-90e7273611ce.mp4

The full video is being prepared...



##Implementation Instructions

To create your own non-spherical planet, you will need to prepare a matrix (in a blender or unity) of points
![image](https://user-images.githubusercontent.com/47187489/207708890-d5f2eef5-2594-4567-ab2d-4b33f653c545.png)

and mesh, witch will consists this points 
![image](https://user-images.githubusercontent.com/47187489/207708972-c60db526-dec6-4598-9bb7-8bf75d1226ec.png)

The quality of simulation directly depends on the number and correctness of the placement of points

After all, you can add the script Planet.cs on all of your points and select #IsStatic
![image](https://user-images.githubusercontent.com/47187489/207709589-703d6bae-cb40-473a-8720-4d9b123c7650.png)

It remains only to add the MKS prefab or create your own object with an inherited class from GravitationSimulation.cs

Now your simulation will work correctly!

You can find all orbits in your mesh if you add the script OrbitParser.cs on scene
