// SceneElement class
using System;
using System.Drawing;

// A SceneElement is any element involved in the simulation.
// When a SceneElement is created, it is automatically added
// by the constructor to the scene

public abstract class SceneElement
{
	protected int layer; // The layer that this object belongs to.
						// A smaller value indicates a lower layer.
						// Elements with a higher layer value are painted
						// after objects with a lower layer value, and
						// thus, objects with a higher layer value
						// appear to be above objects with a lower
						// layer value.

	protected Scene scene; // The Scene it belongs to

	// Construct a SceneElement with a layer value of 0, and add it
	// to the scene.
	public SceneElement(Scene scene)
	{
		this.scene = scene;
		layer = 0;
		scene.Add(this);
	}

	// Construct a SceneElement with a layer value of new_layer, and
	// add it to the scene.
	public SceneElement(int new_layer, Scene scene)
	{
		this.scene = scene;
		layer = new_layer;
		scene.Add(this);
	}

	// Remove the element from the scene.
	public void RemoveSceneElement()
	{
		scene.Remove(this);
	}

	// Update the SceneElement.  This method can be used in a variety
	// of ways: to move Cars along a Road, to move Pedestrians along
	// a sidewalk, etc.  In this simulation, Conveyors are responsible
	// for moving the Mobile objects that they hold, but a Mobile object
	// could use this method to update its appearance when painted.
	public abstract void Update();

	// Paint this SceneElement to the display.
	public abstract void Draw(Graphics g);

	// Return the layer value for this object.
	public int GetLayer() { return layer; }

	// Set the layer value for this object to new_layer.  If new_layer
	// is different from the previous layer, remove and then insert
	// the SceneElement in scene's SceneList to reposition the
	// SceneElement in the proper layer order.
	public void SetLayer(int new_layer)
	{
		if(layer != new_layer)
		{
			layer = new_layer;
			scene.Remove(this);
			scene.Add(this);
		}
	}

	// The following are a few handy functions related to the geometry
	// of SceneElements.

	// Return the distance between the points (x1, y1) and (x2, y2).
	public double Distance(double x1, double y1, double x2, double y2)
	{
  		double dx = x1 - x2;
  		double dy = y1 - y2;

  		return Math.Sqrt(dx*dx + dy*dy);
	}

	// Return the x coordinate of the (x,y) vector
	// when rotated counter-clockwise by angle (in radians).
	public double RotateX(double x, double y, double angle)
	{
  		return Math.Cos(angle)*x - Math.Sin(angle)*y;
	}

	// Return the y coordinate of the (x,y) vector
	// when rotated counter-clockwise by angle (in radians).
	public double RotateY(double x, double y, double angle)
	{
  		return Math.Sin(angle)*x + Math.Cos(angle)*y;
	}
}
