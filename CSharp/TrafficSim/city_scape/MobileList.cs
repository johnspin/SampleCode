// MobileList class


public class MobileList
{
	// MobileListIterator is an interator over the elements of a MobileList.
	public class MobileListIterator
	{
		private int current; // last element returned
		private MobileList mobileList;

		public MobileListIterator(MobileList mobileList)
		{
			this.mobileList = mobileList;
			this.current = mobileList.front;
		}

		// Return true if there are more elements in the iteration.
		// Return false otherwise.
		public bool HasMoreElements() { return current != mobileList.rear; }

		// Return the next element of the iteration.
		// If the iteration has been exhausted, return null.
		public Mobile NextElement()
		{
			if(current != mobileList.rear)
			{
				Mobile mobile = mobileList.mobiles[current];
				current = (current + 1) % mobileList.mobiles_size;
				return mobile;
			}
			else
				return null;
		}
	}


	private Mobile[] mobiles; // Array of Mobile objects
	private int mobiles_size; // size of the array of Mobile objects
	private int front; // index of front of list.
	private int rear; // index immediately after end of list.

	public MobileList()
	{
		mobiles = new Mobile[1];
		mobiles_size = 1;
		front = 0;
		rear = 0;
	}

	// Return true if the MobileList is empty.
	// Return false otherwise.
	public bool IsEmpty()
	{
		return rear == front;
	}

	// Return the front most element of the MobileList.  Do not alter
	// the list.
	public Mobile PeekFront()
	{
		if(!IsEmpty())
			return mobiles[front];
		else
			return null;
	}

	// Return the rear most element of the MobileList.  Do not alter
	// the list.
	public Mobile PeekRear()
	{
		if(!IsEmpty())
			return mobiles[(rear+mobiles_size-1)%mobiles_size];
		else
			return null;
	}

	// Insert mobile into the rear of the MobileList.
	public void InsertRear(Mobile mobile)
	{
		mobiles[rear] = mobile;

		rear = (rear + 1) % mobiles_size;

		// Dynamically expand the size of the MobileList to accomodate
		// additional elements.

		if(rear == front)
		{
			Mobile[] new_mobiles = new Mobile[2*mobiles_size];
			int i = 0;

			do
			{
				new_mobiles[i] = mobiles[front];
				i++;
				front = (front + 1) % mobiles_size;
			}
			while(front != rear);

			front = 0;
			rear = mobiles_size;
			mobiles_size = 2*mobiles_size;
			mobiles = new_mobiles;
		}
	}

	// Remove and return the front most element of the MobileList.
	public Mobile RemoveFront()
	{
		if(IsEmpty())
			return null;

		int oldFront = front;
		front = (front + 1) % mobiles_size;
		return mobiles[oldFront];
	}

	// Return an iterator over the elements of the MobileList.
	public MobileListIterator GetIterator()
	{
		return new MobileListIterator(this);
	}
}
