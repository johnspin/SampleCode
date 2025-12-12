// Boat class

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

public class Boat: Mobile {
	// constants to define the Boat geometry
	// Front and rear positions of the Boat
	private const double INIT_FRONT_X  =  8;
	private const double INIT_FRONT_Y  =  0;
	private const double INIT_REAR_X   = -9;
	private const double INIT_REAR_Y   =  0;


	// Length taken by a Boat on the River (more than its physical length)
	private const double INIT_LENGTH = 40;

	private const double INIT_FRONT_WIDTH = 8;
	private const double INIT_MAX_SPEED = 5;

	private double front_x;	// x coordinate of front of Boat
	private double front_y;	// y coordinate of front of Boat
	private double rear_x;	// x coordinate of rear of Boat
	private double rear_y;	// y coordinate of rear of Boat

	private double width_front;		// width of Boat front

	public Boat(double x, double y, Scene scene): base(x,y,INIT_LENGTH, INIT_MAX_SPEED, scene) {
		front_x = INIT_FRONT_X;
		front_y = INIT_FRONT_Y;
		rear_x  = INIT_REAR_X;
		rear_y  = INIT_REAR_Y;

		width_front = INIT_FRONT_WIDTH;
	}

	// Rotate the coordinate of the Boat such that the Boat points in
	// in the direction of angle.  The units of angle is Radians.
	public override void SetDirection(double angle)
	{
		base.SetDirection(angle);

		front_x = RotateX(INIT_FRONT_X,  INIT_FRONT_Y,  angle);
		front_y = RotateY(INIT_FRONT_X,  INIT_FRONT_Y,  angle);
		rear_x  = RotateX(INIT_REAR_X,   INIT_REAR_Y,   angle);
		rear_y  = RotateY(INIT_REAR_X,   INIT_REAR_Y,   angle);
	}

	// Paint the Boat to the display using the rotated coordinates.
	public override void Draw(Graphics g)
	{
		//pen.StartCap = LineCap.Triangle;
		//pen.EndCap = LineCap.Round;

		// Hull
		Pen pen = new Pen(Color.White, (float)width_front);
		pen.StartCap = LineCap.Triangle;
		pen.EndCap = LineCap.Round;
		g.DrawLine(pen, (float)(x+front_x), (float)(y+front_y),
			(float)(x+rear_x), (float)(y+rear_y));

		/*float xFactor= (float)((rear_x-front_x)/5.0), yFactor=(float)((rear_y-front_y)/5.0);

		Pen pen = new Pen(Color.White, (float)width_front);
		pen.StartCap = LineCap.Triangle;
		pen.EndCap = LineCap.Round;
		for (int i=0; i<5; i++)	{
			g.DrawLine(pen, (float)(x-rear_x+xFactor*i), (float)(y-rear_y+yFactor*i),
				(float)( x-rear_x+xFactor*(i+1) ), (float)( y-rear_y+yFactor*(i+1) ) );
			pen.Width--;
//			if (pen.Color ==  Color.Black)
//				pen.Color = Color.White;
//			else
//				pen.Color = Color.Black;
		}*/
	}
}
