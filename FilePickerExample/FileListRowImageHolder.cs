using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Widget;
using Java.Lang;

namespace com.xamarin.recipes.filepicker
{
	public class FileListRowImageHolder : Java.Lang.Object
	{

		/// <summary>
		///   This class is used to hold references to the views contained in a list row.
		/// </summary>
		/// <remarks>
		///   This is an optimization so that we don't have to always look up the
		///   ImageView and the TextView for a given row in the ListView.
		/// </remarks>
			
		public FileListRowImageHolder (TextView textView, ImageView imageView)
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
		public void Update (string fileName, string url, int fileImageResourceId)
		{
//			TextView.Text = fileName;
//			if (fileImageResourceId == 0) {
//				Bitmap bm = MakeImage (url);
//				if(bm != null)
//				{
//					ImageView.SetImageBitmap (bm);
//				}
//			} else {
//				ImageView.SetImageResource(fileImageResourceId);
//			}
		}


	}
}

