//
//Teresa Wilson-Gunn and John Spinosa
//CSC 298 Assignment 4 Part 1
//11/4/02
//
// We are in full compliance with Part 1 of the requirements 
// of the assignment.  To toggle animation on, use the Enter 
// key.  To toggle the animation off, use the space key. 
//

// Scene class

using System.Windows.Forms; 
using System;
using System.Drawing;

// The Scene is the collection of all of the elements of the simulation:
// the Mobile objects, the Conduits, etc.  When a SceneElement is
// constructed, it is automatically placed into the scene. 
// There is only one Scene.

public class Scene: Form
{
	// List of the scene elements
	private SceneList sceneList;

	// Dimension of the graphics window (inside the frame)
	private const int HEIGHT=500;
	private const int WIDTH=600;

	// A timer for the animation (add code here)
	private Timer timer = new Timer();

	// Create background image
	//Image image = Image.FromFile("O:\\\\CITYMAP.BMP");
	Image image = Image.FromFile(@"/Users/john/Projects/TrafficSim/city_scape/citymap.bmp");
		//"C:\\Documents and Settings\\JS\\My Documents\\My Programs\\City Scape\\citymap.old.bmp");
	//Image image = Image.FromFile("http://www.edison.seattlecentral.org/~twilso08/citymap.html");

	// Constructor
	public Scene()
	{
		// Create a list for the SceneElements
		sceneList = new SceneList();

		// Build the graphics window
		ClientSize = new Size(WIDTH,HEIGHT);
		BackColor = SystemColors.Window;
		//BackColor = Color.Green;
		ForeColor = SystemColors.WindowText;
		//Location = Point.Empty;
		Location = new Point(40,40);

		// No flickering
		SetStyle(ControlStyles.AllPaintingInWmPaint |
				 ControlStyles.DoubleBuffer |
				 ControlStyles.UserPaint, true);

		// Initialize the timer (add code here)
		timer.Interval = 100; // interval in ms
		timer.Tick += new EventHandler(TimerOnTick);

		// Start ticking
		timer.Enabled = true;

		// Construct the scene
		Build();
	}

	// Note: you will need to add two methods to deal with the animation
	// namely an event method for the timer and an event method for the 
	// keyboard.

	private void TimerOnTick(object source, EventArgs ea)
	{
		UpdateScene();
		Invalidate();
	}

	protected override void OnKeyDown(KeyEventArgs kea)
	{
			// start animation
		if (kea.KeyCode == Keys.Enter)
			timer.Enabled = true;

		if (kea.KeyCode == Keys.Up)
		{
			// speed up the animation
			if (timer.Interval > 10) timer.Interval -= 10;
		}
		else if (kea.KeyCode == Keys.Down)
			// slow down the animation
			timer.Interval += 10;

		if (kea.KeyCode == Keys.Space)
		{
			// stop animation
			timer.Enabled = false;
		}
	}


	// Paint the scene
	protected override void OnPaint(PaintEventArgs pea)
	{
		Graphics g = pea.Graphics;
		g.DrawImage(image,0,0,600,500);
		Draw(g);
		
	}

	// Add a SceneElement to the Scene.
	public void Add(SceneElement element)
	{
		sceneList.Insert(element);
	}

	// Remove a SceneElement from the Scene.
	public bool Remove(SceneElement element)
	{
		return sceneList.Remove(element);
	}

	// Update the Scene.  This will update all of the SceneElements
	// currently in the Scene.
	public void UpdateScene()
	{
		SceneList.SceneListIterator iter = sceneList.GetIterator();

		while(iter.HasMoreElements())
		{
			SceneElement e = iter.NextElement();
			e.Update();
		}
	}

	// Paint the Scene.  This will paint all of the SceneElements
	// currently in the Scene.
	private void Draw(Graphics g)
	{
		//g.Clear(Color.Transparent);

		SceneList.SceneListIterator iter = sceneList.GetIterator();

		while(iter.HasMoreElements())
		{
			SceneElement e = iter.NextElement();
			e.Draw(g);
		}
	}

