using Android.Graphics;
using Android.Content;

namespace com.xamarin.recipes.filepicker
{
	using Android.Widget;
	using Java.Lang;

	/// <summary>
	///   This class is used to hold references to the views contained in a list row.
	/// </summary>
	/// <remarks>
	///   This is an optimization so that we don't have to always look up the
	///   ImageView and the TextView for a given row in the ListView.
	/// </remarks>
	public class FileListRowViewHolder : Object
	{
		public FileListRowViewHolder (TextView textView, ImageView imageView)
		{
			TextView = textView;
			ImageView = imageView;
		}

		public ImageView ImageView { get; private set; }

		public TextView TextView { get; private set; }

		/// <summary>
		///   This method will update the TextView and the ImageView that are
		///   are
		/// </summary>
		/// <param name="fileName"> </param>
		/// <param name="fileImageResourceId"> </param>
		public void Update (string fileName, int fileImageResourceId, string url, Context c)
		{
			try {

				//http://stackoverflow.com/questions/10942959/how-to-show-different-layout-in-fragments

				TextView.Text = fileName;
				//ImageView.SetImageResource(fileImageResourceId);
				if (fileImageResourceId == Resource.Drawable.file) {

					if (url.IndexOf (".jpg") != -1) {
						//Toast.MakeText (c, url, ToastLength.Long).Show ();
						Bitmap bm = MakeImage (url);
					if (bm != null) {
						ImageView.SetImageBitmap (bm);
					}
						else
						{
						Toast.MakeText(c, "no bitmap", ToastLength.Long).Show();
						}
					}
				} else {
					ImageView.SetImageResource (fileImageResourceId);
				}
			} catch (Exception ex) {
				Toast.MakeText (c, ex.ToString (), ToastLength.Long).Show ();
			}
		}

		Bitmap MakeImage (string url)
		{
			Bitmap bm = null;
			try {
				BitmapFactory.Options options = new BitmapFactory.Options ();
				options.InSampleSize = 2;
				bm = BitmapFactory.DecodeFile (url, options);
			} catch (Exception ex) {
			
			}

			return bm;
		}
	}
}
