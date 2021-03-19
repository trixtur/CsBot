
namespace CsBot.Command
{
    interface ICommand
    {
        void Handle(string command, int endCommand, string verb);
    }
}
