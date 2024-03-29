#region WatiN Copyright (C) 2006-2009 Jeroen van Menen

//Copyright 2006-2009 Jeroen van Menen
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

#endregion Copyright

using System;
using System.Collections.Generic;
using System.Threading;
using WatiN.Core.Exceptions;
using WatiN.Core.Interfaces;
using WatiN.Core.Logging;
using WatiN.Core.Native.InternetExplorer;
using WatiN.Core.Native.Windows;
using WatiN.Core.UtilityClasses;
using WatiN.Core;

namespace WorkLibrary
{
	/// <summary>
	/// This class handles alert/popup dialogs. Every second it checks if a dialog
	/// is shown. If so, it stores it's message in the alertQueue and closses the dialog
	/// by clicking the close button in the title bar.  
	/// </summary>
	public class FormDialogWatcher : IDisposable
	{
		private readonly IntPtr _mainWindowHwnd;
		private bool _keepRunning = true;
		private readonly IList<IDialogHandler> _handlers;
		private readonly Thread _watcherThread;
		private bool _closeUnhandledDialogs = Settings.AutoCloseDialogs;

	   // private static IList<DialogWatcher> dialogWatchers = new List<DialogWatcher>();

	    /// <summary>
		/// Gets the dialog watcher for the specified (main) internet explorer window. 
		/// It creates new instance if no dialog watcher for the specified window exists.
		/// </summary>
        /// <param name="mainWindowHwnd">The (main) internet explorer window.</param>
		/// <returns></returns>
		

		/// <summary>
		/// Initializes a new instance of the <see cref="DialogWatcher"/> class.
        /// You are encouraged to use the Factory method <see cref="DialogWatcher.GetDialogWatcherFromCache"/>
		/// instead.
		/// </summary>
		/// <param name="mainWindowHwnd">The main window handle of internet explorer.</param>
        public FormDialogWatcher(IntPtr mainWindowHwnd)
		{
			_mainWindowHwnd = mainWindowHwnd;

			_handlers = new List<IDialogHandler>();

			// Create thread to watch windows
			_watcherThread = new Thread(Start);
			// Start the thread.
			_watcherThread.Start();
		}
        
		/// <summary>
		/// Increases the reference count of this DialogWatcher instance with 1.
		/// </summary>
		
		/// <summary>
		/// Decreases the reference count of this DialogWatcher instance with 1.
		/// When reference count becomes zero, the Dispose method will be 
		/// automatically called. This method will throw an <see cref="ReferenceCountException"/>
		/// if the reference count is zero.
		/// </summary>
		

		/// <summary>
		/// Adds the specified handler.
		/// </summary>
		/// <param name="handler">The handler.</param>
		public void Add(IDialogHandler handler)
		{
			lock (this)
			{
				_handlers.Add(handler);
			}
		}

		/// <summary>
		/// Removes the specified handler.
		/// </summary>
		/// <param name="handler">The handler.</param>
		public void Remove(IDialogHandler handler)
		{
			lock (this)
			{
				_handlers.Remove(handler);
			}
		}

		/// <summary>
		/// Removes all instances that match <paramref name="handler"/>.
		/// This method determines equality by calling Object.Equals.
		/// </summary>
		/// <param name="handler">The object implementing IDialogHandler.</param>
		/// <example>
		/// If you want to use RemoveAll with your custom dialog handler to
		/// remove all instances of your dialog handler from a DialogWatcher instance,
		/// you should override the Equals method in your custom dialog handler class 
		/// like this:
		/// <code>
		/// public override bool Equals(object obj)
		/// {
		///   if (obj == null) return false;
		///   
		///   return (obj is YourDialogHandlerClassNameGoesHere);
		/// }                               
		/// </code>
		/// You could also inherit from <see cref="BaseDialogHandler"/> instead of implementing
		/// <see cref="IDialogHandler"/> in your custom dialog handler. <see cref="BaseDialogHandler"/> provides
		/// overrides for Equals and GetHashCode that work with RemoveAll.
		/// </example>
		public void RemoveAll(IDialogHandler handler)
		{
			while (Contains(handler))
			{
				Remove(handler);
			}
		}

		/// <summary>
		/// Removes all registered dialog handlers.
		/// </summary>
		public void Clear()
		{
			lock (this)
			{
				_handlers.Clear();
			}
		}

		/// <summary>
		/// Determines whether this <see cref="DialogWatcher"/> contains the specified dialog handler.
		/// </summary>
		/// <param name="handler">The dialog handler.</param>
		/// <returns>
		/// 	<c>true</c> if [contains] [the specified handler]; otherwise, <c>false</c>.
		/// </returns>
        public bool Contains(IDialogHandler handler)
		{
			lock (this)
			{
				return _handlers.Contains(handler);
			}
		}

