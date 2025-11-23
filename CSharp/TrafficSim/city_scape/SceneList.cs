// SceneList class

// SceneList is a linked list of SceneElements.
public class SceneList
{
	// The data structure used to compose the elements
	// of the linked list.
	private class SceneListNode
	{
		public SceneElement element;
		public SceneListNode next;
	}

	// An iterator over the elements of the SceneList.
	// The SceneList can be dynamically updated during
	// an iteration with the restriction that the element
	// last returned may not be deleted from the list.
	public class SceneListIterator
	{
		private SceneListNode current;	// Node last returned

		public SceneListIterator(SceneList sceneList)
		{
			current = sceneList.head;
		}

		// Return true if the last element of the iteration
		// has not yet been returned.  Return false otherwise.
		public bool HasMoreElements()
		{
			return current.next != null;
		}

		// Return the next element in the iteration.
		public SceneElement NextElement()
		{
			if(current.next != null)
			{
				current = current.next;
				return current.element;
			}
			else
				return null;
		}
	}

	// Start and end nodes of the linked list
	private SceneListNode head;
	private SceneListNode tail;


	// Construct an empty SceneList.
	public SceneList()
	{
		head = new SceneListNode();
		// head.next = null; // not needed since done automatically
		tail = head;
	}

	// Insert the element in increasing sorted order.
	public void Insert(SceneElement element)
	{
		// Find where in the list the element should be inserted
		SceneListNode p = head.next;
		SceneListNode prev = head;

		// Read through the layer values and find the first
		// layer value greater than the layer value of the
		// element to insert.
		while(p!=null && p.element.GetLayer()<=element.GetLayer())
		{
			prev = p;
			p = p.next;
		}

		// Insert the element after prev
		SceneListNode node = new SceneListNode();

		node.element = element;
		node.next = prev.next;
		prev.next = node;

		if (node.next==null)
			tail = node;
	}

	// Remove element from the list.  Return true if
	// element was present in the list and removed.
	// Return false if element was not present in the
	// list.
	public bool Remove(SceneElement element)
	{
		SceneListNode current = head.next;
		SceneListNode trailer = head;

		while(current!=null)
		{
			if(current.element == element)
			{
				trailer.next = current.next;

				if(tail == current)
					tail = trailer;

				current = null;
				return true;
			}

			trailer = current;
			current = current.next;
		}

		return false;
	}

	// Return an iterator over the elements of the
	// SceneList.
	public SceneListIterator GetIterator()
	{
		return new SceneListIterator(this);
	}
}
