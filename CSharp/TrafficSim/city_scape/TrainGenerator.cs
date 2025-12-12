// TrainGenerator class
using System;
using System.Drawing;

public class TrainGenerator: Conveyor
{
	private double x, y;
	private Conveyor cout;
	private int rate;  // The rate at which new Mobile objects are generated.
	private int timer; // A count down timer for generating the next object.
	private string genType;	// this will be "Engine", "TrainCar", "Caboose"
	//private int tCarCount;

	/// <summary>
	/// Initializes a new instance of the <see cref="TrainGenerator"/> class.
	/// </summary>
	/// <param name="new_x">New_x.</param>
	/// <param name="new_y">New_y.</param>
	/// <param name="rate">Rate.</param>
	/// <param name="scene">Scene.</param>
	public TrainGenerator(double new_x, double new_y, int rate, Scene scene): base(scene)
	{
		x = new_x;
		y = new_y;
		this.rate = rate;
		timer = 0;
		genType = "Engine";
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

	public override void Update () {
		if (timer == 0) {  //added on plane should go in next if statement
			
			if (genType.Equals ("Engine")) {
				Mobile new_mobile;

				new_mobile = new TrainEngine (x, y, this.scene);
			
				if (cout.SpaceRear (this) >= new_mobile.GetLength () / 2) {
					cout.HandOff (new_mobile, 0);
					genType = (new Random ().Next (3) == 0 ? "Engine" : "TrainCar");
					//tCarCount = 0;
				} else {
					// Delete the mobile from the SceneList
					scene.Remove (new_mobile);
				}	

				timer = new Random ().Next (int.MaxValue) % rate;
			} else {
				Mobile new_mobile;

				if (genType.Equals ("TrainCar")) {
					new_mobile = new TrainCar (x, y, this.scene);

					if (cout.SpaceRear (this) >= new_mobile.GetLength () / 2) {
						cout.HandOff (new_mobile, 0);
						//genType = "TrainCar";
						if (new Random ().Next (15) < 3)
							genType = "Caboose";
						//tCarCount++;
					} else {
						// Delete the mobile from the SceneList
						scene.Remove (new_mobile);
					}	
					//int rdm = ;
					//if (tCarCount > 3)

				} else if (genType.Equals ("Caboose")) {
					new_mobile = new Caboose (x, y, this.scene);

					if (cout.SpaceRear (this) >= new_mobile.GetLength () / 2) {
						cout.HandOff (new_mobile, 0);
						genType = "Engine";
						//tCarCount = 0;
						timer = new Random ().Next (int.MaxValue) % rate / 2;
					} else {
						// Delete the mobile from the SceneList
						scene.Remove (new_mobile);
					}	
				}

			}
		}  //added on plane
		timer = (timer+1)%rate;
	}

	public override void Draw(Graphics g){}
}