namespace NLib.Patterns
{
    /// <summary>
    /// Represents the command pattern.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes the action.
        /// </summary>
        void Execute();

        /// <summary>
        /// Undoes the action.
        /// </summary>
        void Undo();

        /// <summary>
        /// Redoes the action.
        /// </summary>
        void Redo();
    }
}
