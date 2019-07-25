namespace TogglOn.Client.AspNetCore
{
    internal class TogglOnContextAccessor : ITogglOnContextAccessor
    {
        private static TogglOnContextHolder _togglOnContextCurrent = new TogglOnContextHolder();

        public TogglOnContext TogglOnContext
        {
            get
            {
                return _togglOnContextCurrent.Context;
            }
            set
            {
                var holder = _togglOnContextCurrent;
                if (holder != null)
                {
                    holder.Context = null;
                }

                if (value != null)
                {
                    _togglOnContextCurrent = new TogglOnContextHolder { Context = value };
                }
            }
        }

        private class TogglOnContextHolder
        {
            public TogglOnContext Context;
        }
    }
}
