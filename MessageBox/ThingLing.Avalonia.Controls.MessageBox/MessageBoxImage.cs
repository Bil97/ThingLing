namespace ThingLing.Avalonia.Controls
{
    /// <summary>
    ///  Specifies the icon that is displayed by a message box.
    /// </summary>

    public enum MessageBoxImage
    {
        /// <summary>
        ///     The message box contains no symbols.
        /// </summary> 
        None = 0,

        /// <summary>
        /// The message box contains no symbols consisting of a red X.
        /// </summary>
        Error,

        /// <summary>
        ///     The message box contains a symbol consisting of a white dash in a circle with a red background.
        /// </summary>
        Stop,

        /// <summary>
        ///     The message box contains a symbol consisting of an exclamation point in a triangle
        ///     with a yellow background.
        /// </summary>
        Warning,

        /// <summary>
        ///     The message box contains a symbol consisting of a lowercase letter i in a circle.
        /// </summary>
        Information
    }
}
