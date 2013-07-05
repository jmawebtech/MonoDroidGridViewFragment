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
using System.IO;
using Android.Util;
using Java.Util.Zip;
using Android.Support.V4.App;

namespace com.xamarin.recipes.filepicker
{
	[Activity (Label = "FileListGridViewFragment")]			
	public class FileListGridViewFragment : Fragment
	{
		public static string DefaultInitialDirectory = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;

		private FileListAdapter _adapter;
		private DirectoryInfo _directory;
		GridView gridView;

		//http://stackoverflow.com/questions/10942959/how-to-show-different-layout-in-fragments

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			_adapter = new FileListAdapter(Activity, new FileSystemInfo[0]);

			if (Directory.Exists (DefaultInitialDirectory + "/" + Android.OS.Environment.DirectoryDcim)) {

				DefaultInitialDirectory += "/" + Android.OS.Environment.DirectoryDcim;

				if (Directory.Exists (DefaultInitialDirectory + "/Camera")) {

					DefaultInitialDirectory = DefaultInitialDirectory + "/Camera";

				}
			}
			//ListAdapter = _adapter;
		}

		public override View OnCreateView (LayoutInflater p0, ViewGroup p1, Bundle p2)
		{
			View view = p0.Inflate (Resource.Layout.gridview, null);
			//return base.OnCreateView (p0, p1, p2);
			gridView = (GridView)view.FindViewById(Resource.Id.gridViewImage);
			gridView.ItemClick += OnListItemClick;
			gridView.Adapter = _adapter;
			return view;
		}




		public void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var fileSystemInfo = _adapter.GetItem(e.Position);

			if (fileSystemInfo.IsFile())
			{
				// Do something with the file.  In this case we just pop some toast.
				Log.Verbose("FileListFragment", "The file {0} was clicked.", fileSystemInfo.FullName);
				Toast.MakeText(Activity, "You selected file " + fileSystemInfo.FullName, ToastLength.Short).Show();
			}
			else
			{
				// Dig into this directory, and display it's contents
				RefreshFilesList(fileSystemInfo.FullName);
			}


		}

		public override void OnResume()
		{
			base.OnResume();
			RefreshFilesList(DefaultInitialDirectory);
		}

		public void RefreshFilesList(string directory)
		{
			IList<FileSystemInfo> visibleThings = new List<FileSystemInfo>();
			var dir = new DirectoryInfo(directory);

			try
			{
				foreach (var item in dir.GetFileSystemInfos().Where(item => item.IsVisible()))
				{
					visibleThings.Add(item);
				}
			}
			catch (Exception ex)
			{
				Log.Error("FileListFragment", "Couldn't access the directory " + _directory.FullName + "; " + ex);
				Toast.MakeText(Activity, "Problem retrieving contents of " + directory, ToastLength.Long).Show();
				return;
			}

			_directory = dir;

			_adapter.AddDirectoryContents(visibleThings);

			// If we don't do this, then the ListView will not update itself when then data set 
			// in the adapter changes. It will appear to the user that nothing has happened.
			//ListView.RefreshDrawableState();
			gridView.RefreshDrawableState ();
			Log.Verbose("FileListFragment", "Displaying the contents of directory {0}.", directory);
		}
	}
}

