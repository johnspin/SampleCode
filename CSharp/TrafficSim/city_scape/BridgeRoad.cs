// BridgeRoad class
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

// A BridgeRoad is a Conduit for Vehicles.  It is distinguished from other
// Conduits only by how it is painted and what Mobile objects are
// actually handed off to it.

public class BridgeRoad: Conduit
{
	private bool dontPaintRoad = false;	//paint or don't paint road when boat is under

	// Construct a new BridgeRoad with incoming Conveyor in,
	// outgoing Conveyor out, and speed limit new_speed_limit.
	public BridgeRoad(Conveyor cin, Conveyor cout, double new_speed_limit, Scene scene): base(cin, cout, new_speed_limit, scene){}

	// Construct a new BridgeRoad with incoming coordinates
	// (new_in_x, new_in_y), outgoing coordinates
	// (new_out_x, new_out_y), and speed limit new_speed_limit.
	public BridgeRoad(double new_in_x, double new_in_y,
		double new_out_x, double new_out_y,
		double new_speed_limit, Scene scene):
		base(new_in_x, new_in_y, new_out_x, new_out_y, new_speed_limit, scene)
	{}

	public void setPaint (bool paintIt)	{
		dontPaintRoad = paintIt;
	}

	// Paint the BridgeRoad.
	public override void Draw(Graphics g)
	{
		// do not paint BridgeBridgeRoad when boat is under bridge
		if (!dontPaintRoad)	{
			Pen pen = new Pen(Color.LightGray, 16);
			pen.StartCap = LineCap.Round;
			pen.EndCap = LineCap.Round;
			g.DrawLine(pen,(int)in_x, (int)in_y, (int)out_x, (int)out_y);
		}
	}
}