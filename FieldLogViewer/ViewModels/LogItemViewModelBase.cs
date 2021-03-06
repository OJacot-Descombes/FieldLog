﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using Unclassified.UI;
using Unclassified.Util;

namespace Unclassified.FieldLogViewer.ViewModels
{
	internal class LogItemViewModelBase : ViewModelBase, IComparable<LogItemViewModelBase>, IEditableObject
	{
		public int EventCounter { get; set; }
		public DateTime Time { get; set; }

		public TimeSpan TimeToNextItem
		{
			get { return GetValue<TimeSpan>("TimeToNextItem"); }
			set { SetValue(value, "TimeToNextItem"); }
		}

		public void RefreshTimeToNextItem()
		{
			OnPropertyChanged("TimeToNextItem");
		}

		public virtual Color BackColor
		{
			get { return Colors.Transparent; }
		}

		[NotifiesOn("TimeToNextItem")]
		public Brush BottomBorderBrush
		{
			get
			{
				if (App.Settings.ShowTimeSeparator)
				{
					long halfOpacityTicks = TimeSpan.FromMinutes(1).Ticks;
					float opacity = 1 - 1 / (((float)TimeToNextItem.Ticks / halfOpacityTicks) + 1);
					return new SolidColorBrush(BackColor.BlendWith(Color.FromRgb(0, 0, 0), 1 - opacity));
				}
				return Brushes.Transparent;
			}
		}

		public int UtcOffset
		{
			get { return GetValue<int>("UtcOffset"); }
			set { SetValue(value, "UtcOffset"); }
		}

		public int IndentLevel
		{
			get { return GetValue<int>("IndentLevel"); }
			set { SetValue(value, "IndentLevel"); }
		}

		public string DisplayTime
		{
			get
			{
				switch (App.Settings.ItemTimeMode)
				{
					case ItemTimeType.Utc:
						return Time.ToString("yyyy-MM-dd  HH:mm:ss.ffffff") + "  UTC";
					case ItemTimeType.Local:
						return Time.ToLocalTime().ToString("yyyy-MM-dd  HH:mm:ss.ffffff");
					case ItemTimeType.Remote:
						int hours = UtcOffset / 60;
						int mins = Math.Abs(UtcOffset) % 60;
						return Time.AddMinutes(UtcOffset).ToString("yyyy-MM-dd  HH:mm:ss.ffffff") + "  " +
							hours.ToString("+00;-00;+00") + ":" + mins.ToString("00");
				}
				return null;
			}
		}

		public void RaiseDisplayTimeChanged()
		{
			OnPropertyChanged("DisplayTime");
		}

		public virtual int CompareTo(LogItemViewModelBase other)
		{
			// First compare the items by time
			int i = this.Time.CompareTo(other.Time);
			if (i != 0) return i;

			// If the time is equal, consider the event counter value and handle overflows
			const int range = 10000;
			if (EventCounter > int.MaxValue - range && other.EventCounter < int.MinValue + range)
			{
				// Overflow, other is newer
				return -1;
			}
			if (EventCounter < int.MinValue + range && other.EventCounter > int.MaxValue - range)
			{
				// Overflow, other is older
				return 1;
			}
			return EventCounter.CompareTo(other.EventCounter);
		}

		/// <summary>
		/// Refreshes all data in the item that can be resolved from other sources. Deriving
		/// classes override this method to refresh the relevant item data.
		/// </summary>
		public virtual void Refresh()
		{
		}

		#region IEditableObject members

		public void BeginEdit()
		{
			// Does nothing. IEditableObject is just used for signalling the CollectionView to
			// update the item.
		}

		public void CancelEdit()
		{
			// Does nothing. IEditableObject is just used for signalling the CollectionView to
			// update the item.
		}

		public void EndEdit()
		{
			// Does nothing. IEditableObject is just used for signalling the CollectionView to
			// update the item.
		}

		#endregion IEditableObject members
	}
}
