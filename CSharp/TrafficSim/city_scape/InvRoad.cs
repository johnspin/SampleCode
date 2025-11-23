// Road class
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

// A Road is a Conduit for Vehicles.  It is distinguished from other
// Conduits only by how it is painted and what Mobile objects are
// actually handed off to it.

public class InvRoad: Conduit
{
	// Construct a new Road with incoming Conveyor in,
	// outgoing Conveyor out, and speed limit new_speed_limit.
	public InvRoad(Conveyor cin, Conveyor cout, double new_speed_limit, Scene scene): base(cin, cout, new_speed_limit, scene){}

	// Construct a new Road with incoming coordinates
	// (new_in_x, new_in_y), outgoing coordinates
	// (new_out_x, new_out_y), and speed limit new_speed_limit.
	public InvRoad(double new_in_x, double new_in_y,
		double new_out_x, double new_out_y,
		double new_speed_limit, Scene scene):
		base(new_in_x, new_in_y, new_out_x, new_out_y, new_speed_limit, scene)
	{}

	// Paint the road.
	public override void Draw(Graphics g)
	{
		// do not paint InvRoad
//		Pen pen = new Pen(Color.LightGray, 16);
//		pen.StartCap = LineCap.Round;
//		pen.EndCap = LineCap.Round;
//		g.DrawLine(pen,(int)in_x, (int)in_y, (int)out_x, (int)out_y);
	}
}