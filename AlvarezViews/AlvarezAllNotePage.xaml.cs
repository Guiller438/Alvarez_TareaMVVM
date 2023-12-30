namespace Alvarez_TareaMVVM.AlvarezViews;

public partial class AlvarezAllNotePage : ContentPage
{
	public AlvarezAllNotePage()
	{
		InitializeComponent();
	}

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        notesCollection.SelectedItem = null;
    }
}