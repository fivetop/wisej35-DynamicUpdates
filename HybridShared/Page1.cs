using System.IO;
using Wisej.Hybrid;
using Wisej.Web;

namespace HybridDynamic
{
	public partial class Page1 : Page
	{
		public Page1()
		{
			InitializeComponent();
		}

		private void Page1_Load(object sender, System.EventArgs e)
		{
			AlertBox.Show($"This is a dynamically downloaded Page!");
		}
	}
}
