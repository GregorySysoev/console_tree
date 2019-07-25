using System;
namespace myTree
{
    abstract class Command
    {
        public abstract void Execute();
    }

    class HelpCommand : Command
    {

        public override void Execute()
        {
            Console.WriteLine("List of available commands:");
            Console.WriteLine("-d --depth [num]  nesting level");
            Console.WriteLine("-s --size  show size of files");
        }
    }

    class DepthCommand : Command
    {
        public override void Execute()
        {

        }
    }
    class SizeCommand : DepthCommand
    {
        public override void Execute()
        {

        }
    }
}