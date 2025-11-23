// Bus class

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

public class Firetruck: Mobile
{
	// constants to define the bus geometry
	// Front and rear positions of the bus
	private const double INIT_FRONT_X  =  12;
	private const double INIT_FRONT_Y  =  0;
	private const double INIT_REAR_X   = -12;
	private const double INIT_REAR_Y   =  0;

	// Length taken by a bus on the road (more than its physical length)
	private const double INIT_LENGTH = 40;

	private const double INIT_WIDTH = 10;
	private const double INIT_MAX_SPEED = 9;

	// Tires
	//front
	private const double INIT_FLTX =  10;
	private const double INIT_FLTY =  6;
	private const double INIT_FRTX =  10;
	private const double INIT_FRTY = -6;

	//front rear
	private const double INIT_RLTX = -5;
	private const double INIT_RLTY =  6;
	private const double INIT_RRTX = -5;
	private const double INIT_RRTY = -6;

	//rear rear
	private const double INIT_RLT2X = -10;
	private const double INIT_RLT2Y =  6;
	private const double INIT_RRT2X = -10;
	private const double INIT_RRT2Y = -6;

//	//ladder
//	private const double INIT_FLLdrX=5;
//	private const double INIT_FLLdrY=2;
//	private const double INIT_RLLdrX=-15;
//	private const double INIT_RLLdrY=2;
//
//	private const double INIT_FRLdrX=5;
//	private const double INIT_FRLdrY=-2.5;
//	private const double INIT_RRLdrX=-15;
//	private const double INIT_RRLdrY=-2.5;

	//ladder
	//black part
	private const double INIT_FLLdrX=3;
	private const double INIT_FLLdrY=0;
	private const double INIT_RLLdrX=-15;
	private const double INIT_RLLdrY=0;

	//red part
	private const double INIT_FRLdrX=4;
	private const double INIT_FRLdrY=0;
	private const double INIT_RRLdrX=-15;
	private const double INIT_RRLdrY=0;

	//private const double INIT_LdrW=2;
	//private const double INIT_LdrRung=3;
	//private const double INIT_LdrPole=1;


	private const double INIT_TIRE_WIDTH = 3;

	private double front_x;	// x coordinate of front of bus
	private double front_y;	// y coordinate of front of bus
	private double rear_x;	// x coordinate of rear of bus
	private double rear_y;	// y coordinate of rear of bus

	private double fltx, flty, frtx, frty;  // front tire coordinates
	private double rltx, rlty, rrtx, rrty;  // rear tire coordinates
	private double rlt2x, rlt2y, rrt2x, rrt2y;  // rear tire coordinates

	//ladder
	private double FLLdrX, FLLdrY, RLLdrX, RLLdrY;	//,  ldrRung, 
	private double FRLdrX, FRLdrY, RRLdrX, RRLdrY;	//,  ldrRung, 
	//private double ldrW, ldrPole;

	private double width;		// width of bus
	private double tire_width;	// width of tires.

	public Firetruck(double x, double y, Scene scene): base(x,y,INIT_LENGTH, INIT_MAX_SPEED, scene)
	{
		front_x = INIT_FRONT_X;
		front_y = INIT_FRONT_Y;
		rear_x  = INIT_REAR_X;
		rear_y  = INIT_REAR_Y;

		fltx = INIT_FLTX;
		flty = INIT_FLTY;
		frtx = INIT_FRTX;
		frty = INIT_FRTY;

		rltx = INIT_RLTX;
		rlty = INIT_RLTY;
		rrtx = INIT_RRTX;
		rrty = INIT_RRTY;

		rlt2x = INIT_RLT2X;
		rlt2y = INIT_RLT2Y;
		rrt2x = INIT_RRT2X;
		rrt2y = INIT_RRT2Y;

		FLLdrX = INIT_FLLdrX;
		FLLdrY = INIT_FLLdrY;
		RLLdrX = INIT_RLLdrX;
		RLLdrY = INIT_RLLdrY;

		FRLdrX = INIT_FRLdrX;
		FRLdrY = INIT_FRLdrY;
		RRLdrX = INIT_RRLdrX;
		RRLdrY = INIT_RRLdrY;
		//ldrRung = INIT_LdrRung;

		//ldrPole = INIT_LdrPole;
		//ldrW = INIT_LdrW;
		width = INIT_WIDTH;
		tire_width = INIT_TIRE_WIDTH;

	}