	private void Build()
	{
		// create the scene elements
		Generator g = new Generator(0,100, 41,this);
		Router ro = new Router(80,150,this);

		Relay r1 = new Relay(80,100,this);
		Relay r15 = new Relay(197,100,this);
		Relay r17 = new Relay(267,100,this);

		Relay r2 = new Relay(80,290,this);
		Relay r3 = new Relay(550,100,this);
		Relay r4 = new Relay(550,290,this);
		Merge m = new Merge(550,290,10,this);
		Stoplight s = new Stoplight(75,150,0,1,this);
		//s.SetLayer(1);
		//SafetyLight s = new SafetyLight(75,150,0,1,10,this);

		RRCrossing rrcr = new RRCrossing(80,210,18,this);
		RRCrossing rrcr2 = new RRCrossing(550,150,18,this);
		BoatCrossing bcr1 = new BoatCrossing(229,100,40,this);
		//BoatCrossing bcr2 = new BoatCrossing(484,290,40,this);
		//BoatCrossing bcr3 = new BoatCrossing(440,400,40,this);

		Destination d = new Destination(600,290,this);

		new Road(s,ro,6,this);
		new Road(ro,r1,6,this);

		//new Road(ro,r2,6,this);
		new Road(ro,rrcr,6,this);
		new Road(rrcr,r2,6,this);

		//new Road(r1,r3,6,this);
		new Road(r1,r15,6,this);
		new BridgeRoad(r15,bcr1,6,this);
		new BridgeRoad(bcr1,r17,6,this);
		new Road(r17,r3,6,this);

		new Road(r2,r4,6,this);
		//new Road(r2,bcr2,6,this);
		//new Road(bcr2,r4,6,this);


		//new Road(r3,m,6,this);
		new Road(r3,rrcr2,6,this);
		new Road(rrcr2,m,6,this);

		new Road(r4,m,6,this);
    new Road(m,d,6,this);

		// Dead man's curve
		AccidentRouter r2suv = new AccidentRouter(60,140,this);
		AccidentDestination dsuv = new AccidentDestination(150,190,this);

		new Road(g,r2suv,6,this);
		new Road(r2suv,s,6,this);
		new InvRoad(r2suv,dsuv,6,this);
						
		// C# highway
		Generator g2 = new Generator(0,50, 31,this);
		Destination d2 = new Destination(600,50,this);
		HOVGenerator hovg2 = new HOVGenerator(0,65, 32,this);
		Destination hovd2 = new Destination(600,65,this);

		SceneLine sl = new SceneLine(0,40,605,40,Color.Yellow,5,1,this);
		SceneLine s2 = new SceneLine(0,23,600,23,Color.White,1,1, this);
		SceneLine s3 = new SceneLine(0,57,600,57,Color.White,1,1,this);

		Generator g3 = new Generator(600,30, 33,this);
		Destination d3 = new Destination(0,30,this);
		HOVGenerator hovg3 = new HOVGenerator(600,15, 34,this);
		Destination hovd3 = new Destination(0,15,this);

		new Road(g2, d2, 100,this);
		new Road(hovg2, hovd2, 100,this);
		new Road(g3, d3, 100, this);
		new Road(hovg3, hovd3, 100, this);
		
		// C# scenic way
		Generator g1 = new Generator(600,400,35,this);
		Router ro1 = new Router(395,400,this);
		Relay r11 = new Relay(365,475,this);
		Relay r21 = new Relay(425,325,this);
		Relay r31 = new Relay(80,450,this);
		Relay r41 = new Relay(80,325,this);
		Merge m1 = new Merge(80,425,10,this);

		Stoplight s11 = new Stoplight(500,400,0,1,this);
		//s11.SetLayer(1);

		Destination d1 = new Destination(0,425,this);

		new Road(g1,s11,6,this);

		new Road(s11,ro1,6,this);
		//new Road(s11,bcr3,6,this);
		//new Road(bcr3,ro1,6,this);

		new Road(ro1,r11,6,this);
		new Road(ro1,r21,6,this);
		new Road(r21,r31,6,this);
		new Road(r11,r41,6,this);
		new Road(r31,m1,6,this);
		new Road(r41,m1,6,this);
		new Road(m1,d1,6,this);

		//it appears that the Road must be assigned to a variable if you want it 
		//be ontop of or under
		new Road(r11,r41,6,3,this);
		new Road(m1,d1,6,this);

		// Start the animation (add some code below)

		//A river for the boats
		BoatGenerator rr1 = new BoatGenerator(300,0,100,this);
		Relay rr2 = new Relay(125,250,this);
		Relay rr3 = new Relay(500,250,this);
		Destination rr4 = new Destination(400,500,this);

		//new River(rr1,rr2,35,-3,this);
		new River(rr1,bcr1,35,-3,this);
		new River(bcr1,rr2,35,-3,this);

		new River(rr2,rr3,35,-3,this);
		
		new River(rr3,rr4,35,-3,this);
		//new River(rr3,bcr2,35,-3,this);
		//new River(bcr2,rr4,35,-3,this);
		//new River(bcr2,bcr3,35,-3,this);
		//new River(bcr3,rr4,35,-3,this);

		// start the animation

		// Train track for the train
		TrainGenerator tg = new TrainGenerator(600,150,95,this);
		Relay tr1 = new Relay(245, 150, this);
		Relay tr2 = new Relay(205, 210, this);
		//RRCrossing rrcr = new RRCrossing(80,210,20,this);
		Destination td = new Destination(0,210, this);

		//new RRTrack(tg,tr1,7,2,this);
		new RRTrack(tg,rrcr2,7,2,this);
		new RRTrack(rrcr2,tr1,7,2,this);

 		new RRTrack(tr1,tr2,7,2,this);

		new RRTrack(tr2,rrcr,7,2,this);
		new RRTrack(rrcr,td,7,2,this);
		//new RRTrack(tg2,td,7,2,this);
	}

	// Where the action starts
	public static void Main()
	{
		Application.Run(new Scene());
	}
}