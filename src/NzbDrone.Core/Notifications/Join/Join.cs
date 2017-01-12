﻿using System.Collections.Generic;
using FluentValidation.Results;
using NzbDrone.Common.Extensions;
using NzbDrone.Core.Tv;

namespace NzbDrone.Core.Notifications.Join
{
    public class Join : NotificationBase<JoinSettings>
    {
        private readonly IJoinProxy _proxy;

        public Join(IJoinProxy proxy)
        {
            _proxy = proxy;
        }

        public override string Link => "https://joinjoaomgcd.appspot.com/";

        public override void OnGrab(GrabMessage grabMessage)
        {
            const string title = "Radarr - Episode Grabbed";

            _proxy.SendNotification(title, grabMessage.Message, Settings);
        }

        public override void OnDownload(DownloadMessage message)
        {
            const string title = "Radarr - Episode Downloaded";

            _proxy.SendNotification(title, message.Message, Settings);
        }

        public override void OnRename(Series series)
        {
        }

        public override string Name => "Join";

        public override bool SupportsOnRename => false;

        public override ValidationResult Test()
        {
            var failures = new List<ValidationFailure>();

            failures.AddIfNotNull(_proxy.Test(Settings));

            return new ValidationResult(failures);
        }
    }
}