	// Rotate the coordinate of the bus such that the bus points in
	// in the direction of angle.  The units of angle is Radians.
	public override void SetDirection(double angle)
	{
		base.SetDirection(angle);

		front_x  = RotateX(INIT_FRONT_X,  INIT_FRONT_Y,  angle);
		front_y  = RotateY(INIT_FRONT_X,  INIT_FRONT_Y,  angle);
		rear_x   = RotateX(INIT_REAR_X,   INIT_REAR_Y,   angle);
		rear_y   = RotateY(INIT_REAR_X,   INIT_REAR_Y,   angle);

		fltx = RotateX(INIT_FLTX, INIT_FLTY, angle);
		flty = RotateY(INIT_FLTX, INIT_FLTY, angle);
		frtx = RotateX(INIT_FRTX, INIT_FRTY, angle);
		frty = RotateY(INIT_FRTX, INIT_FRTY, angle);

		rltx = RotateX(INIT_RLTX, INIT_RLTY, angle);
		rlty = RotateY(INIT_RLTX, INIT_RLTY, angle);
		rrtx = RotateX(INIT_RRTX, INIT_RRTY, angle);
		rrty = RotateY(INIT_RRTX, INIT_RRTY, angle);

		rlt2x = RotateX(INIT_RLT2X, INIT_RLT2Y, angle);
		rlt2y = RotateY(INIT_RLT2X, INIT_RLT2Y, angle);
		rrt2x = RotateX(INIT_RRT2X, INIT_RRT2Y, angle);
		rrt2y = RotateY(INIT_RRT2X, INIT_RRT2Y, angle);

		FLLdrX = RotateX(INIT_FLLdrX, INIT_FLLdrY, angle);
		FLLdrY = RotateY(INIT_FLLdrX, INIT_FLLdrY, angle);
		RLLdrX = RotateX(INIT_RLLdrX, INIT_RLLdrY, angle);
		RLLdrY = RotateY(INIT_RLLdrX, INIT_RLLdrY, angle);

		FRLdrX = RotateX(INIT_FRLdrX, INIT_FRLdrY, angle);
		FRLdrY = RotateY(INIT_FRLdrX, INIT_FRLdrY, angle);
		RRLdrX = RotateX(INIT_RRLdrX, INIT_RRLdrY, angle);
		RRLdrY = RotateY(INIT_RRLdrX, INIT_RRLdrY, angle);

		//ldrRung = RotateX(INIT_fLdrLX, INIT_fLdrLY, angle);

	}

	// Paint the bus to the display using the rotated coordinates.
	public override void Draw(Graphics g)
	{
		Pen pen = new Pen(Color.Black, (float)tire_width);
		// Front tires
		g.DrawLine(pen, (float)(x+fltx), (float)(y+flty),
						(float)(x+frtx), (float)(y+frty));
		// Rear tires
		g.DrawLine(pen, (float)(x+rltx), (float)(y+rlty),
						(float)(x+rrtx), (float)(y+rrty));
		// Second Rear tires
		g.DrawLine(pen, (float)(x+rlt2x), (float)(y+rlt2y),
			(float)(x+rrt2x), (float)(y+rrt2y));
		// Bus
		pen = new Pen(Color.Red, (float)width);
		g.DrawLine(pen, (float)(x+front_x), (float)(y+front_y),
			(float)(x+rear_x), (float)(y+rear_y));
		// Ladder - POLES
		pen = new Pen(Color.Black, (float)6);
		g.DrawLine(pen, (float)(x+FLLdrX), (float)(y+FLLdrY),
				(float)(x+RLLdrX), (float)(y+RLLdrY));
		pen = new Pen(Color.Red, (float)4);
		pen.DashPattern = new float[] {0.6f, 0.4f};
		g.DrawLine(pen, (float)(x+FRLdrX), (float)(y+FRLdrY),
			(float)(x+RRLdrX), (float)(y+RRLdrY));
//		pen = new Pen(Color.Black, (float)ldrPole);
//		g.DrawLine(pen, (float)(x+FLLdrX), (float)(y+FLLdrY),
//						(float)(x+RLLdrX), (float)(y+RLLdrY));
//		g.DrawLine(pen, (float)(x+FRLdrX), (float)(y+FRLdrY),
//			(float)(x+RRLdrX), (float)(y+RRLdrY));
		// Ladder - RUNGS
		//g.DrawLine(pen, (float)(x+FRLdrX), (float)(y+FRLdrY),
			//(float)(x+RRLdrX), (float)(y+RRLdrY));

	}
}
