
namespace CsBot.Interfaces
{
    interface ICommand
    {
		string Name { get; }
        void Handle(string command, int endCommand, string verb);
    }
}
