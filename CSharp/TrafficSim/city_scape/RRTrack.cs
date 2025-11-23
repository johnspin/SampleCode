// RRTrack class
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

// A RRTrack is a Conduit for Trains.  It is distinguished from other
// Conduits only by how it is painted and what Mobile objects are
// actually handed off to it.

public class RRTrack: Conduit
{
	private const double INIT_LTTrkX=0.5;	//4 pixels less than the end
	private const double INIT_LTTrkY=-4;
	private const double INIT_RTTrkX=-0.5;	//4 pixels less than the end
	private const double INIT_RTTrkY=-4;
	
	private const double INIT_LBTrkX=0.5;	//4 pixels less than the end
	private const double INIT_LBTrkY=4;
	private const double INIT_RBTrkX=-0.5;	//4 pixels less than the end
	private const double INIT_RBTrkY=4;

	private const double INIT_RAIL_WIDTH=3;
	private const double INIT_TRACK_WIDTH=16;

	private double lTTrkX, lTTrkY, rTTrkX, rTTrkY;
	private double lBTrkX, lBTrkY, rBTrkX, rBTrkY;

	private double railWidth;
	private double trackWidth;

	// Construct a new RRTrack with incoming Conveyor in,
	// outgoing Conveyor out, and speed limit new_speed_limit.
	public RRTrack(Conveyor cin, Conveyor cout, double new_speed_limit, Scene scene): 
				base(cin, cout, new_speed_limit, scene){
		CalcRail();
		trackWidth = INIT_TRACK_WIDTH;
		railWidth = INIT_RAIL_WIDTH;
	}

	public RRTrack(Conveyor cin, Conveyor cout, double new_speed_limit, int new_layer, Scene scene): 
				base(cin, cout, new_speed_limit, new_layer, scene){
		CalcRail();
		trackWidth = INIT_TRACK_WIDTH;
		railWidth = INIT_RAIL_WIDTH;
	}

	// Construct a new RRTrack with incoming coordinates
	// (new_in_x, new_in_y), outgoing coordinates
	// (new_out_x, new_out_y), and speed limit new_speed_limit.
	public RRTrack(double new_in_x, double new_in_y,
				double new_out_x, double new_out_y,
				double new_speed_limit, Scene scene):
				base(new_in_x, new_in_y, new_out_x, new_out_y, new_speed_limit, scene){
		CalcRail();
		trackWidth = INIT_TRACK_WIDTH;
		railWidth = INIT_RAIL_WIDTH;
	}

	public RRTrack(double new_in_x, double new_in_y,
		double new_out_x, double new_out_y,
		double new_speed_limit, int new_layer, Scene scene):
		base(new_in_x, new_in_y, new_out_x, new_out_y, new_speed_limit, new_layer, scene){
		CalcRail();
		trackWidth = INIT_TRACK_WIDTH;
		railWidth = INIT_RAIL_WIDTH;
	}

	public void CalcRail() {
		
		//System.Console.WriteLine("angle {0} ", angle);
																	       									
		lTTrkX = RotateX(INIT_LTTrkX,  INIT_LTTrkY,  angle);
		lTTrkY = RotateY(INIT_LTTrkX,  INIT_LTTrkY,  angle);
		rTTrkX = RotateX(INIT_RTTrkX,  INIT_RTTrkY,  angle);
		rTTrkY = RotateY(INIT_RTTrkX,  INIT_RTTrkY,  angle);
																	 
		lBTrkX = RotateX(INIT_LBTrkX,  INIT_LBTrkY,  angle);
		lBTrkY = RotateY(INIT_LBTrkX,  INIT_LBTrkY,  angle);
		rBTrkX = RotateX(INIT_RBTrkX,  INIT_RBTrkY,  angle);
		rBTrkY = RotateY(INIT_RBTrkX,  INIT_RBTrkY,  angle);
		
//		lTTrkX = RotateX( RotateX(in_x,  in_y,  -angle)+ INIT_LTTrkX, in_y,  angle);
//		lTTrkY = RotateY( in_x,  RotateY(in_x,  in_y,  -angle)+ INIT_LTTrkY, angle);
//		lBTrkX = RotateX( RotateX(out_x, out_y, -angle)+ INIT_LBTrkX, out_y, angle);
//		lBTrkY = RotateY( out_x, RotateY(out_x, out_y, -angle)+ INIT_LBTrkY, angle);
//		
//		double[] chkArr = new double[] {lTTrkX, lTTrkY, lBTrkX, lBTrkY};
//
//		for (int i=0;i<chkArr.Length;i++)
//			System.Console.WriteLine("chkArr {0}  {1}",i,chkArr[i]);
//		System.Console.WriteLine(" ");
//
//
//		rTTrkX = RotateX( RotateX(in_x,  in_y,  -angle)+ INIT_RTTrkX, in_y,  angle);
//		rTTrkY = RotateY( in_x,  RotateY(in_x,  in_y,  -angle)+ INIT_RTTrkY, angle);
//		rBTrkX = RotateX( RotateX(out_x, out_y, -angle)+ INIT_RBTrkX, out_y, angle);
//		rBTrkY = RotateY( out_x, RotateY(out_x, out_y, -angle)+ INIT_RBTrkY, angle);
//
//		chkArr = new double[] {rTTrkX, rTTrkY, rBTrkX, rBTrkY};
//
//		for (int i=0;i<chkArr.Length;i++)
//			System.Console.WriteLine("chkArr {0}  {1}",i,chkArr[i]);
//		System.Console.WriteLine(" ");

	}

	// Paint the RRTrack.
	public override void Draw(Graphics g)
	{
		// Make sure that the roads are nicely connected
		Pen pen = new Pen(Color.BurlyWood, (float)trackWidth);
		pen.StartCap = LineCap.Flat;
		pen.EndCap = LineCap.Flat;
		pen.DashPattern = new float[] {0.3f, 0.2f};
		g.DrawLine(pen,(float)in_x, (float)in_y, (float)out_x, (float)out_y);

		pen.Color = Color.Black;
		pen.DashStyle = DashStyle.Solid;
		pen.StartCap = LineCap.Square;
		pen.EndCap = LineCap.Square;
		pen.Width = (float)railWidth;
		g.DrawLine(pen,(float)(in_x+lTTrkX), (float)(in_y+lTTrkY), (float)(out_x+rTTrkX), (float)(out_y+rTTrkY));
		g.DrawLine(pen,(float)(in_x+lBTrkX), (float)(in_y+lBTrkY), (float)(out_x+rBTrkX), (float)(out_y+rBTrkY));

//		pen.Color = Color.LightGray;
//		g.DrawLine(pen,(float)in_x, (float)in_y, (float)out_x, (float)out_y);

//		pen.Color =  Color.BurlyWood;
//		pen.Width -=  4;
//		pen.DashPattern = new float[] {0.3f, 0.2f};
//		g.DrawLine(pen,(float)in_x, (float)in_y, (float)out_x, (float)out_y);
	}
}
