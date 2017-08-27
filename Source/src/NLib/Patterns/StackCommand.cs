namespace NLib.Patterns
{
    using System;
    using System.Collections.Generic;

    using NLib.Patterns.Resources;

    /// <summary>
    /// Stack command pattern to do Undo/Redo.
    /// </summary>
    public class StackCommand : ICommand
    {
        /// <summary>
        /// the undo stack.
        /// </summary>
        private readonly Stack<Action> undoStack;
        
        /// <summary>
        /// the redo stack.
        /// </summary>
        private readonly Stack<Action> redoStack;

        /// <summary>
        /// Execute the command.
        /// </summary>
        private readonly Action execute;

        /// <summary>
        /// Undo the command.
        /// </summary>
        private readonly Action undo;

        /// <summary>
        /// Redo the command
        /// </summary>
        private readonly Action redo;

        /// <summary>
        /// Initializes a new instance of the <see cref="StackCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute command.</param>
        /// <param name="undo">The undo command.</param>
        /// <exception cref="ArgumentNullException">execute is null.</exception>
        /// /// <exception cref="ArgumentNullException">undo is null.</exception>
        public StackCommand(Action execute, Action undo)
            : this(execute, undo, execute)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StackCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute command.</param>
        /// <param name="undo">The undo command.</param>
        /// <param name="redo">The redo command. If it's null, the <paramref name="execute"/> will also be the <paramref name="redo"/> command</param>
        /// <exception cref="ArgumentNullException">execute is null.</exception>
        /// <exception cref="ArgumentNullException">undo is null.</exception>
        public StackCommand(Action execute, Action undo, Action redo)
            : this()
        {
            Check.Current.ArgumentNullException(execute, "execute")
                         .ArgumentNullException(undo, "undo");

            this.execute = execute;
            this.undo = undo;
            this.redo = redo ?? execute;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StackCommand"/> class.
        /// </summary>
        protected StackCommand()
        {
            this.undoStack = new Stack<Action>();
            this.redoStack = new Stack<Action>();
        }

        /// <summary>
        /// Gets a value indicating whether this instance can undo.
        /// </summary>
        /// <value><c>true</c> if this instance can undo; otherwise, <c>false</c>.</value>
        public bool CanUndo
        {
            get { return this.UndoStack.Count > 0; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can redo.
        /// </summary>
        /// <value><c>true</c> if this instance can redo; otherwise, <c>false</c>.</value>
        public bool CanRedo
        {
            get { return this.RedoStack.Count > 0; }
        }

        /// <summary>
        /// Gets the undo stack.
        /// </summary>
        /// <value>The undo stack.</value>
        protected Stack<Action> UndoStack 
        { 
            get { return this.undoStack; }
        }

        /// <summary>
        /// Gets the redo stack.
        /// </summary>
        /// <value>The redo stack.</value>
        protected Stack<Action> RedoStack
        {
            get { return this.redoStack; }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public virtual void Clear()
        {
            this.UndoStack.Clear();
            this.RedoStack.Clear();
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public virtual void Execute()
        {
            this.UndoStack.Push(this.undo);
            this.execute();
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public virtual void Undo()
        {
            Check.Current.Requires<InvalidOperationException>(this.CanUndo, SimpleCommandResource.Undo_InvalidOperationException);
            this.RedoStack.Push(this.undoStack.Pop());
            this.undo();
        }

        /// <summary>
        /// Redoes the action.
        /// </summary>
        public virtual void Redo()
        {
            Check.Current.Requires<InvalidOperationException>(this.CanRedo, SimpleCommandResource.Redo_InvalidOperationException);
            this.UndoStack.Push(this.redoStack.Pop());
            this.redo();
        }
    }
}
