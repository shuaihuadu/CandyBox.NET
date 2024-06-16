namespace System
{
    /// <summary>
    /// Common extensions of <see cref="int"/>.
    /// </summary>
    public static class Int32Extensions
    {
        /// <summary>
        /// Convert specified int to an <see cref="Enum"/> value.
        /// </summary>
        /// <typeparam name="T">The type of enum.</typeparam>
        /// <param name="value">The int value.</param>
        /// <returns>The converted <see cref="Enum"/> value.</returns>
        public static T ToEnum<T>(this int value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }
        /// <summary>
        /// Convert specified int value to a file size string.
        /// <para>Supported:KB MB GB TB PB EB</para>
        /// </summary>
        /// <param name="size">The file size value.</param>
        /// <returns>The file size string. eg:MB,GB...</returns>
        public static string ToFileSizeString(this int size)
        {
            decimal decimalSize = new(size);

            return decimalSize.ToFileSizeString();
        }
    }
}