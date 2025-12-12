// Police Car class
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

public class Police: Mobile
{
	// constants to define the car geometry
	// length taken by the Police car on the road
	private const double INIT_LENGTH = 26;	//give length on road
	private const double INIT_WIDTH = 6;
	private const double INIT_MAX_SPEED = 10;

	//body
	private const double INIT_FRONT_X  =  9;
	private const double INIT_FRONT_Y  =  0;
	private const double INIT_REAR_X   = -9;
	private const double INIT_REAR_Y   =  0;

	//tire - front
	private const double INIT_FLTX =  5;
	private const double INIT_FLTY =  5;
	private const double INIT_FRTX =  5;
	private const double INIT_FRTY = -5;

	//tire - rear
	private const double INIT_RLTX = -5;
	private const double INIT_RLTY =  5;
	private const double INIT_RRTX = -5;
	private const double INIT_RRTY = -5;

	//flasher bar location
	private const double INIT_TLtX = 0;
	private const double INIT_TLtY = -4;
	private const double INIT_BLtX = 0;
	private const double INIT_BLtY = 4;

	private const double INIT_LITE_WIDTH = 3;
	//private const double INIT_BAR_WIDTH = 1;
	//private const double INIT_

	private const double INIT_TIRE_WIDTH = 3;


	private double front_x;	// x coordinate of front of car
	private double front_y;	// y coordinate of front of car
	private double rear_x;	// x coordinate of rear of car
	private double rear_y;	// y coordinate of rear of car

	private double fltx, flty, frtx, frty;  // front tire coordinates
	private double rltx, rlty, rrtx, rrty;  // rear tire coordinates

	private double tLtX, tLtY, bLtX, bLtY;

	private double lite_width;	// width of flashies
	private double width;		// width of car
	private double tire_width;	// width of tires.
	private bool lite_side;		//which side of the flash bar is red;

	public Police(double x, double y, Scene scene): base(x, y, INIT_LENGTH, INIT_MAX_SPEED, scene)
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

		lite_width = INIT_LITE_WIDTH;
		width = INIT_WIDTH;
		tire_width = INIT_TIRE_WIDTH;
		lite_side = true;
	}

	// Rotate the coordinate of the car such that the car points in
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

		tLtX = RotateX(INIT_TLtX, INIT_TLtY, angle);
		tLtY = RotateY(INIT_TLtX, INIT_TLtY, angle);
		bLtX = RotateX(INIT_BLtX, INIT_BLtY, angle);
		bLtY = RotateY(INIT_BLtX, INIT_BLtY, angle);

	}

	// Paint the Police car to the display using the rotated coordinates.
	public override void Draw(Graphics g)
	{
		Pen pen = new Pen(Color.Black, (float)tire_width);
		// Front tires
		g.DrawLine(pen, (float)(x+fltx), (float)(y+flty),
						(float)(x+frtx), (float)(y+frty));

		// Rear tires
		g.DrawLine(pen, (float)(x+rltx), (float)(y+rlty),
						(float)(x+rrtx), (float)(y+rrty));
		
		// Police Car
		pen = new Pen(Color.Black, (float)(width+2));
		float xFactor= (float)((rear_x-front_x)/3.0), yFactor=(float)((rear_y-front_y)/3.0);

		/*for (int i=0; i<3; i++)	{
			g.DrawLine(pen, (float)(x-rear_x+xFactor*i), (float)(y-rear_y+yFactor*i),
				(float)( x-rear_x+xFactor*(i+1) ), (float)( y-rear_y+yFactor*(i+1) ) );
			if (pen.Color ==  Color.Black)
				pen.Color = Color.White;
			else
				pen.Color = Color.Black;
		}*/
		g.DrawLine(pen, (float)(x+front_x), (float)(y+front_y),
			(float)(x+rear_x), (float)(y+rear_y));
		pen.Color = Color.White;
		pen.Width=(float)width;
		g.DrawLine(pen, (float)(x-rear_x+xFactor), (float)(y-rear_y+yFactor),
			(float)( x-rear_x+xFactor*2 ), (float)( y-rear_y+yFactor*2 ) );

		//Draw flasher bar
//		pen = new Pen(Color.Black, (float)lite_width);
//		g.DrawLine(pen, (float)(x+tLtX), (float)(y+tLtY),
//			(float)(x+bLtX), (float)(y+bLtY) );

		//pen.Width++;
		if (lite_side)
			pen = new Pen(Color.Red, (float)lite_width);
		else
			pen = new Pen(Color.White, (float)lite_width);
		g.DrawLine(pen, (float)(x+tLtX), (float)(y+tLtY),
				(float)(x), (float)(y) );

		if (lite_side)
			pen.Color = Color.Blue;
		else
			pen.Color = Color.White;
		g.DrawLine(pen, (float)(x), (float)(y),
			(float)(x+bLtX), (float)(y+bLtY) );

		lite_side = !lite_side;

	}
}
