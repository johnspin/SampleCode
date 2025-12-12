// Generator class
using System;
using System.Drawing;

public class Generator: Conveyor
{
	private double x, y;
	private Conveyor cout;
	private int rate;  // The rate at which new Mobile objects are generated.
	private int timer; // A count down timer for generating the next object.

	public Generator(double new_x, double new_y, int rate, Scene scene): base(scene)
	{
		x = new_x;
		y = new_y;
		this.rate = rate;
		timer = 0;
	}

	public override double InX() { return x; }
	public override double InY() { return y; }
	public override double OutX() { return x; }
	public override double OutY() { return y; }

	public override void AddIn(Conveyor new_out) {}
	public override void AddOut(Conveyor new_out) { cout = new_out; }

	public override double SpaceFront() { return 0; }
	public override double SpaceRear(Conveyor me)  { return 0; }

	public override void HandOff(Mobile mobile, double residual_speed) {}

	public override void Update()
	{
		if (timer == 0)
		{
			Mobile new_mobile;
			double rd = new Random().NextDouble();

			/*if (rd <= 0.3)
				new_mobile = null;
			else*/
			if (rd <= 0.2)
				new_mobile = new Suv(x, y, this.scene);
			else if (rd <= 0.4)
				new_mobile = new Motorcycle(x, y, this.scene);
			else if (rd <= 0.6)
				new_mobile = new Bus(x, y, this.scene);
			else //(rd <= 0.6)
				new_mobile = new Car(x, y, this.scene);
				
			if (new_mobile != null)	{
				if(cout.SpaceRear(this) >= new_mobile.GetLength()/2)
					cout.HandOff(new_mobile, 0);
				else {
					// Delete the mobile from the SceneList
					scene.Remove(new_mobile);
				}	
			}
			timer = new Random().Next(int.MaxValue) % rate;
		}
		timer = (timer+1)%rate;
	}

	public override void Draw(Graphics g){}
}
