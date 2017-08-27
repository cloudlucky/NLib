using System;
using System.Diagnostics.CodeAnalysis;

namespace NLib.Extensions
{
    /// <summary>
    /// Defines extensions methods for <see cref="EventHandler"/>.
    /// </summary>
    public static class EventHandlerExtensions
    {
        /// <summary>
        /// Raises the event which is represented by the event <paramref name="handler"/> specified with empty arguments, if the <paramref name="handler"/> specified isn't null.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="sender">The sender.</param>
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "It's not an event")]
        public static void RaiseEvent(this EventHandler handler, object sender)
        {
            RaiseEvent(handler, sender, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the event which is represented by the <paramref name="handler"/> specified with arguments, if the <paramref name="handler"/> specified isn't null.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "It's not an event")]
        public static void RaiseEvent(this EventHandler handler, object sender, EventArgs e)
        {
            handler?.Invoke(sender, e);
        }

        /// <summary>
        /// Raises the event which is represented by the <paramref name="handler"/> specified with arguments, if the <paramref name="handler"/> specified isn't null.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="EventArgs"/>.</typeparam>
        /// <param name="handler">The handler.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/>.</param>
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "It's not an event")]
        public static void RaiseEvent<T>(this EventHandler<T> handler, object sender, T e)
            where T : EventArgs
        {
            handler?.Invoke(sender, e);
        }
    }
}
