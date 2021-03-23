
namespace CsBot.Interfaces
{
    interface IGame
    {
		string Name { get; }
        void Play(string command, int endCommand, string verb);
    }
}
