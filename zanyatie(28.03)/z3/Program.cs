using System;
using System.Collections.Generic;

namespace z3_command
{
    interface ICommand
    {
        void Execute();
        void Undo();
    }

    class TextEditor
    {
        private string _text = "";

        public void Copy(string text)
        {
            _text = text;
            Console.WriteLine("Скопировано: " + text);
        }

        public void Paste()
        {
            Console.WriteLine("Вставлено: " + _text);
        }

        public string GetText()
        {
            return _text;
        }
    }

    class CopyCommand : ICommand
    {
        private TextEditor _editor;
        private string _text;

        public CopyCommand(TextEditor editor, string text)
        {
            _editor = editor;
            _text = text;
        }

        public void Execute()
        {
            _editor.Copy(_text);
        }

        public void Undo()
        {
            Console.WriteLine("Отмена копирования");
        }
    }

    class PasteCommand : ICommand
    {
        private TextEditor _editor;

        public PasteCommand(TextEditor editor)
        {
            _editor = editor;
        }

        public void Execute()
        {
            _editor.Paste();
        }

        public void Undo()
        {
            Console.WriteLine("Отмена вставки");
        }
    }

    class EditorInvoker
    {
        private Stack<ICommand> _commands = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _commands.Push(command);
        }

        public void Undo()
        {
            if (_commands.Count > 0)
            {
                ICommand command = _commands.Pop();
                command.Undo();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TextEditor editor = new TextEditor();
            EditorInvoker invoker = new EditorInvoker();

            ICommand copy = new CopyCommand(editor, "Hello World");
            invoker.ExecuteCommand(copy);

            ICommand paste = new PasteCommand(editor);
            invoker.ExecuteCommand(paste);

            invoker.Undo();
            invoker.Undo();

            Console.ReadLine();
        }
    }
}