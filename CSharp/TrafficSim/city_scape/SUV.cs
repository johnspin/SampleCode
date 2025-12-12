// SUV class
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

public class Suv: Mobile
{
	// constants to define the car geometry
	// length taken by the SUV on the road
	private const double INIT_LENGTH = 20;
	private const double INIT_WIDTH = 9;
	private const double INIT_MAX_SPEED = 8;

	// body
	private const double INIT_FRONT_X  =  9;
	private const double INIT_FRONT_Y  =  0;
	private const double INIT_REAR_X   = -9;
	private const double INIT_REAR_Y   =  0;

	// tire-front
	private const double INIT_FLTX =  3;
	private const double INIT_FLTY =  5.5;
	private const double INIT_FRTX =  3;
	private const double INIT_FRTY = -5.5;

	// tire - rear
	private const double INIT_RLTX = -4;
	private const double INIT_RLTY =  5.5;
	private const double INIT_RRTX = -4;
	private const double INIT_RRTY = -5.5;

	// death symbol location
	private const double INIT_St1X = 3.5;
	private const double INIT_St1Y = 0;

	private const double INIT_St2X = 1.25;
	private const double INIT_St2Y = 0;

	// crossbones
	// top cross
	private const double INIT_TTCrX = -4;
	private const double INIT_TTCrY = 3;
	private const double INIT_TBCrX = -1;
	private const double INIT_TBCrY = -3;

	//side cross
	private const double INIT_SLCrX = -4;
	private const double INIT_SLCrY = -3;
	private const double INIT_SRCrX = -1;
	private const double INIT_SRCrY = 3;

	private const double INIT_STACK1_WIDTH = 3.5;
	private const double INIT_STACK2_WIDTH = 1.5;

	private const double INIT_TIRE_WIDTH = 3;


	private double front_x;	// x coordinate of front of car
	private double front_y;	// y coordinate of front of car
	private double rear_x;	// x coordinate of rear of car
	private double rear_y;	// y coordinate of rear of car

	private double fltx, flty, frtx, frty;  // front tire coordinates
	private double rltx, rlty, rrtx, rrty;  // rear tire coordinates

	private double St1X;  // death symbol coordinates
	private double St1Y;
	
	private double St2X;
	private double St2Y;

	private double tTCrX, tTCrY, tBCrX, tBCrY;	//crossbones
	private double sLCrX, sLCrY, sRCrX, sRCrY;	//crossbones

	private double width;		// width of car
	private double tire_width;	// width of tires.
	
	private double width_stack1;	// width of death symbol
	private double width_stack2;

	public Suv(double x, double y, Scene scene): base(x, y, INIT_LENGTH, INIT_MAX_SPEED, scene)
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

		St1X = INIT_St1X;
		St1Y = INIT_St1Y;

		St2X = INIT_St2X;
		St2Y = INIT_St2Y;

		tTCrX = INIT_TTCrX;
		tTCrY = INIT_TTCrY;
		tBCrX = INIT_TBCrX;
		tBCrY = INIT_TBCrY;
		sLCrX = INIT_SLCrX;
		sLCrY = INIT_SLCrY;
		sRCrX = INIT_SRCrX;
		sRCrY = INIT_SRCrY;

		width = INIT_WIDTH;
		tire_width = INIT_TIRE_WIDTH;

		width_stack1 = INIT_STACK1_WIDTH;
		width_stack2 = INIT_STACK2_WIDTH;
	}

	// Rotate the coordinate of the SUV such that the SUV points in
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

		St1X = RotateX(INIT_St1X, INIT_St1Y, angle);
		St1Y = RotateY(INIT_St1X, INIT_St1Y, angle);

		St2X = RotateX(INIT_St2X, INIT_St2Y, angle);
		St2Y = RotateY(INIT_St2X, INIT_St2Y, angle);

		tTCrX = RotateX(INIT_TTCrX, INIT_TTCrY, angle);
		tTCrY = RotateY(INIT_TTCrX, INIT_TTCrY, angle);
		tBCrX = RotateX(INIT_TBCrX, INIT_TBCrY, angle);
		tBCrY = RotateY(INIT_TBCrX, INIT_TBCrY, angle);

		sLCrX = RotateX(INIT_SLCrX, INIT_SLCrY, angle);
		sLCrY = RotateY(INIT_SLCrX, INIT_SLCrY, angle);
		sRCrX = RotateX(INIT_SRCrX, INIT_SRCrY, angle);
		sRCrY = RotateY(INIT_SRCrX, INIT_SRCrY, angle);
	}

	// Paint the SUV to the display using the rotated coordinates.
	public override void Draw(Graphics g)
	{
		Pen pen = new Pen(Color.Black, (float)tire_width);
		// Front tires
		g.DrawLine(pen, (float)(x+fltx), (float)(y+flty),
			(float)(x+frtx), (float)(y+frty));

		// Rear tires
		g.DrawLine(pen, (float)(x+rltx), (float)(y+rlty),
			(float)(x+rrtx), (float)(y+rrty));
		
		// SUV
		pen = new Pen(Color.Black, (float)width);
		float xFactor= (float)((rear_x-front_x)/3.0), yFactor=(float)((rear_y-front_y)/3.0);

		g.DrawLine(pen, (float)(x+front_x), (float)(y+front_y),
			(float)(x+rear_x), (float)(y+rear_y));
		pen.Color = Color.Black;
		pen.Width=(float)width;
		g.DrawLine(pen, (float)(x-rear_x+xFactor), (float)(y-rear_y+yFactor),
			(float)( x-rear_x+xFactor*2 ), (float)( y-rear_y+yFactor*2 ) );

		//death symbol
//		pen = new Pen(Color.White, (float)width_stack1);
//		pen.StartCap = LineCap.Round;
//		pen.EndCap = LineCap.Round;
//		//SolidBrush sbr = new SolidBrush();
//		//sbr.Color = Color.White;
//		g.DrawEllipse(pen, (float)(x+St1X), (float)(y+St1Y),
//			(float)(width_stack1/2), (float)(width_stack1/2));
//
//		//death symbol
//		//pen = new Pen(Color.White, (float)width_stack2);
//		//pen.StartCap = LineCap.Round;
//		//pen.EndCap = LineCap.Round;
//		pen.Width = (float)width_stack2;
//		//FillEllipse(new SolidBrush(color),(int)x-5,(int)y-5,10,10);
//		g.DrawEllipse(pen, (float)(x+St2X), (float)(y+St2Y),
//			(float)(width_stack2/2), (float)(width_stack2/2));

		//crossbones
		pen = new Pen(Color.White, (float)width_stack2-2);
		g.DrawLine(pen, (float)(x+tTCrX), (float)(y+tTCrY),
			(float)(x+tBCrX), (float)(y+tBCrY));
		g.DrawLine(pen, (float)(x+sRCrX), (float)(y+sRCrY),
			(float)(x+sLCrX), (float)(y+sLCrY));
	}
}