		/// <summary>
		/// Gets the count of registered dialog handlers.
		/// </summary>
		/// <value>The count.</value>
		public int Count
		{
			get
			{
				lock (this)
				{
					return _handlers.Count;
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether unhandled dialogs should be closed automaticaly.
		/// The initial value is set to the value of <cref name="Settings.AutoCloseDialogs" />.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if unhandled dialogs should be closed automaticaly; otherwise, <c>false</c>.
		/// </value>
		public bool CloseUnhandledDialogs
		{
			get
			{
				lock (this)
				{
					return _closeUnhandledDialogs;
				}
			}
			set
			{
				lock (this)
				{
					_closeUnhandledDialogs = value;
				}
			}
		}

		/// <summary>
		/// Gets the (main) internet explorer window hanlde this dialog watcher watches.
		/// </summary>
		/// <value>The process id.</value>
		public IntPtr MainWindowHwnd
		{
			get { return _mainWindowHwnd; }
		}

		/// <summary>
		/// Called by the constructor to start watching popups
		/// on a separate thread.
		/// </summary>
		private void Start()
		{
			while (_keepRunning)
			{
                var mainWindow = new Window(MainWindowHwnd);
                if (mainWindow.Exists())
                {
                    var winEnumerator = new WindowsEnumerator();
                    var windows = winEnumerator.GetWindows(window => window.ProcessID == mainWindow.ProcessID);
				
                    foreach (var window in windows)
                    {
                        if (!_keepRunning) return;
                        HandleWindow(new Window(window.Hwnd));
                    }

                    // Keep DialogWatcher responsive during 1 second sleep period
                    var count = 0;
                    while (_keepRunning && count < 5)
                    {
                        Thread.Sleep(200);
                        count++;
                    }
                }
                else
                {
                    _keepRunning = false;
                }
                
			}
		}

		public bool IsRunning
		{
			get { return _watcherThread.IsAlive; }
		}

	   

	    /// <summary>
	    /// Get the last stored exception thrown by a dialog handler while 
	    /// calling the <see cref="IDialogHandler.HandleDialog"/> method of the
	    /// dialog handler.
	    /// </summary>
	    /// <value>The last exception.</value>
	    public Exception LastException { get; private set; }

	    /// <summary>
		/// If the window is a dialog and visible, it will be passed to
		/// the registered dialog handlers. I none if these can handle
		/// it, it will be closed if <see cref="CloseUnhandledDialogs"/>
		/// is <c>true</c>.
		/// </summary>
		/// <param name="window">The window.</param>
		public void HandleWindow(Window window)
		{
			if (!window.IsDialog()) return;
            
            // This is needed otherwise the window Style will return a "wrong" result.
            WaitUntilVisibleOrTimeOut(window);

		    // Lock the thread and see if a handler will handle
		    // this dialog window
           
		    lock (this)
		    {
                /*
               foreach (var dialogHandler in _handlers)
               {
                   try
                   {
                       if (dialogHandler.CanHandleDialog(window, MainWindowHwnd))
                       {
                           if (dialogHandler.HandleDialog(window)) return;
                       }
                   }
                   catch (Exception e)
                   {
                       LastException = e;

                       Logger.LogAction("Exception was thrown while DialogWatcher called HandleDialog: {0}",e.ToString());
                   }
               }
               */
		        // If no handler handled the dialog, see if the dialog
		        // should be closed automatically.
		       //if (!CloseUnhandledDialogs || MainWindowHwnd != window.ToplevelWindow.Hwnd) return;
                if (!CloseUnhandledDialogs ) return;
                //Logger.LogAction("Auto closing dialog with title: '{0}', text: {1}, style: ", window.Title, window.Message, window.StyleInHex);
		        try
		        {
                    window.ForceClose();
		        }
		        catch (Exception)
		        {
		           
		        }
               
		    }
		}

	    private static void WaitUntilVisibleOrTimeOut(Window window)
		{
			// Wait untill window is visible so all properties
			// of the window class (like Style and StyleInHex)
			// will return valid values.
		    /*var tryActionUntilTimeOut = new TryFuncUntilTimeOut(TimeSpan.FromSeconds(Settings.WaitForCompleteTimeOut));
            var success = tryActionUntilTimeOut.Try(() => window.Visible);

            if (!success)
            {
                Logger.LogAction("Dialog with title '{0}' not visible after {1} seconds.", window.Title, Settings.WaitForCompleteTimeOut);
            }*/
		}

		#region IDisposable Members

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or
		/// resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			lock (this)
			{
				_keepRunning = false;
			}
			if (IsRunning)
			{
                _watcherThread.Suspend();
                _watcherThread.Abort();
			}
			Clear();
		}

		#endregion
	}
}