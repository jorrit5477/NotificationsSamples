using System;
using JetBrains.Annotations;
using Unity.Notifications.Android;
using Unity.Notifications.iOS;
using UnityEngine.UIElements;

namespace NotificationSamples.Android
{
	/// <summary>
	/// Android implementation of <see cref="IGameNotificationsPlatform"/>.
	/// </summary>
	public class AndroidNotificationsPlatform : IGameNotificationsPlatform<AndroidGameNotification>
	{
		/// <summary>
		/// Gets or sets the default channel ID for notifications.
		/// </summary>
		/// <value>The default channel ID for new notifications, or null.</value>
		public string DefaultChannelId { get; set; }

		/// <inheritdoc />
		/// <remarks>
		/// Will set the <see cref="AndroidGameNotification.Id"/> field of <paramref name="gameNotification"/>.
		/// </remarks>
		public void ScheduleNotification(AndroidGameNotification gameNotification)
		{
			int notificationId = AndroidNotificationCenter.SendNotification(gameNotification.InternalNotification,
			                                                                gameNotification.Channel);
			gameNotification.OnScheduled(notificationId);
		}

		/// <inheritdoc />
		/// <remarks>
		/// Will set the <see cref="AndroidGameNotification.Id"/> field of <paramref name="gameNotification"/>.
		/// </remarks>
		public void ScheduleNotification([NotNull] IGameNotification gameNotification)
		{
			if (gameNotification == null)
			{
				throw new ArgumentNullException(nameof(gameNotification));
			}

			if (! (gameNotification is AndroidGameNotification androidNotification))
			{
				throw new InvalidOperationException("Notification provided to ScheduleNotification isn't an AndroidNotification.");
			}
			
			ScheduleNotification(androidNotification);
		}

		/// <inheritdoc />
		/// <summary>
		/// Create a new <see cref="T:NotificationSamples.Android.AndroidNotification" />.
		/// </summary>
		public AndroidGameNotification CreateNotification()
		{
			var notification = new AndroidGameNotification()
			{
				Channel = DefaultChannelId
			};

			return notification;
		}

		/// <inheritdoc />
		IGameNotification IGameNotificationsPlatform.CreateNotification()
		{
			return CreateNotification();
		}
	}
}