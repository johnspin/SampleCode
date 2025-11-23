// Ambulance class

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

public class Ambulance: Mobile
{
	// constants to define the ambulance geometry
	// Front and rear positions of the ambulance
	private const double INIT_FRONT_X  =  8;
	private const double INIT_FRONT_Y  =  0;
	private const double INIT_REAR_X   = -8;
	private const double INIT_REAR_Y   =  0;


	// Length taken by an ambulance on the road (more than its physical length)
	private const double INIT_LENGTH = 30;

	private const double INIT_WIDTH = 10;
	private const double INIT_MAX_SPEED = 7;

	// Tires
	private const double INIT_FLTX =  5;
	private const double INIT_FLTY =  6;
	private const double INIT_FRTX =  5;
	private const double INIT_FRTY = -6;

	private const double INIT_RLTX = -5;
	private const double INIT_RLTY =  6;
	private const double INIT_RRTX = -5;
	private const double INIT_RRTY = -6;

	private const double INIT_TIRE_WIDTH = 3;

	// flasher bar location
	private const double INIT_TLtX = 5;
	private const double INIT_TLtY = -4;
	private const double INIT_BLtX = 5;
	private const double INIT_BLtY = 4;

	private const double INIT_LITE_WIDTH = 2;

	//red cross
	// top cross
	private const double INIT_TTCrX = 1;
	private const double INIT_TTCrY = 0;
	private const double INIT_TBCrX = -5;
	private const double INIT_TBCrY = 0;

	//side cross
	private const double INIT_SLCrX = -2;
	private const double INIT_SLCrY = 3;
	private const double INIT_SRCrX = -2;
	private const double INIT_SRCrY = -3;

	private double front_x;	// x coordinate of front of ambulance
	private double front_y;	// y coordinate of front of ambulance
	private double rear_x;	// x coordinate of rear of ambulance
	private double rear_y;	// y coordinate of rear of ambulance

	private double fltx, flty, frtx, frty;  // front tire coordinates
	private double rltx, rlty, rrtx, rrty;  // rear tire coordinates

	private double tLtX, tLtY, bLtX, bLtY;	// flasher bar coords

	private double tTCrX, tTCrY, tBCrX, tBCrY;	//cross
	private double sLCrX, sLCrY, sRCrX, sRCrY;	//cross
  
	private double lite_width;  // width of flashes
	private double width;		// width of ambulance
	private double tire_width;	// width of tires.
	private bool lite_side;		// which side of the flash bar is red

	public Ambulance(double x, double y, Scene scene): base(x,y,INIT_LENGTH, INIT_MAX_SPEED, scene)
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

		tLtX = INIT_TLtX;
		tLtY = INIT_TLtY;
		bLtX = INIT_BLtX;
		bLtY = INIT_BLtY;

		tTCrX = INIT_TTCrX;
		tTCrY = INIT_TTCrY;
		tBCrX = INIT_TBCrX;
		tBCrY = INIT_TBCrY;
		sLCrX = INIT_SLCrX;
		sLCrY = INIT_SLCrY;
		sRCrX = INIT_SRCrX;
		sRCrY = INIT_SRCrY;
		
		lite_width = INIT_LITE_WIDTH;
		width = INIT_WIDTH;
		tire_width = INIT_TIRE_WIDTH;
		lite_side = true;
	}


	// Rotate the coordinate of the ambulance such that the ambulance points in
	// in the direction of angle.  The units of angle is Radians.
	public override void SetDirection(double angle)
	{
		base.SetDirection(angle);

		front_x = RotateX(INIT_FRONT_X,  INIT_FRONT_Y,  angle);
		front_y = RotateY(INIT_FRONT_X,  INIT_FRONT_Y,  angle);
		rear_x  = RotateX(INIT_REAR_X,   INIT_REAR_Y,   angle);
		rear_y  = RotateY(INIT_REAR_X,   INIT_REAR_Y,   angle);

		fltx = RotateX(INIT_FLTX, INIT_FLTY, angle);
		flty = RotateY(INIT_FLTX, INIT_FLTY, angle);
		frtx = RotateX(INIT_FRTX, INIT_FRTY, angle);
		frty = RotateY(INIT_FRTX, INIT_FRTY, angle);

		rltx = RotateX(INIT_RLTX, INIT_RLTY, angle);
		rlty = RotateY(INIT_RLTX, INIT_RLTY, angle);
		rrtx = RotateX(INIT_RRTX, INIT_RRTY, angle);
		rrty = RotateY(INIT_RRTX, INIT_RRTY, angle);

		tLtX = RotateX(INIT_TLtX, INIT_TLtY, angle);
		tLtY = RotateY(INIT_TLtX, INIT_TLtY, angle);
		bLtX = RotateX(INIT_BLtX, INIT_BLtY, angle);
		bLtY = RotateY(INIT_BLtX, INIT_BLtY, angle);

		tTCrX = RotateX(INIT_TTCrX, INIT_TTCrY, angle);
		tTCrY = RotateY(INIT_TTCrX, INIT_TTCrY, angle);
		tBCrX = RotateX(INIT_TBCrX, INIT_TBCrY, angle);
		tBCrY = RotateY(INIT_TBCrX, INIT_TBCrY, angle);

		sLCrX = RotateX(INIT_SLCrX, INIT_SLCrY, angle);
		sLCrY = RotateY(INIT_SLCrX, INIT_SLCrY, angle);
		sRCrX = RotateX(INIT_SRCrX, INIT_SRCrY, angle);
		sRCrY = RotateY(INIT_SRCrX, INIT_SRCrY, angle);
	}

	// Paint the ambulance to the display using the rotated coordinates.
	public override void Draw(Graphics g)
	{
		Pen pen = new Pen(Color.Black, (float)tire_width);
		// Front tires
		g.DrawLine(pen, (float)(x+fltx), (float)(y+flty),
			(float)(x+frtx), (float)(y+frty));
		// Rear tires
		g.DrawLine(pen, (float)(x+rltx), (float)(y+rlty),
			(float)(x+rrtx), (float)(y+rrty));
		// Ambulance
		pen = new Pen(Color.White, (float)(width));
		g.DrawLine(pen, (float)(x+front_x), (float)(y+front_y),
		(float)(x+rear_x), (float)(y+rear_y));

		//cross
		pen = new Pen(Color.Red, (float)lite_width);
		g.DrawLine(pen, (float)(x+tTCrX), (float)(y+tTCrY),
			(float)(x+tBCrX), (float)(y+tBCrY));
		g.DrawLine(pen, (float)(x+sRCrX), (float)(y+sRCrY),
			(float)(x+sLCrX), (float)(y+sLCrY));

		// flasher bar
		if (lite_side)
			pen = new Pen(Color.Red, (float)lite_width);
		else
			pen = new Pen(Color.White, (float)lite_width);

		g.DrawLine(pen, (float)(x+tLtX), (float)(y+tLtY),
			(float)(x+bLtX), (float)(y+bLtY) );

//		if (lite_side)
//			pen.Color = Color.Blue;
//		else
//			pen.Color = Color.White;
//		g.DrawLine(pen, (float)(x), (float)(y),
//			(float)(x+bLtX), (float)(y+bLtY) );

		lite_side = !lite_side;
	}
}
