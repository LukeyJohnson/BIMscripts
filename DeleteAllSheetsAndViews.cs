public void DeleteAllSheetsAndViews()
{
	// setup uidoc and doc for accessing the Revit UI (uidoc) and the Model (doc)
	UIDocument uidoc = this.ActiveUIDocument;
	Document doc = uidoc.Document;
	
	// get all the elements in the model database
	FilteredElementCollector collector = new FilteredElementCollector(doc);
	// filter out all elements except Views
	ICollection<Element> collection = collector.OfClass(typeof(View)).ToElements();
	
	// create a transaction
	using(Transaction t = new Transaction(doc, "Delete Views"))
	{
		// start the transaction
		t.Start();
		
		// add a counter to count the views/sheets deleted
		int x = 0;
		
		// loop through each view in the model
		foreach (Element e in collection)
		{			    	
			try 
			{	
				View view = e as View;
				
				// all views/sheets are deleted and increment counter by 1
				doc.Delete(e);
				x += 1;
			}
			catch(Exception ex)
			{
				// uncomment below to show error messages
				//View view = e as View;
				//TaskDialog.Show("Error", e.Name + "\n" + "\n" + ex.Message);
				//TaskDialog.Show("Error", ex.Message);
			}
		}
		// finalize the transaction
		t.Commit();
		// show message with number of views/sheets deleted
		TaskDialog.Show("DeleteSheets", "Views & Sheets Deleted: " + x.ToString());
	}
}	