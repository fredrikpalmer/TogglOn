using System.Threading;

namespace TogglOn.Client.AspNetCore
{
    internal class TogglOnContextAccessor : ITogglOnContextAccessor
    {
        private static AsyncLocal<TogglOnContextHolder> _togglOnContextCurrent = new AsyncLocal<TogglOnContextHolder>();

        public TogglOnContext TogglOnContext
        {
            get
            {
                return _togglOnContextCurrent.Value?.Context;
            }
            set
            {
                var holder = _togglOnContextCurrent.Value;
                if (holder != null)
                {
                    holder.Context = null;
                }

                if (value != null)
                {
                    _togglOnContextCurrent.Value = new TogglOnContextHolder { Context = value };
                }
            }
        }

        private class TogglOnContextHolder
        {
            public TogglOnContext Context;
        }
    }
}
