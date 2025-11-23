// SceneLine class
using System;
using System.Drawing;

// A SceneLine is simply a line which appears in the scene.  It can
// be used to create a more complex image of a SceneElement.

public class SceneLine: SceneElement
{
	private double x1, y1, x2, y2; // coordinates of line's start and end
	private Color color; // color of line
	private double width; // width of line

	// Construct a new SceneLine object with one of the end points
	// having coordinates (new_x1, new_y1), the other having
	// coordinates (new_x2, new_y2), color new_color, width
	// new_width, and layer value new_layer.
	public SceneLine(double new_x1, double new_y1,
					double new_x2, double new_y2,
					Color new_color, double new_width,
					int new_layer, Scene scene): base(new_layer,scene)
	{
		x1 = new_x1;
		y1 = new_y1;
		x2 = new_x2;
		y2 = new_y2;
		color = new_color;
		width = new_width;
	}

	// Do nothing.
	public override void Update(){}

	// Paint the line.
	public override void Draw(Graphics g)
	{
		Pen pen = new Pen(color, (float)width);
		g.DrawLine(pen, (int)x1, (int)y1, (int)x2, (int)y2);
	}
}
