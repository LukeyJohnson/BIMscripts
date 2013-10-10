/*
 * Revit Macro
 * Created by: troygates
 * Created on: 10/10/2013
 *
 * Version: Revit 2013/2014
 * Description: This macro will delete all views except for floor and ceiling plans.
 */
 
public void DeleteViews()
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
		
		// add a counter to count the views deleted
		int x = 0;
		
		// loop through each view in the model
		foreach (Element e in collection)
		{			    	
			try 
			{	
				View view = e as View;
				
				// determine what type of view it is
				switch(view.ViewType)
				{
					// if view is a floor plan, don't delete
					case ViewType.FloorPlan:
						break;
					// if view is a ceiling plan, don't delete
					case ViewType.CeilingPlan:
						break;
					// if view is a sheet, don't delete
					case ViewType.DrawingSheet:
						break;
					// all other views can be deleted and increment counter by 1
					default:
						doc.Delete(e);
						x += 1;
						break;
				}
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
		TaskDialog.Show("DeleteSheets", "Views Deleted: " + x.ToString());
	}